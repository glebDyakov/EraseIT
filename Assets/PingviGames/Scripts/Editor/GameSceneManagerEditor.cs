using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(GameSceneManager))]
public class GameSceneManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GameSceneManager myTarget = (GameSceneManager)target;

        DrawDefaultInspector();

        if (GUILayout.Button("FixTypes_Wrong_Neutral"))
        {
            // Get the Prefab Asset root GameObject and its asset path.
            GameObject assetRoot = Selection.activeObject as GameObject;
            string assetPath = AssetDatabase.GetAssetPath(assetRoot);

            // Modify prefab contents and save it back to the Prefab Asset
            using (var editScope = new EditPrefabAssetScope(assetPath))
            {
                editScope.prefabRoot.GetComponent<GameSceneManager>().FixTypes(); //AddComponent<BoxCollider>();
            }

            //myTarget.FixTypes();
            Debug.Log("[FIX Done]");
        }

        

        //myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
        //EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
    }
    public class EditPrefabAssetScope : IDisposable
    {

        public readonly string assetPath;
        public readonly GameObject prefabRoot;

        public EditPrefabAssetScope(string assetPath)
        {
            this.assetPath = assetPath;
            prefabRoot = PrefabUtility.LoadPrefabContents(assetPath);
        }

        public void Dispose()
        {
            PrefabUtility.SaveAsPrefabAsset(prefabRoot, assetPath);
            PrefabUtility.UnloadPrefabContents(prefabRoot);
        }
    }

}