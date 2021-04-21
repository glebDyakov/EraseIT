using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace CyberCradle
{
    public class ImagePsdCreator/* : IPsdCreator*/
    {
        private bool forBundle = false;

        public string GetDisplayName( )
        {
            if ( forBundle )
            {
                return "Image for bundle";
            }
            else
            {
                return "Image";
            }
        }

        public ImagePsdCreator( )
        {
            this.forBundle = false;
        }

        public ImagePsdCreator( bool forBundle )
        {
            this.forBundle = forBundle;
        }

        public SpriteDescription[] ParseSinCityPSDAndCreateColliders(   PsdDocument psdDocument, 
                                                                        string pathToSaveBundle, 
                                                                        string sceneName, 
                                                                        string platformName, 
                                                                        TextureImporterType _importType, 
                                                                        bool highDetailCollider,
                                                                        float colliderScaleKoeff )
        {
            AssetDatabase.Refresh();

            List<PsdLayer> layers = psdDocument.GetLayers();            
            List<SpriteDescription> spriteList = new List<SpriteDescription>();
            List<string> textureNames = new List<string>();

            try
            {
                //удаляем ассет с текстурами из Resources/Images/Levels
                if (System.IO.Directory.Exists(PsdParserUtils.ResourcePath + psdDocument.document.Fullname))
                {
                    FileUtil.DeleteFileOrDirectory(PsdParserUtils.ResourcePath + psdDocument.document.Fullname);
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                Debug.Log(ex);
            }

            try
            {
                //удаляем папку с текстурами из Assets/PingviGames/HogScenes/sceneName/[resolution]/Textures
                if (System.IO.Directory.Exists(pathToSaveBundle + "/Textures"))
                {
                    FileUtil.DeleteFileOrDirectory(pathToSaveBundle + "/Textures");
                }
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            }
            catch (DirectoryNotFoundException ex)
            {
                Debug.Log(ex);
            }

            int idimg = 0;
            foreach (PsdLayer layer in layers)
            {
                if (layer.Type == LayerType.Image)
                {
                    string fileName = layer.Fullname.Replace("/" + sceneName + "/", "").Replace("%SCALE%_","").Trim().ToLower() + "_id"+idimg.ToString();
                    string filePath = pathToSaveBundle + "Textures/" + fileName + ".png"; 
                    //Debug.Log("filepath:"+ filePath);
                    
                    //создание текстур
                    System.IO.Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    Texture2D texture = new Texture2D((int)layer.rect.width, (int)layer.rect.height);
                    textureNames.Add(StringUtils.RemoveFirst(layer.Fullname.Replace("%SCALE%_","").Trim().ToLower()));

                    if (layer.Data == null || layer.Data.Length == 0)
                    {
                        Debug.LogError("Psd layer without image data: " + layer.Fullname.Trim());
                    }
                    else
                    {
                        texture.SetPixels32(layer.Data);
                    }

                    //сохранение в файл
                    byte[] content = texture.EncodeToPNG();
                    File.WriteAllBytes(filePath, content);

                    //создание инфы для ассета
                    SpriteDescription spriteDescription = new SpriteDescription();

                    bool needScaleCollider = layer.Fullname.Contains("%SCALE%");

                    spriteDescription.name = StringUtils.RemoveFirst(layer.Fullname.Replace("%SCALE%_","").Trim().ToLower());
                    spriteDescription.sourceTexture = filePath.Substring(filePath.IndexOf("Assets"));
                    spriteDescription.sourceRect = new Rect(0.0f, 0.0f, texture.width, texture.height);

                    spriteDescription.sourceRect = PsdParserUtils.CorrectY(psdDocument, layer.Rect);
                    //hiddenItem.patchImage = PsdParserUtils.SpriteFromLayerFullname(psdDocument, patchLayer.Fullname);
                    spriteDescription.Zcoord = PsdParserUtils.LayerToZ(layer.Layer);

                    //создаем коллайдеры
                    //Contour[] colliderContours = generateCollider(texture, highDetailCollider, needScaleCollider ? colliderScaleKoeff : 1f);
                    //spriteDescription.colliderContours = colliderContours;

                    spriteList.Add(spriteDescription);
                    Texture2D.DestroyImmediate(texture);

                    idimg++;
                }
            }



            //создаем ассет с инфой по текстурам            
            
            
           

            //импортируем изображения в юнити
            string path = pathToSaveBundle + "Textures/"; //PsdParserUtils.CommonPath + pathExtention + psdDocument.document.Fullname;

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            List<FileInfo> arr = new List<FileInfo>();
            Tools.GetFiles(arr, new DirectoryInfo(path));

            


            for (int i = 0; i < arr.Count; ++i)
            {
                string assetPath = arr[i].FullName.Substring(arr[i].FullName.IndexOf("Assets")).Replace("\\", "/");
                bool hasAlpha = true;

                
                TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                textureImporter.textureType = _importType;
                
                if (textureImporter != null)
                {
                    SpritePostprocessor.isReadable = true;

                    SpritePostprocessor.AssignImportSettings(textureImporter, TextureImporterFormat.RGBA32);
                    //textureImporter.SaveAndReimport();
                    AssetDatabase.ImportAsset(assetPath);
                    
                    Texture2D txt = (Texture2D)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Texture2D));

                    if (txt == null)
                    {
                        Debug.LogError("========== >>>>>>>>>>>>>>>> txt=null");
                    }

                    SpritePostprocessor.isReadable = false;
/*#if UNITY_ANDROID
                    TextureImporterFormat format = TextureImporterFormat.ETC_RGB4Crunched;
                    textureImporter.allowAlphaSplitting = true;
#else*/
                    TextureImporterFormat format = TextureImporterFormat.RGBA32;
                    //#endif
                    if (textureImporter.assetPath.Contains("erase_correct") || textureImporter.assetPath.Contains("erase_wrong") || textureImporter.assetPath.Contains("erase_neutral"))
                    { 
                        //для стираемых всегда с альфаканалом
                    }
                    else if (!TextureAlphaCannelTest.IsExistAlpha(txt))
                    {
/*#if UNITY_ANDROID
                        format = TextureImporterFormat.ETC_RGB4Crunched;
                        textureImporter.allowAlphaSplitting = false;
#else*/
                        format = TextureImporterFormat.RGB24;
//#endif
                        hasAlpha = false;
                    }
                    
                    SpritePostprocessor.AssignImportSettings(textureImporter, format);
                    AssetDatabase.ImportAsset(assetPath);
                }
                else
                {
                    Debug.LogError("========================================   textureImporter == null " + assetPath);
                }
                

                TextureImporter textureImporter2 = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                if (textureImporter2 != null)
                {                    
                    

                    TextureImporterSettings texSettings = new TextureImporterSettings();

                    textureImporter2.ReadTextureSettings(texSettings);
                    texSettings.spriteAlignment = (int)SpriteAlignment.Custom;
                    //texSettings.readable = true;                    
                    textureImporter2.SetTextureSettings(texSettings);


                    //pack tag
                    if (textureImporter2.assetPath.Contains("circle_place"))
                    {
                        textureImporter2.spritePackingTag = string.Empty;
                    }
                    else if (textureImporter2.assetPath.Contains("_back"))
                    {
                        textureImporter2.spritePackingTag = psdDocument.document.Name + "_" + psdDocument.document.Width.ToString() + "x" + psdDocument.document.Height.ToString() + "_background_" + platformName; //бэк в отдельный атлас
                    }
                    else if (textureImporter2.assetPath.Contains("erase_correct") || textureImporter2.assetPath.Contains("erase_wrong") || textureImporter2.assetPath.Contains("erase_neutral"))
                    {
                        string[] namesStr = textureImporter2.assetPath.Replace(".png","").Split('/');
                        textureImporter2.spritePackingTag = 
                            psdDocument.document.Name + "_" + psdDocument.document.Width.ToString() + "x" + psdDocument.document.Height.ToString() + "_items_" + platformName+namesStr[namesStr.Length-1]; //атлас с предметами
                    }
                    else
                    {
                        textureImporter2.spritePackingTag = psdDocument.document.Name + "_" + psdDocument.document.Width.ToString() + "x" + psdDocument.document.Height.ToString() + "_items_" + platformName; //атлас с предметами
                    }
                    textureImporter2.filterMode = FilterMode.Bilinear;
                    textureImporter2.wrapMode = TextureWrapMode.Clamp;
                    textureImporter2.textureType = _importType;
                    textureImporter2.spritePivot = new Vector2(0.5f, 0.5f);
                    textureImporter2.spritePixelsPerUnit = 1;
                    textureImporter2.compressionQuality = 100;
                    



                    //настройки импорта     
                    //webgl
                    TextureImporterPlatformSettings platformSettings = textureImporter2.GetPlatformTextureSettings("WebGL");
                    if (platformName == "mobile") //webgl mobile
                    {
                        platformSettings.format = (hasAlpha) ? TextureImporterFormat.RGBA32 : TextureImporterFormat.RGB24;
                        //platformSettings.textureCompression = TextureImporterCompression.Compressed;
                        //platformSettings.crunchedCompression = true;
                        //platformSettings.compressionQuality = 100;
                    }
                    else //webgl
                    {
                        platformSettings.format = (hasAlpha) ? TextureImporterFormat.DXT5Crunched : TextureImporterFormat.DXT1Crunched;
                        platformSettings.textureCompression = TextureImporterCompression.Compressed;
                        platformSettings.crunchedCompression = true;
                        platformSettings.compressionQuality = 100;
                    }
                    platformSettings.overridden = true;
                    textureImporter2.SetPlatformTextureSettings(platformSettings);

                    //IOS
                    TextureImporterPlatformSettings platformSettingsIOS = textureImporter2.GetPlatformTextureSettings("IOS");
                    platformSettingsIOS.format = (hasAlpha) ? TextureImporterFormat.PVRTC_RGBA4 : TextureImporterFormat.PVRTC_RGB4;
                    platformSettingsIOS.textureCompression = TextureImporterCompression.Compressed;
                    platformSettingsIOS.crunchedCompression = true;
                    platformSettingsIOS.compressionQuality = 100;
                    platformSettingsIOS.overridden = true;
                    textureImporter2.SetPlatformTextureSettings(platformSettingsIOS);


                    //android                    
                    TextureImporterPlatformSettings platformSettingsAndroid = textureImporter2.GetPlatformTextureSettings("Android");
                    platformSettingsAndroid.format = TextureImporterFormat.ETC2_RGBA8Crunched; // ETC_RGB4Crunched;
                    platformSettingsAndroid.textureCompression = TextureImporterCompression.Compressed;
                    platformSettingsAndroid.crunchedCompression = true;
                    platformSettingsAndroid.compressionQuality = 100;
                    //platformSettingsAndroid.allowsAlphaSplitting = hasAlpha;
                    platformSettingsAndroid.overridden = true;                    
                    textureImporter2.SetPlatformTextureSettings(platformSettingsAndroid);
                    

                    UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(textureImporter2.assetPath, typeof(Texture2D));
                    if (asset)
                    {
                        EditorUtility.SetDirty(asset);
                    }
                    
                    textureImporter2.SaveAndReimport();
                    //Debug.Log("[asset "+ textureImporter2.assetPath +"] textureType:"+ textureImporter2.textureType.ToString());

                }
                AssetDatabase.ImportAsset(assetPath);
                
            }

            AssetDatabase.SaveAssets();


            AssetDatabase.Refresh();



            

            


            

            return spriteList.ToArray();

        }

        private Contour[] generateCollider(Texture2D texture, bool highDetailCollider, float scaleSize)
        {
            
            Texture2D texture_mask = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
            Color32[] sourceColors = texture.GetPixels32();
            Color[] colors = new Color[texture.width * texture.height];
            for (int idx = 0; idx < texture.width * texture.height; ++idx)
            {
                if (sourceColors[idx].a == 0) colors[idx] = new Color(0f, 0f, 0f, 0f);
                else colors[idx] = new Color(1f,1f,1f,1f);
            }
            texture_mask.SetPixels(colors);
            texture_mask.Apply();

            //if (scaleSize > 1f)
              //  texture_mask.Resize(Mathf.RoundToInt(texture.width * scaleSize), Mathf.RoundToInt(texture.height * scaleSize));

            if (!highDetailCollider)
            {
                //сторона блока(квадрата), по которому будет высчитываться среднее альфа значение пикселя и либо заливаться белым цветом, либо оставлять как есть
                int area = 10;

                Color[] arr_white = new Color[area * area];
                for (int i = 0; i < area; ++i)
                    for (int j = 0; j < area; ++j)
                    {
                        arr_white[i * area + j] = new Color(1, 1, 1, 1);
                    }


                for (int y = 0; y < texture.height; y += area)
                    for (int x = 0; x < texture.width; x += area)
                    {
                        int w_x = area;
                        int h_y = area;

                        if (x + area >= texture.width)
                            w_x = texture.width - x;

                        if (y + area >= texture.height)
                            h_y = texture.height - y;



                        Color[] cl = /*texture*/texture_mask.GetPixels(x, y, w_x, h_y);
                        float middleAlpha = 0f;
                        for (int i = 0; i < h_y; ++i)
                            for (int j = 0; j < w_x; ++j)
                            {
                                float curAlpha = cl[i * w_x + j].a;
                                middleAlpha += curAlpha; //(curAlpha > 0f ? 1f : 0f); //cl[i * w_x + j].a;
                            }

                        middleAlpha = middleAlpha / w_x * h_y;

                        if (middleAlpha > 0.2f)//0.1f //0 - все пиксели прозрачные, 1 - все пиксели не прозрачные
                        {
                            texture_mask.SetPixels(x, y, w_x, h_y, arr_white);
                        }


                    }


                texture_mask.Apply();
            }
            
            UnityEngine.Sprite sprite_mask = UnityEngine.Sprite.Create(texture_mask,
                                                          new Rect(0.0f, 0.0f, texture_mask.width, texture_mask.height),
                                                          new Vector2(0f,0f),
                                                          1.0f);

            GameObject go = new GameObject();

            UnityEngine.SpriteRenderer renderer = go.AddComponent<UnityEngine.SpriteRenderer>();
            renderer.sprite = sprite_mask;

            UnityEngine.PolygonCollider2D collider = go.AddComponent<UnityEngine.PolygonCollider2D>();
            Contour[] colliderContours = new Contour[collider.pathCount];

            for (int i = 0; i < collider.pathCount; ++i)
            {
                Contour contour = new Contour();

                Vector2[] colliderPnts = collider.GetPath(i);

                if (scaleSize > 1f)
                {
                    
                    float xLeft = 0f, xRight = 0f, yTop = 0f, yBottom = 0f;
                    for (int n = 0; n < colliderPnts.Length; n++)
                    {
                        Vector2 needPoint = colliderPnts[n];
                        
                        needPoint = needPoint * scaleSize; //масштабируем координаты
                        
                        colliderPnts[n] = needPoint;

                        if (n == 0)
                        {
                            xLeft = needPoint.x;
                            xRight = needPoint.x;
                            yBottom = needPoint.y;
                            yTop = needPoint.y;
                        }

                        //сразу записываем габариты
                        if (needPoint.x < xLeft) xLeft = needPoint.x;
                        if (needPoint.x > xRight) xRight = needPoint.x;
                        if (needPoint.y < yBottom) yBottom = needPoint.y;
                        if (needPoint.y > yTop) yTop = needPoint.y;
                    }

                    //определяем габариты
                    float width = xRight - xLeft;
                    float height = yTop - yBottom;

                    //смещаем точки для центрирования
                    for (int n = 0; n < colliderPnts.Length; n++)
                    {
                        Vector2 pt = colliderPnts[n];
                        pt.x = pt.x - (width / 2f) + (width / scaleSize / 2f); // смещение на половину ширины влево + смещение на половину дефолтной ширины вправо
                        pt.y = pt.y - (height / 2f) + (height / scaleSize / 2f );
                        colliderPnts[n] = pt;
                    }
                    

                }

                contour.points = colliderPnts; //collider.GetPath(i);



                colliderContours[i] = contour;
            }
            //renderer.sprite = sprite;

            GameObject.DestroyImmediate(sprite_mask);
            GameObject.DestroyImmediate(texture_mask);
            GameObject.DestroyImmediate(go);
            //UnityEngine.Sprite.DestroyImmediate(sprite);

            return colliderContours;
        }


        /*
        private Contour[] generateColliderHighDetail(Texture2D texture)
        {

            Texture2D texture_mask = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
            Color32[] sourceColors = texture.GetPixels32();
            Color[] colors = new Color[texture.width * texture.height];
            for (int idx = 0; idx < texture.width * texture.height; ++idx)
            {
                if (sourceColors[idx].a == 0) colors[idx] = new Color(0f, 0f, 0f, 0f);
                else colors[idx] = new Color(1f, 1f, 1f, 1f);
            }
            texture_mask.SetPixels(colors);
            texture_mask.Apply();
            
            UnityEngine.Sprite sprite_mask = UnityEngine.Sprite.Create(texture_mask,
                                                          new Rect(0.0f, 0.0f, texture.width, texture.height),
                                                          new Vector2(0f, 0f),
                                                          1.0f);

            GameObject go = new GameObject();

            UnityEngine.SpriteRenderer renderer = go.AddComponent<UnityEngine.SpriteRenderer>();
            renderer.sprite = sprite_mask;

            UnityEngine.PolygonCollider2D collider = go.AddComponent<UnityEngine.PolygonCollider2D>();
            Contour[] colliderContours = new Contour[collider.pathCount];

            for (int i = 0; i < collider.pathCount; ++i)
            {
                Contour contour = new Contour();
                contour.points = collider.GetPath(i);
                colliderContours[i] = contour;
            }
            //renderer.sprite = sprite;

            GameObject.DestroyImmediate(sprite_mask);
            GameObject.DestroyImmediate(texture_mask);
            GameObject.DestroyImmediate(go);
            //UnityEngine.Sprite.DestroyImmediate(sprite);

            return colliderContours;
        }
        */


        public void Parse( PsdDocument psdDocument , string pathToSaveBundle , string pathExtention, TextureImporterType _importType )
        {
            AssetDatabase.Refresh( );

            List<PsdLayer> layers = psdDocument.GetLayers( );
            SpriteCollection spriteCollection = 
								ScriptableObject.CreateInstance<SpriteCollection>( );
            List<SpriteDescription> spriteList = new List<SpriteDescription>( );
            List<string> textureNames = new List<string>( );

            try
            {
                //удаляем ассет с текстурами из Resources/Images/Levels
                if ( System.IO.Directory.Exists( PsdParserUtils.ResourcePath + psdDocument.document.Fullname ) )
                {
                    FileUtil.DeleteFileOrDirectory( PsdParserUtils.ResourcePath + psdDocument.document.Fullname );
                }
            }
            catch ( DirectoryNotFoundException ex )
            {
                Debug.Log( ex );
            }

            try
            {
                //удаляем папку с текстурами из Assets/Editor/Images
                if ( System.IO.Directory.Exists( PsdParserUtils.CommonPath + pathExtention + psdDocument.document.Fullname ) )
                {
                    FileUtil.DeleteFileOrDirectory( PsdParserUtils.CommonPath + pathExtention + psdDocument.document.Fullname );
                }
            }
            catch ( DirectoryNotFoundException ex )
            {
                Debug.Log( ex );
            }

            foreach ( PsdLayer layer in layers )
            {
                if ( layer.Type == LayerType.Image )
                {

                    string assetPath = PsdParserUtils.ImagePathFromLayerFullname( layer.Fullname , pathExtention );

                    //создание текстур
                    System.IO.Directory.CreateDirectory( Path.GetDirectoryName( assetPath ) );

                    Texture2D texture = new Texture2D( ( int )layer.rect.width , ( int )layer.rect.height );
                    textureNames.Add( StringUtils.RemoveFirst( layer.Fullname ) );

                    if ( layer.Data == null || layer.Data.Length == 0 )
                    {
                        Debug.LogError( "Psd layer without image data: " + layer.Fullname );
                    }
                    else
                    {
                        texture.SetPixels32( layer.Data );
                    }

                    //создание файла
                    byte[] content = texture.EncodeToPNG( );
                    File.WriteAllBytes( assetPath , content );

                    //создание инфы для ассета
                    SpriteDescription spriteDescription = new SpriteDescription( );

                    spriteDescription.name = StringUtils.RemoveFirst( layer.Fullname );
                    spriteDescription.sourceTexture = assetPath;
                    spriteDescription.sourceRect = new Rect( 0.0f , 0.0f , texture.width , texture.height );

                    spriteList.Add( spriteDescription );
                    Texture2D.DestroyImmediate( texture );
                }
            }



            //создаем ассет с инфой по текстурам
            spriteCollection.sprites = spriteList;

            if ( forBundle )
            {
                spriteCollection.bundleName = psdDocument.document.Name + "_textures";
            }

            string assetPath1 = PsdParserUtils.ResourcePath + psdDocument.document.Fullname + PsdParserUtils.AssetExtension;
            AssetDatabase.CreateAsset( spriteCollection , assetPath1 );

            string path = PsdParserUtils.CommonPath + pathExtention + psdDocument.document.Fullname;

            AssetDatabase.Refresh( ImportAssetOptions.ForceUpdate );
            List<FileInfo> arr = new List<FileInfo>( );
            Tools.GetFiles( arr , new DirectoryInfo( path ) );

            //импортируем изображения в юнити
            for ( int i = 0 ; i < arr.Count ; ++i )
            {
                string assetPath = arr[ i ].FullName.Substring( arr[ i ].FullName.IndexOf( "Assets" ) ).Replace( "\\" , "/" );


                TextureImporter textureImporter = AssetImporter.GetAtPath( assetPath ) as TextureImporter;
                if ( textureImporter != null )
                {
                    SpritePostprocessor.isReadable = true;
                    
                    SpritePostprocessor.AssignImportSettings( textureImporter , TextureImporterFormat.RGBA32 );
                    AssetDatabase.ImportAsset( assetPath );

                    Texture2D txt = ( Texture2D )AssetDatabase.LoadAssetAtPath( assetPath , typeof( Texture2D ) );

                    if ( txt == null )
                    {
                        Debug.LogError( "========== >>>>>>>>>>>>>>>> txt=null" );
                    }

                    SpritePostprocessor.isReadable = false;
                    TextureImporterFormat format = TextureImporterFormat.RGBA32;
                    if ( !TextureAlphaCannelTest.IsExistAlpha( txt ) )
                    {
                        format = TextureImporterFormat.RGB24;
                       // Debug.LogError( "====================================== RGB24 " + assetPath );
                    }

                    textureImporter.textureType = _importType;
                    SpritePostprocessor.AssignImportSettings(textureImporter, format);
                    AssetDatabase.ImportAsset(assetPath);

                }
                else
                {
                    Debug.LogError( "========================================   textureImporter == null " + assetPath );
                }

            }

            AssetDatabase.SaveAssets( );

            //создаем бандл с текстурами
            if (forBundle)
            {
                List<Texture2D> textures = new List<Texture2D>( );

                for ( int i = 0 ; i < spriteList.Count ; ++i )
                {
                    Texture2D texture = ( Texture2D )AssetDatabase.LoadAssetAtPath(
                        spriteList[ i ].sourceTexture ,
                        typeof( Texture2D ) );
                    if ( texture != null )
                    {
                        textures.Add( texture );
                    }
                    else
                    {
                        Debug.LogError( "==========================texture != null " + spriteList[ i ].sourceTexture );
                    }
                }

                string bundlePath = pathToSaveBundle + spriteCollection.bundleName + ".u3d";                
                Debug.Log( "===================================================" + textures.Count + " " + textureNames.Count + " " + bundlePath );
                BuildPipeline.BuildAssetBundleExplicitAssetNames( textures.ToArray( ) , textureNames.ToArray( ) ,
                                                bundlePath ,
                                                BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.ChunkBasedCompression,
                                                /*BuildTarget.iOS*/EditorUserBuildSettings.activeBuildTarget);
				// build explicitly for WebGL
				int last_slash_index = bundlePath.LastIndexOf("/");
				string webgl_directory = bundlePath.Substring(0, last_slash_index) + "/webgl";
				string bundleWebGLPath = webgl_directory + bundlePath.Substring(last_slash_index);
				if (!Directory.Exists(webgl_directory))
					Directory.CreateDirectory(webgl_directory);
				if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.WebGL)
					File.Copy(bundlePath, bundleWebGLPath,true);
				else
					BuildPipeline.BuildAssetBundleExplicitAssetNames( textures.ToArray( ) , textureNames.ToArray( ) ,
				                                                 bundleWebGLPath,
				                                                 BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.ChunkBasedCompression,
				                                                 BuildTarget.WebGL);


                AssetDatabase.Refresh( );
                Thread.Sleep( 100 );

                string path2 ="";
                if ( bundlePath.Contains( "hogs" ) )
                {
                    path2 = Application.dataPath + "/" + bundlePath.Substring( bundlePath.IndexOf( "hogs" ) );
                    path2 = path2.Replace( "hogs" , "Editor/bundles" );
                }
                if ( bundlePath.Contains( "locations" ) )
                {
                    path2 = Application.dataPath + "/" + bundlePath.Substring( bundlePath.IndexOf( "locations" ) );
                    path2 = path2.Replace( "locations" , "Editor/bundles" );
                }

                Debug.Log( "=====" + path2 );
                BuildPipeline.BuildAssetBundleExplicitAssetNames( textures.ToArray( ) , textureNames.ToArray( ) ,
                                path2 ,
                                BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.UncompressedAssetBundle ,
                                /*BuildTarget.iOS*/EditorUserBuildSettings.activeBuildTarget);

				// build explicitly for WebGL
				last_slash_index = path2.LastIndexOf("/");
				webgl_directory = path2.Substring(0, last_slash_index) + "/webgl";
				bundleWebGLPath = webgl_directory + path2.Substring(last_slash_index);
				if (!Directory.Exists(webgl_directory))
					Directory.CreateDirectory(webgl_directory);
				if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.WebGL)
					File.Copy(path2, bundleWebGLPath,true);
				else
					BuildPipeline.BuildAssetBundleExplicitAssetNames( textures.ToArray( ) , textureNames.ToArray( ) ,
				                                                 bundleWebGLPath,
				                                                 BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.UncompressedAssetBundle ,
				                                                 BuildTarget.WebGL);


            }

            AssetDatabase.Refresh( );

        }




    }
}
