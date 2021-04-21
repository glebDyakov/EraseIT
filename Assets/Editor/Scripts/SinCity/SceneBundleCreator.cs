using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SceneManagement;

namespace CyberCradle
{
    public class SceneBundleCreator : EditorWindow
    {
        const string TemplateHogScenePath = "Assets/PingviGames/TemplateScenePrefabs/SceneRoot.prefab";
        const string TemplateHogSceneSpriteItemPath = "Assets/PingviGames/TemplateScenePrefabs/ItemSprite.prefab";
        


        List<FileData> files = new List<FileData>();
        int lastindex = 0;
        List<string> platform_mode = new List<string>();

        List<string> createdScenesResults = new List<string>();

        bool IsHD()
        {
            return false;
        }

        [MenuItem("BUILD/Scene Bundle Creator from PSD")]
        public static void Init()
        {
            Debug.Log("[START WINDOW]");

            SceneBundleCreator hogSceneCreatorWindow = (SceneBundleCreator)
                EditorWindow.GetWindow(typeof(SceneBundleCreator));

            hogSceneCreatorWindow.titleContent = new GUIContent("Scene Creator");
            hogSceneCreatorWindow.SelectRes();
            hogSceneCreatorWindow.InternalInit();
        }

        private void SelectRes()
        {
            List<DirectoryInfo> f_arr = new List<DirectoryInfo>();
            string dataRoot = Application.dataPath.Replace("/Assets", "") + "/Psd_External/";
            Debug.Log("[PSD_PATH] " + dataRoot);

            Tools.GetFolders(f_arr, new DirectoryInfo(dataRoot));

            foreach (var item in f_arr)
            {
                Debug.Log("[FOUND TYPE] " + item.Name);
                platform_mode.Add(item.Name);
            }
        }

        private void InternalInit()
        {
            Debug.Log("[Create new scene...]");
            //новая сцена
            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
            //EditorApplication.NewScene();
            //удалим камеру и освещение
            Camera cam = GameObject.FindObjectOfType<Camera>();
            if (cam !=null) DestroyImmediate(cam.gameObject);
            Light _light = GameObject.FindObjectOfType<Light>();
            if (_light != null) DestroyImmediate(_light.gameObject);
            Debug.Log("[Create new scene...ok]");


            //---------------------------------------------------------------------
            Debug.Log("[Loading psd files...]");
            files = new List<FileData>();
            List<FileInfo> arr = new List<FileInfo>();

            string dataRoot = Application.dataPath.Replace("/Assets", "");


            Tools.GetFiles(arr, new DirectoryInfo(dataRoot + "/Psd_external/" + platform_mode[lastindex] + "/"));



            foreach (var v in arr)
            {
                if (v.Name.Contains("name_") && v.Name.IndexOf("name_") == 0)
                {
                    FileData data = new FileData();
                    data.m_File = v;
                    files.Add(data);
                }
            }

            Debug.Log("[Loading psd files...ok]");

        }


        void DrawListFiles()
        {
            for (int i = 0; i < files.Count; ++i)
            {
                bool tmp = GUILayout.Toggle(files[i].needbuild, files[i].m_File.Name);
                if (tmp != files[i].needbuild)
                {
                    files[i].needbuild = tmp;
                }
            }
        }
        bool DrawResCombo()
        {
            int index = EditorGUILayout.Popup(lastindex, platform_mode.ToArray());
            if (index != lastindex)
            {
                lastindex = index;
                return true;
            }

            return false;
        }

        Vector2 scrollPos = Vector2.zero;
        //float scaleFactor = 1.5f;
        public void OnGUI()
        {

            //NGUIEditorTools.DrawSeparator();
            EditorGUILayout.Separator();

            scrollPos = GUILayout.BeginScrollView(scrollPos);
            GUILayout.BeginVertical();

            bool result = DrawResCombo();
            GUILayout.FlexibleSpace();
            EditorGUILayout.Separator(); 
            DrawListFiles();
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            EditorGUILayout.Separator(); 
            GUILayout.FlexibleSpace();


            //scaleFactor = EditorGUILayout.FloatField("Scale FActor:", scaleFactor);

            if (GUILayout.Button("Generate levels", GUILayout.ExpandWidth(false)))
            {
                buildSelected(false, 1f);
            }
            /*
            if (GUILayout.Button("Generate levels Hi-Det Colliders", GUILayout.ExpandWidth(false)))
            {
                buildSelected(true, scaleFactor);
            }
            */

            if (result)
            {
                InternalInit();
            }

        }

