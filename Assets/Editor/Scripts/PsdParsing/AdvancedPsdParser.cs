using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

namespace CyberCradle
{
	public class AdvancedPsdParser : IPsdParser
	{
		public string GetDisplayName ()
		{
			return "Advanced";
		}

		public PsdDocument Parse (string path)
		{
			var proc = new Process {
				StartInfo = new ProcessStartInfo {
					FileName = Application.dataPath + "/../Tools/PsdParser.exe",
					Arguments = Application.dataPath + "/../" + path,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				}
			};
			
			proc.Start ();
			string output = proc.StandardOutput.ReadToEnd ();
			
			if (proc.ExitCode != -1) {
				
				XmlSerializer serializer = new XmlSerializer (typeof(PsdLayer));
				StringReader reader = new StringReader (output);
				
				PsdLayer psdLayer = (PsdLayer)serializer.Deserialize (reader);
				psdLayer.Width = 1024;
				psdLayer.Height = 768;
				psdLayer.Opacity = 255;
				PsdDocument psdDocument = null;
				
				psdDocument = ScriptableObject.CreateInstance<PsdDocument> ();
				psdDocument.path = path;
				psdDocument.document = psdLayer;
				psdDocument.document.Type = LayerType.Document;
				
				return psdDocument;
			} else {
				UnityEngine.Debug.LogError (output);
				return null;
			}
		}
	}
}
