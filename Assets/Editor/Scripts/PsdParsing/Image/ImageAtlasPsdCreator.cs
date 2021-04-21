using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;

namespace CyberCradle
{
// 	public class ImageAtlasPsdCreator : IPsdCreator
// 	{
// 		public static List<string> IgnorePatterns = new List<string> {
// 			NGUIPsdCreator.PlaceholderPattern,
// 			NGUIPsdCreator.LabelPattern,
// 			NGUIPsdCreator.LabelPlaceholderPattern,
// 			NGUIPsdCreator.ImagePlaceholderPattern
// 		};
// 		public const float IgnoreMaxSize = 256.0f;
// 		public const float IgnoreMaxXSize = 512.0f;
// 		public const float IgnoreMaxYSize = 512.0f;
// 
// 		public string GetDisplayName ()
// 		{
// 			return "Atlas";
// 		}
// 
// 		public static bool CheckIgnoreSizes (PsdLayer layer)
// 		{
// 			if (layer.Width >= IgnoreMaxSize && layer.Height >= IgnoreMaxSize) {
// 				return false;
// 			}
// 			
// 			if (layer.Width > IgnoreMaxXSize || layer.Height > IgnoreMaxYSize) {
// 				return false;
// 			}
// 
// 			return true;
// 		}
// 
// 		public static bool CheckIgnorePatterns (PsdLayer layer)
// 		{
// 			foreach (string ignorePattern in IgnorePatterns) {
// 				if (StringUtils.CompareWithPattern (ignorePattern, layer.Name)) {
// 					return false;
// 				}
// 			}
// 
// 			return true;
// 		}
// 		
// 		public void Parse (PsdDocument psdDocument, string path)
// 		{
// 			List<PsdLayer> layers = psdDocument.GetLayers ();
// 			List<Texture> textures = new List<Texture> ();
// 			
// 			try {
// 				System.IO.Directory.Delete (PsdParserUtils.ResourcePath + psdDocument.document.Fullname, true);
// 			} catch (DirectoryNotFoundException ex) {
// 				Debug.Log (ex);
// 			}
// 
// 			try {
// 				System.IO.Directory.Delete (PsdParserUtils.CommonPath + psdDocument.document.Fullname, true);
// 			} catch (DirectoryNotFoundException ex) {
// 				Debug.Log (ex);
// 			}
// 
// 			AssetDatabase.Refresh ();
// 
// 			System.IO.Directory.CreateDirectory (PsdParserUtils.CommonPath + psdDocument.document.Fullname);
// 			
// 			foreach (PsdLayer layer in layers) {
// 				if (layer.Type == LayerType.Image) {
// 
// 					Texture2D texture = new Texture2D ((int)layer.rect.width, (int)layer.rect.height);
// 					texture.name = PsdParserUtils.UISpriteNameFromLayerFullname (psdDocument, layer.Fullname);
// 					
// 					if (layer.Data == null || layer.Data.Length == 0) {
// 						Debug.LogError ("Psd layer without image data: " + layer.Fullname);
// 					} else {
// 						texture.SetPixels32 (layer.Data);
// 					}
// 					
// 					if (CheckIgnorePatterns (layer) && CheckIgnoreSizes(layer)) {
// 						textures.Add (texture);
// 					} else if (CheckIgnorePatterns (layer)) {
// 						string assetPath = PsdParserUtils.ImagePathFromLayerFullname (layer.Fullname);
// 						System.IO.Directory.CreateDirectory (Path.GetDirectoryName (assetPath));
// 
// 						byte[] content = texture.EncodeToPNG ();
// 						File.WriteAllBytes (assetPath, content);
// 
// 						Texture2D.DestroyImmediate (texture);
// 					}
// 				}
// 			}
// 
// 			AssetDatabase.ImportAsset (PsdParserUtils.CommonPath + 
// 			                           psdDocument.document.Fullname, 
// 			                           ImportAssetOptions.ForceSynchronousImport | 
// 			                           ImportAssetOptions.ImportRecursive);
// 			
// 			AssetDatabase.SaveAssets ();
// 
// 			string prefabPath = PsdParserUtils.CommonPath + psdDocument.document.Fullname + "/Atlas.prefab";
// 			
// 			if (!string.IsNullOrEmpty (prefabPath)) {
// 				GameObject go = AssetDatabase.LoadAssetAtPath (prefabPath, typeof(GameObject)) as GameObject;
// 				string matPath = Path.ChangeExtension (prefabPath, ".mat");
// 				
// 				// Try to load the material
// 				Material mat = AssetDatabase.LoadAssetAtPath (matPath, typeof(Material)) as Material;
// 				
// 				// If the material doesn't exist, create it
// 				if (mat == null) {
// 					Shader shader = Shader.Find (NGUISettings.atlasPMA ? 
// 					                             "Unlit/Premultiplied Colored" : 
// 					                             "Unlit/Transparent Colored");
// 					mat = new Material (shader);
// 					
// 					// Save the material
// 					AssetDatabase.CreateAsset (mat, matPath);
// 					AssetDatabase.Refresh ();
// 					
// 					// Load the material so it's usable
// 					mat = AssetDatabase.LoadAssetAtPath (matPath, typeof(Material)) as Material;
// 				}
// 				
// 				// Create a new prefab for the atlas
// 				UnityEngine.Object prefab = (go != null) ? go : PrefabUtility.CreateEmptyPrefab (prefabPath);
// 				
// 				// Create a new game object for the atlas
// 				string atlasName = Path.GetFileNameWithoutExtension (prefabPath);
// 
// 				go = new GameObject (atlasName);
// 				go.AddComponent<UIAtlas> ().spriteMaterial = mat;
// 				
// 				// Update the prefab
// 				PrefabUtility.ReplacePrefab (go, prefab);
// 				GameObject.DestroyImmediate (go);
// 				AssetDatabase.SaveAssets ();
// 				AssetDatabase.Refresh ();
// 				
// 				// Select the atlas
// 				go = AssetDatabase.LoadAssetAtPath (prefabPath, typeof(GameObject)) as GameObject;
// 				NGUISettings.atlas = go.GetComponent<UIAtlas> ();
// 				Selection.activeGameObject = go;
// 			}
// 
// 			List<UIAtlasMaker.SpriteEntry> sprites = UIAtlasMaker.CreateSprites (textures);
// 			UIAtlasMaker.UpdateAtlas (NGUISettings.atlas, sprites);
// 
// 			for (int i = 0; i < textures.Count; ++i) {
// 				Texture2D.DestroyImmediate (textures [i]);
// 			}
// 		}
// 	}
}