        private void buildSelected(bool generateHighDetailColliders, float scaleFactor)
        {
            createdScenesResults.Clear();

            for (int i = 0; i < files.Count; ++i)
            {
                if (files[i].needbuild)
                {
                    string selectedSceneName = "";

                    AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);


                    //подгрузим префаб-оболочку
                    GameObject levelPrefab = (GameObject)AssetDatabase.LoadMainAssetAtPath(TemplateHogScenePath);
                    GameObject rootSceneGO = (GameObject)Editor.Instantiate(levelPrefab);


                    rootSceneGO.name = files[i].m_File.Name.Replace(files[i].m_File.Extension, "");
                    selectedSceneName = rootSceneGO.name;

                    string locationName = files[i].m_File.Name;//.Substring(0, files[i].m_File.Name.IndexOf("HO"));


                    string exPath = platform_mode[lastindex] + "";


                    GameObject gameLayerGO = rootSceneGO.transform.Find("GameLayer").gameObject;
                    //GameObject backLayerGO = rootSceneGO.transform.Find("BackgroundLayer").gameObject;
                    GameObject animationLayerGO = rootSceneGO.transform.Find("AnimationLayer").gameObject;


                    string pathToSaveBundle = FileUtils.GetPingvigamesPath() + "GameScenes/"+platform_mode[lastindex]+"/" + rootSceneGO.name + "/";
                    if (!Directory.Exists(pathToSaveBundle))
                    {
                        Directory.CreateDirectory(pathToSaveBundle);
                    }

                    ImagePsdParser imagePsdParser = new ImagePsdParser();
                    PsdDocument hogPsdDocument = imagePsdParser.Parse(files[i].m_File.FullName);

                    ImagePsdCreator imagePsdCreator = new ImagePsdCreator(true);

                    //создаем текстуры из псд, создаем файлы картинок, создаем бандл текстур
                    SpriteDescription[] _sprites = imagePsdCreator.ParseSinCityPSDAndCreateColliders(   hogPsdDocument, 
                                                                                                        pathToSaveBundle, 
                                                                                                        rootSceneGO.name, 
                                                                                                        platform_mode[lastindex], 
                                                                                                        TextureImporterType.Sprite, 
                                                                                                        generateHighDetailColliders,
                                                                                                        scaleFactor); 

                    
                    //SearchItemPsdCreator searchItemPsdCreator = new SearchItemPsdCreator(); //создание даты по предметам(координаты расположения)
                    //SearchItemDesc[] searchItems = searchItemPsdCreator.ParseAndCreateData(hogPsdDocument);


                    //создание и инициализация спрайтов в GameObject
                    object[] backAndItems = 
                        createAndInitItems(gameLayerGO, animationLayerGO, _sprites, pathToSaveBundle.Substring(pathToSaveBundle.IndexOf("Assets")) + "Textures/");
                    _sprites = null;

                    //настройки для HogSceneManager
                    GameSceneManager gameSceneManager = rootSceneGO.GetComponent<GameSceneManager>();
                    gameSceneManager.SetSceneSize(hogPsdDocument.document.Width, hogPsdDocument.document.Height);
                    List<GameObject> _gameItems = backAndItems[0] as List<GameObject>;
                    //HOGameBackground _hoGameBack = backAndItems[0] as HOGameBackground;
                    List<GameObject> _animItems = backAndItems[1] as List<GameObject>;
                    //List<HOGameHiddenItemPatch> _patchItems = backAndItems[3] as List<HOGameHiddenItemPatch>;

                    gameSceneManager.SetGameLayerItems(_gameItems);
                    //hogManager.SetBackground(_hoGameBack);
                    gameSceneManager.SetAnimationLayerItems(_animItems);
                    //hogManager.SetHiddenPatchItems(_patchItems);

                    //проверим созданные предметы
                    bool buildSuccess = true;
                    //if (_hoGameBack == null) buildSuccess = false;
                    foreach (var _checkitem in _gameItems)
                        if (_checkitem == null) buildSuccess = false;
                    foreach (var _checkitem in _animItems)
                        if (_checkitem == null) buildSuccess = false;

                    //создадим префаб
                    string prefabPath = pathToSaveBundle + hogPsdDocument.document.name + ".prefab";
                    if (File.Exists(prefabPath))
                        FileUtil.DeleteFileOrDirectory(prefabPath);

                    prefabPath = prefabPath.Substring(prefabPath.IndexOf("Assets"));

                    AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
                    AssetDatabase.StartAssetEditing();
                    
                    GameObject go = PrefabUtility.SaveAsPrefabAsset(rootSceneGO, prefabPath);
                    AssetDatabase.StopAssetEditing();                    
                    AssetDatabase.SaveAssets();

                    foreach (GameObject o in UnityEngine.Object.FindObjectsOfType<GameObject>())
                    {
                        DestroyImmediate(o);
                    }

                    //закинем в список ассетбандлов
                    AssetImporter.GetAtPath(prefabPath).SetAssetBundleNameAndVariant("scenes/" + platform_mode[lastindex] + "/" + hogPsdDocument.document.name, "");

                    Debug.Log("prefab_path:" + prefabPath);

                    string scenebuildres = string.Format("{0}.......{1}", selectedSceneName, (buildSuccess ? "success" : "null_item_exist"));
                    createdScenesResults.Add(scenebuildres);

                }
            }
            string mess = "Build Done!";
            foreach (string buildRes in createdScenesResults)
            {
                mess += "\n" + buildRes;
            }
            Debug.Log(mess);
            
