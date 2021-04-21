using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using CyberCradle;

namespace CyberCradle
{
// 	public class PsdParserWindow : EditorWindow
// 	{
// 		private Texture2D psdTexture = null;
// 		private PsdDocument psdDocument = null;
// 		private Editor psdEditor = null;
// 		private Vector2 scrollPos = Vector2.zero;
// 		private List<IPsdCreator> psdCreators = new List <IPsdCreator> ();
// 		private List<IPsdParser> psdParsers = new List <IPsdParser> ();
// 		private string[] psdParserNames = new string[0];
// 		private string[] psdCreatorNames = new string[0];
// 		private int selectedPsdParserIndex = -1;
// 		private int selectedPsdCreatorIndex = -1;
// 		private bool initialized = false;
// 		
// 		[MenuItem ("Window/Psd Parser")]
// 		public static void Init ()
// 		{
// 			PsdParserWindow psdParserWindow = (PsdParserWindow)
// 								EditorWindow.GetWindow (typeof(PsdParserWindow));
// 			psdParserWindow.title = "Psd Parser";
// 			psdParserWindow.InitInternal ();
// 		}
// 
// 		private void InitInternal ()
// 		{
// 			if (!initialized) {
// 				psdParsers.Add (new ImagePsdParser ());
// 				psdParsers.Add (new AdvancedPsdParser ());
// 
// 				psdCreators.Add (new BackgroundPsdCreator (false, false));
// 				psdCreators.Add (new BackgroundPsdCreator (true, false));
//                 psdCreators.Add (new BackgroundPsdCreator (false, true));
//                 psdCreators.Add (new BackgroundPsdCreator (true, true));
// 				psdCreators.Add (new NGUIPsdCreator ());
// 				psdCreators.Add (new SearchItemPsdCreator ());
// 				psdCreators.Add (new ImagePsdCreator ());
//                 psdCreators.Add (new ImagePsdCreator (true));
// 				psdCreators.Add (new ImageAtlasPsdCreator ());
// 				psdCreators.Add (new MapPsdCreator ());
// 				psdCreators.Add (new LocationPsdCreator ());
// 				psdCreators.Add (new SimpleObjectsPsdCreator ());
// 				
// 				CollectParserNames ();
// 				CollectCreatorNames ();
// 
// 				initialized = false;
// 			}
// 		}
// 
// 		private void CollectParserNames ()
// 		{
// 			List<string> names = new List<string> ();
// 
// 			foreach (IPsdParser parser in psdParsers) {
// 				names.Add (parser.GetDisplayName ());
// 			}
// 
// 			psdParserNames = names.ToArray ();
// 		}
// 
// 		private void CollectCreatorNames ()
// 		{
// 			List<string> names = new List<string> ();
// 			
// 			foreach (IPsdCreator creator in psdCreators) {
// 				names.Add (creator.GetDisplayName ());
// 			}
// 			
// 			psdCreatorNames = names.ToArray ();
// 		}
// 		
// 		public void OnGUI ()
// 		{	
// 			InitInternal ();
// 
// 			Texture2D prevPsdTexture = psdTexture;
// 			psdTexture = (Texture2D)EditorGUILayout.ObjectField (
// 														"Document: ", 
// 														psdTexture, 
// 														typeof(Texture2D), 
// 														false,
// 														GUILayout.ExpandWidth (false));
// 			string path = AssetDatabase.GetAssetPath (psdTexture);
// 
// 			if (prevPsdTexture != psdTexture) {
// 				psdDocument = null;
// 				psdEditor = null;
// 			}
// 
// 			selectedPsdParserIndex = EditorGUILayout.Popup (
// 											"Parsers :",
// 											selectedPsdParserIndex, 
// 											psdParserNames);
// 
// 			if (GUILayout.Button ("Parse", GUILayout.ExpandWidth (false))) {
// 
// 				if (selectedPsdParserIndex >= 0 && !String.IsNullOrEmpty(path)) {
// 					IPsdParser psdParser = psdParsers [selectedPsdParserIndex];
// 
// 					psdDocument = psdParser.Parse (path);
// 					psdEditor = Editor.CreateEditor (psdDocument);
// 				}
// 			}
// 
// 			selectedPsdCreatorIndex = EditorGUILayout.Popup (
// 											"Creators :",
// 											selectedPsdCreatorIndex, 
// 											psdCreatorNames);
// 			
// 			if (GUILayout.Button ("Create", GUILayout.ExpandWidth (false))) 
//             {
// 				
// 
//  				if (selectedPsdCreatorIndex >= 0 && psdDocument != null) {
//  					IPsdCreator psdCreator = psdCreators [selectedPsdCreatorIndex];
//  
//  					psdCreator.Parse (psdDocument,"");
//  				}
// 			}
// 			
// 			if (psdDocument != null) {
// 				scrollPos = GUILayout.BeginScrollView (scrollPos);
// 				OnInspectorGUI (psdEditor);
// 				GUILayout.EndScrollView ();
// 			}
// 		}
// 		
// 		private void OnInspectorGUI (Editor editor)
// 		{
// 			SerializedObject serializedObject = editor.serializedObject;
// 			SerializedProperty iterator = serializedObject.GetIterator ();
// 			
// 			iterator.NextVisible (true);
// 			while (iterator.NextVisible(false)) {
// 				EditorGUILayout.PropertyField (iterator, true);
// 			}
// 			
// 			if (GUI.changed) {
// 				serializedObject.ApplyModifiedProperties ();
// 			}
// 		}
// 	}
}
