using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using CyberCradle;

namespace CyberCradle
{
	public class ImagePsdParser : IPsdParser
	{
		[DllImport ("PsdParser")]
		private static extern IntPtr PsdLoadImage (string path);

		[DllImport("PsdParser")]
		private static extern void PsdFreeImage (IntPtr psdImage);
		
		[DllImport("PsdParser")]
		private static extern int PsdGetImageWidth (IntPtr psdImage);
		
		[DllImport("PsdParser")]
		private static extern int PsdGetImageHeight (IntPtr psdImage);
		
		[DllImport("PsdParser")]
		private static extern int PsdGetLayerCount (IntPtr psdImage);
		
		[DllImport("PsdParser")]
		private static extern IntPtr PsdGetLayer (IntPtr psdImage, int layerIndex);
		
		[DllImport("PsdParser")]
		private static extern string PsdGetLayerName (IntPtr psdLayer);

		[DllImport("PsdParser")]
		private static extern int PsdGetLayerOpacity (IntPtr psdLayer);

		[DllImport("PsdParser")]
		private static extern int PsdGetLayerFillOpacity (IntPtr psdLayer);
		
		[DllImport("PsdParser")]
		private static extern int PsdGetLayerLeft (IntPtr psdLayer);
		
		[DllImport("PsdParser")]
		private static extern int PsdGetLayerTop (IntPtr psdLayer);
		
		[DllImport("PsdParser")]
		private static extern int PsdGetLayerWidth (IntPtr psdLayer);
		
		[DllImport("PsdParser")]
		private static extern int PsdGetLayerHeight (IntPtr psdLayer);
		
		[DllImport("PsdParser")]
		private static extern int PsdGetLayerDataSize (IntPtr psdLayer);
		
		[DllImport("PsdParser")]
		private static extern int PsdGetLayerData (IntPtr psdLayer, byte[] data);
		
		[DllImport("PsdParser")]
		private static extern bool PsdIsLayer (IntPtr psdLayer);
		
		[DllImport("PsdParser")]
		private static extern bool PsdIsGroup (IntPtr psdLayer);
		
		[DllImport("PsdParser")]
		private static extern bool PsdIsEndOfGroup (IntPtr psdLayer);
		
		private int layerIndex = 0;
		private int imageLayerIndex = 0;

		public string GetDisplayName ()
		{
			return "Image";
		}
		
		public PsdDocument Parse (string fileName)
		{
			PsdDocument psdDocument = ScriptableObject.CreateInstance<PsdDocument> ();
			IntPtr psdImage = PsdLoadImage (fileName);
			
			psdDocument.path = fileName;
			psdDocument.document = new PsdLayer (
										Path.GetFileNameWithoutExtension(fileName), "",
										0, 0,
										PsdGetImageWidth (psdImage), 
										PsdGetImageHeight (psdImage),
										-1);
			psdDocument.document.Type = LayerType.Document;

			ResetLayerIndex (psdImage);
			ResetImageLayerIndex (psdImage);

			Parse (psdImage, psdDocument.document);
			
			PsdFreeImage (psdImage);
			return psdDocument;
		}
		
		private bool LayerIndexIsValid ()
		{
			return layerIndex >= 0;
		}
		
		private void NextLayerIndex ()
		{
			--layerIndex;
		}
		
		private void ResetLayerIndex (IntPtr psdImage)
		{
			layerIndex = PsdGetLayerCount (psdImage) - 1;
		}

		private int NextImageLayerIndex ()
		{
			return ++imageLayerIndex;
		}
		
		private void ResetImageLayerIndex (IntPtr psdImage)
		{
			imageLayerIndex = 0;
		}

		private void ParseImageData (IntPtr psdLayer, PsdLayer layer)
		{
			int dataSize = PsdGetLayerDataSize (psdLayer);
			
			byte[] data = new byte[dataSize];
			Color32[] colors = new Color32[dataSize / 4];
			
			PsdGetLayerData (psdLayer, data);
			
			for (int j = 0; j < layer.Height; ++j) {
				for (int i = 0; i < layer.Width; ++i) {
					
					int fromIndex = j * layer.Width + i;
					int toIndex = ((layer.Height - 1) - j) * layer.Width + i;

                    byte r = data [4 * fromIndex + 2];
                    byte g = data [4 * fromIndex + 1];
                    byte b = data [4 * fromIndex + 0];
                    byte a = data [4 * fromIndex + 3];

                    /*if (a == 0)
                    {
                        r = g = b = 0;
                    }*/
					
					colors [toIndex] = new Color32 (r, g, b, a);
				}
			}
			
			layer.Data = colors;
		}
		
		private void Parse (IntPtr psdImage, PsdLayer parent)
		{
			while (LayerIndexIsValid()) {
				IntPtr psdLayer = PsdGetLayer (psdImage, layerIndex);
		
				if (PsdIsLayer (psdLayer)) {

					PsdLayer layer = new PsdLayer (
									PsdGetLayerName (psdLayer), 
									parent.Fullname,
									PsdGetLayerLeft (psdLayer), 
									PsdGetLayerTop (psdLayer), 
									PsdGetLayerWidth (psdLayer), 
									PsdGetLayerHeight (psdLayer),
									NextImageLayerIndex ());

					layer.Opacity = PsdGetLayerOpacity (psdLayer) * PsdGetLayerFillOpacity (psdLayer) / 255;
					layer.Type = LayerType.Image;

					parent.AddChild (layer);
					ParseImageData (psdLayer, layer);

				} else if (PsdIsGroup (psdLayer)) {

					PsdLayer layer = new PsdLayer (
									PsdGetLayerName (psdLayer), 
	                               	parent.Fullname);
					
					layer.Type = LayerType.Group;
					parent.AddChild (layer);
		
					NextLayerIndex ();
					Parse (psdImage, layer);

				} else if (PsdIsEndOfGroup (psdLayer)) {
					return;
				}
		
				NextLayerIndex ();
			}
		}
	}
}