            EditorUtility.DisplayDialog("Done", mess, "OK");
        }

        //сформируем предметы 
        private object[] createAndInitItems(GameObject _gameGO, /*GameObject _backGO,*/ GameObject _animationGO, SpriteDescription[] _sprites, string removeStrFromName)
        {
            //сформированные текстуры            
            
            bool spritesCreated = true;
            if (_sprites == null || _sprites.Length == 0) return null;

            List<GameObject> GameLayerGOs = new List<GameObject>();
            List<GameObject> AnimationLayerGOs = new List<GameObject>();

            List<GameItem> gameItems = new List<GameItem>();
            //HOGameBackground _backItem = null;
            //List<HOGamePassiveItem> _passiveItems = new List<HOGamePassiveItem>();            
            //List<HOGameHiddenItemPatch> _patchItems = new List<HOGameHiddenItemPatch>();
            //List<UnityEngine.Sprite> itemsSprites = new List<UnityEngine.Sprite>();

            Dictionary<string, string> pathnamesDict = new Dictionary<string, string>();

            for (int i = 0; i < _sprites.Length; ++i)
            {
                string txtpath = _sprites[i].sourceTexture.Substring(_sprites[i].sourceTexture.IndexOf("Assets"));
                string txtName = txtpath.Replace(removeStrFromName, "");
                txtName = txtName.Replace("/", ".");

                pathnamesDict.Add(txtName, txtpath);

                UnityEngine.Sprite sprite = (UnityEngine.Sprite)AssetDatabase.LoadAssetAtPath(txtpath, typeof(UnityEngine.Sprite));
                if (sprite != null)
                {

                    sprite.name = txtName;


                    //gamelayer
                    if (txtName.Contains("game."))
                    {
                        GameObject itemPrefab = (GameObject)AssetDatabase.LoadMainAssetAtPath(TemplateHogSceneSpriteItemPath);
                        GameObject itemGO = (GameObject)Editor.Instantiate(itemPrefab);

                        itemGO.layer = 30;
                        itemGO.transform.SetParent(_gameGO.transform);
                        itemGO.name = txtName;
                        itemGO.transform.localPosition = new Vector3(_sprites[i].sourceRect.x+ _sprites[i].sourceRect.width/2f, _sprites[i].sourceRect.y + _sprites[i].sourceRect.height/2f, _sprites[i].Zcoord);
                        itemGO.GetComponent<UnityEngine.SpriteRenderer>().sprite = sprite;

                        GameItem gItem = itemGO.AddComponent<GameItem>(); 
                        gItem.InitInEditor(txtName);
                        gameItems.Add(gItem);

                        GameLayerGOs.Add(itemGO);
                        
                    }
                    else if (txtName.Contains("animation."))
                    {
                        GameObject itemPrefab = (GameObject)AssetDatabase.LoadMainAssetAtPath(TemplateHogSceneSpriteItemPath);
                        GameObject itemGO = (GameObject)Editor.Instantiate(itemPrefab);

                        itemGO.layer = 30;
                        itemGO.transform.SetParent(_animationGO.transform);
                        itemGO.name = txtName;
                        itemGO.transform.localPosition = new Vector3(_sprites[i].sourceRect.x + _sprites[i].sourceRect.width / 2f, _sprites[i].sourceRect.y + _sprites[i].sourceRect.height / 2f, _sprites[i].Zcoord);
                        //new Vector3(_sprites[i].sourceRect.x, _sprites[i].sourceRect.y, _sprites[i].Zcoord);
                        itemGO.GetComponent<UnityEngine.SpriteRenderer>().sprite = sprite;
                        
                        GameItem gItem = itemGO.AddComponent<GameItem>();
                        gItem.InitInEditor(txtName);
                        gameItems.Add(gItem);

                        AnimationLayerGOs.Add(itemGO);
                    }
                    else
                    {
                        spritesCreated = false;
                    }

                    //itemsSprites.Add(sprite);
                }
            }


