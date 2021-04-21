/*
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using CyberCradle;
using System.IO;

[CustomEditor(typeof(HOSceneBundleLoader))]
public class HOSceneBundleLoaderInspector : Editor
{

    private int selectedIndex = 0;
    List<string> fileNames = new List<string>();


    public override void OnInspectorGUI()
    {
        //base.DrawDefaultInspector();

        //List<string> fileNames = new List<string>();
        fileNames.Clear();
        List<FileInfo> arr = new List<FileInfo>();
        CyberCradle.Tools.GetFiles(arr, new DirectoryInfo("Assets/StreamingAssets/bundles/webgl"));
        foreach (FileInfo f in arr)
        {
            if (f.Name.Contains("1024"))
                fileNames.Add(f.Name);
        }


        GUILayout.BeginHorizontal();
        //namefield
        selectedIndex = EditorGUILayout.Popup("Select Bundle", selectedIndex, fileNames.ToArray());
        GUILayout.EndHorizontal();

        string selectedName = fileNames[selectedIndex].Replace("_1024x768.u3d","");

        if (GUILayout.Button("Load HO Scene"))
        {

            TextureManager.Clean();
            HOSceneBundleLoader bundleLoader = target as HOSceneBundleLoader;
            Caching.ClearCache();
            Caching.expirationDelay = 1;           

            string texturespath = Application.dataPath + "/Editor/bundles/" + selectedName + "_1024_768_textures.u3d";
            string scenePath = Application.streamingAssetsPath + "/bundles/webgl/" + selectedName + "_1024x768.u3d";

            Debug.LogError(texturespath);

            if (bundleLoader.rootGO != null) DestroyImmediate(bundleLoader.rootGO);

            AssetBundle sceneBundle = AssetBundle.LoadFromFile(scenePath);
            GameObject go = (GameObject)sceneBundle.mainAsset;
            go.name = scenePath;
            GameObject sceneInstance = Instantiate(go);
            bundleLoader.rootGO = sceneInstance;
            
            sceneInstance.transform.localPosition = Vector3.zero;
            sceneInstance.transform.localScale = Vector3.one;
            sceneBundle.Unload(false);

            HogStartup hs = sceneInstance.GetComponentInChildren<HogStartup>();
            bundleLoader.hogStartupGO = hs.gameObject;

            AssetBundle abTextures = AssetBundle.LoadFromFile(texturespath);

            bundleLoader.StartInEditor(abTextures);
            abTextures.Unload(false);
        }

        if (GUILayout.Button("Create Animation Bundle"))
        {
            HOSceneBundleLoader bundleLoader = target as HOSceneBundleLoader;

            //FIRST need create prefab
            string prefabsFolderPath = FileUtils.GetExternalEditorPath() + "tempPrefabs";
            if (!Directory.Exists(prefabsFolderPath))
            {
                Directory.CreateDirectory(prefabsFolderPath);
            }
            string prefabPath = prefabsFolderPath + "/testAnimationPrefab.prefab"; //editor/external/tempPrefabs/

            if (File.Exists(prefabPath))
                FileUtil.DeleteFileOrDirectory(prefabPath);

            prefabPath = prefabPath.Substring(prefabPath.IndexOf("Assets"));

            //AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            //AssetDatabase.StartAssetEditing();
            GameObject prefabGO = PrefabUtility.CreatePrefab(prefabPath, bundleLoader.AnimationLayer);
            //AssetDatabase.StopAssetEditing();
            //EditorUtility.SetDirty(go);
            //AssetDatabase.SaveAssets();
            //GameObject.DestroyImmediate(root);
            Debug.Log("Temp Prefab created: "+prefabPath);

            //SECOND create bundle from prefab
            string bundleFolderPath = Application.streamingAssetsPath + "/bundles/ANIMATION_BUNDLES";
            if (!Directory.Exists(bundleFolderPath))
            {
                Directory.CreateDirectory(bundleFolderPath);
            }
            string bundlePath = bundleFolderPath + "/"+selectedName+"_animation.u3d"; //StreamingAssets/bundles/ANIMATION_BUNDLES/

            if (File.Exists(bundlePath))
                FileUtil.DeleteFileOrDirectory(bundlePath);

            bundlePath = bundlePath.Substring(bundlePath.IndexOf("Assets"));

            bool rez = BuildPipeline.BuildAssetBundle(prefabGO, null,
                                                      bundlePath,
                                                      BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.ChunkBasedCompression,
                                                      EditorUserBuildSettings.activeBuildTarget);
            if (rez) Debug.Log("Animation Bundle created: " + bundlePath);
            else Debug.LogError("Animation Bundle create error");

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);


        }
        
    }

    public void OnInspectorUpdate()
    {
        Debug.LogError("test");
    }
}
*/