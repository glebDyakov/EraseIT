using System;
using UnityEngine;

namespace CyberCradle
{
	public class PsdParserUtils
	{
		public const string ResourcePath = "Assets/Resources/Images/Levels";


		//public const string CommonPath = "Assets/StreamingAssets/Images/Levels";
        public const string CommonPath = "Assets/Editor/Images/";

		public const string CommonExtension = ".png";
		public const string AssetExtension = ".asset";

		public const float ZStartOffset = 200.0f;
		public const float ZLayerOffset = 5.0f;

		public static float LayerToZ (int layer)
		{
			float z = ZStartOffset + layer * ZLayerOffset;
			return z;
		}

        public static float LayerToZ(int layer, float offset )
        {
            float z = ZStartOffset + layer * offset;
            return z;
        }

		public static Rect CorrectY (PsdDocument psdDocument, Rect rect)
		{
			Rect correctedRect = rect;
			correctedRect.y = psdDocument.document.rect.height - (rect.y + rect.height);
			
			return correctedRect;
		}

		public static Rect CorrectYinGUI (PsdDocument psdDocument, Rect rect)
		{
			Rect correctedRect = rect;
			correctedRect.y = psdDocument.document.rect.height - (rect.y + rect.height);

			float scale = 2.0f / psdDocument.document.rect.height;

			//correctedRect.x -= NGUIManager.Instance.GuiOffset.x;
			//correctedRect.y -= NGUIManager.Instance.GuiOffset.y;

			correctedRect.x *= scale;
			correctedRect.y *= scale;
			correctedRect.width *= scale;
			correctedRect.height *= scale;
			
			return correctedRect;
		}

 		public static string ImagePathFromLayerFullname (string fullname, string exPath )
 		{
            return CommonPath + exPath + fullname + CommonExtension;
 		}
		
		/*
		public static Sprite SpriteFromLayerFullname (PsdDocument psdDocument, string fullname)
		{
			Sprite sprite = new Sprite ();
			
			sprite.name = StringUtils.RemoveFirst(fullname);//.Replace("_dif","");
            sprite.collection = ( ResourcePath + psdDocument.document.Fullname + AssetExtension );//.Replace( "_dif" , "" ); ;
			
			return sprite;
		}
		*/

		public static string UISpriteNameFromLayerFullname (PsdDocument psdDocument, string fullname)
		{
			return StringUtils.RemoveFirst(fullname);
		}
	}
}