            //переопределим дубликаты         
            List<GameItem> eraseItems = gameItems.FindAll(item => item.CurType == ItemTypes.EraseCorrect || item.CurType == ItemTypes.EraseWrong || item.CurType == ItemTypes.EraseNeutral);
            //List<GameItem> eraseItems = gameItems.FindAll(item => item.CurType == ItemTypes.EraseWrong);
            List<GameItem> otherItems = gameItems.FindAll(item => item.CurType == ItemTypes.Passive);
            
            Dictionary<string, UnityEngine.Sprite> filteredSpritesDict = new Dictionary<string, UnityEngine.Sprite>();
            
            List<string> namesToDelete = new List<string>();
            
            //текстуры, участвующие в стирании - не удаляем, у них будут уникальные атласы
            foreach (var gItem in eraseItems)
            {
                string itemName = gItem.ItemName;
                UnityEngine.Sprite spr = gItem.CurrentSprite;
                if (!filteredSpritesDict.ContainsKey(itemName))
                {
                    filteredSpritesDict.Add(itemName, spr);
                }
                else
                {
                    namesToDelete.Add(gItem.name);
                }
            }
            foreach (var gItem in otherItems)
            {
                string itemName = gItem.ItemName;
                UnityEngine.Sprite spr = gItem.CurrentSprite;
                if (!filteredSpritesDict.ContainsKey(itemName))
                {
                    filteredSpritesDict.Add(itemName, spr);
                }
                else
                {
                    namesToDelete.Add(gItem.name);
                }
            }

            foreach (var gItem in eraseItems)
            {
                gItem.ReinitWithSprites(filteredSpritesDict[gItem.ItemName]);
            }
            foreach (var gItem in otherItems)
            {
                gItem.ReinitWithSprites(filteredSpritesDict[gItem.ItemName]);
            }


            foreach (var n in namesToDelete)
            {
                //string path = removeStrFromName + n;
                AssetDatabase.DeleteAsset(pathnamesDict[n]);
                
            }

            if (!spritesCreated) return null;

            return new object[2] { GameLayerGOs, AnimationLayerGOs };
        }

        



        
        private void OnInspectorGUI(Editor editor)
        {
            if (editor == null)
            {
                Close();
                return;
            }

            SerializedObject serializedObject = null;
            SerializedProperty iterator = null;

            try
            {
                serializedObject = editor.serializedObject;
                iterator = serializedObject.GetIterator();
            }
            catch
            {
                Close();
            }

            iterator.NextVisible(true);
            while (iterator.NextVisible(false))
            {
                EditorGUILayout.PropertyField(iterator, true);
            }

            if (GUI.changed)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}






