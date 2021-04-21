using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace CyberCradle
{
    public class SpritePostprocessor : AssetPostprocessor
    {
        public static string[] ImageExtensions = { "png", "jpg" };
        public static bool isReadable = false;
        // for create atlas
        public static bool isIgnore = false;
//         public void OnPreprocessTexture()
//         {
//            // if (isIgnore)
//                return;
//             TextureImporter textureImporter = (TextureImporter)assetImporter;
// 
//             if (textureImporter.assetPath.Contains("Resources/Effects") ||
//                 textureImporter.assetPath.Contains("Resources/Levels/Effects"))
//             {
//                 return;
//             }
// 
//             textureImporter.mipmapEnabled = false;
//             textureImporter.isReadable = isReadable/*false*/;
//             textureImporter.npotScale = TextureImporterNPOTScale.None;
//             textureImporter.textureType = TextureImporterType.Advanced;
//             textureImporter.textureFormat = TextureImporterFormat.ARGB32;
//             textureImporter.wrapMode = TextureWrapMode.Clamp;
//             textureImporter.maxTextureSize = 2048;
//         }
         
        internal static void AssignImportSettings(TextureImporter textureImporter, TextureImporterFormat format )
        {
            if ( textureImporter.assetPath.Contains( "Resources/Effects" ) ||
                textureImporter.assetPath.Contains( "Resources/Levels/Effects" ) )
            {
                return;
            }


            textureImporter.mipmapEnabled = false;
            textureImporter.isReadable = isReadable/*false*/;
            textureImporter.npotScale = TextureImporterNPOTScale.None;
            textureImporter.textureType = TextureImporterType.Default;
            textureImporter.textureFormat = format/* TextureImporterFormat.ARGB32*/;
            textureImporter.wrapMode = TextureWrapMode.Clamp;
            textureImporter.maxTextureSize = 4096;
        }


        [PostProcessScene]
        public static void OnPostprocessScene()
        {
            /*object[] allObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject));

            foreach (GameObject currentObject in allObjects)
            {
                SpriteRenderer spriteRenderer = currentObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    Renderer renderer = currentObject.GetComponent<Renderer>();
                    renderer.material = null;
                }
            }*/
        }

        static public void OnGeneratedCSProjectFiles()
        {
            WtSolution sln = new WtSolution();
            sln.Adjust("Assembly-CSharp.csproj");
            sln.Adjust("Assembly-CSharp-vs.csproj");
            sln.Adjust("Assembly-CSharp-firstpass.csproj");
            sln.Adjust("Assembly-CSharp-firstpass-vs.csproj");
            sln.Adjust("Assembly-CSharp-Editor.csproj");
            sln.Adjust("Assembly-CSharp-Editor-vs.csproj");
            sln.Adjust("Assembly-CSharp-Editor-firstpass.csproj");
            sln.Adjust("Assembly-CSharp-Editor-firstpass-vs.csproj");
        }
    }

    class WtSolution
    {
        XmlDocument doc = null;

        public void Adjust(string fileName)
        {
            try
            {
                string path = Path.GetDirectoryName(Application.dataPath + "..");
                string filePath = path + "/" + fileName;

                if (File.Exists(filePath))
                {

                    TextReader tx = new StreamReader(filePath);
                    if (doc == null)
                        doc = new XmlDocument();
                    doc.Load(tx);
                    tx.Close();

                    if (doc != null && doc.DocumentElement != null)
                    {

                        string xmlns = doc.DocumentElement.Attributes["xmlns"].Value;
                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                        nsmgr.AddNamespace("N", xmlns);

                        if (doc != null && doc.DocumentElement != null)
                        {
                            XmlNode node = doc.SelectSingleNode(
                                "/N:Project/N:PropertyGroup/N:DefineConstants",
                                nsmgr);

                            if (node != null)
                            {
                                XmlNode newNode = doc.CreateNode(
                                    "element",
                                    "AllowUnsafeBlocks",
                                    xmlns);

                                newNode.InnerText = "True";
                                node.ParentNode.AppendChild(newNode);
                            }
                        }

                        if (doc != null && doc.DocumentElement != null)
                        {
                            XmlNode node = doc.SelectSingleNode(
                                "/N:Project/N:PropertyGroup/N:TargetFrameworkVersion",
                                nsmgr);

                            if (node != null)
                            {
                                node.InnerText = "v4.0";
                            }
                        }

                        TextWriter txs = new StreamWriter(filePath);
                        doc.Save(txs);
                        txs.Close();
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }
    }
}
