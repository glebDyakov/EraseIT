/*
using UnityEngine;
using System;
using System.Collections;

namespace CyberCradle
{
	//[RequireComponent(typeof(CyberCradle.SpriteRenderer))]
	public class PassiveItem : MonoBehaviour
	{
		public static Vector2 DefaultAnchor = new Vector2 (0.5f, 0.5f);

		private void CreateShadow (HiddenItemDesc desc)
		{
			if (!String.IsNullOrEmpty(desc.shadowImage.name)) {

				GameObject shadow = new GameObject ("Shadow");
				//SpriteRenderer spriteRenderer = shadow.AddComponent<SpriteRenderer> ();

				//spriteRenderer.Sprite = desc.shadowImage;
				//spriteRenderer.Anchor = DefaultAnchor;

				shadow.transform.parent = transform;
				shadow.transform.localPosition = new Vector3 (
					desc.shadowRect.x + DefaultAnchor.x * desc.shadowRect.width,
					desc.shadowRect.y + DefaultAnchor.y * desc.shadowRect.height,
                    desc.shadowLayer) - transform.localPosition;
			}
		}

        private void CreatePatch (HiddenItemDesc desc)
		{
			if (!String.IsNullOrEmpty(desc.patchImage.name)) {

                GameObject patch = new GameObject ("Patch");
				//SpriteRenderer spriteRenderer = patch.AddComponent<SpriteRenderer> ();

				//spriteRenderer.Sprite = desc.patchImage;
				//spriteRenderer.Anchor = DefaultAnchor;
			
				patch.transform.parent = transform;
				patch.transform.localPosition = new Vector3 (
					desc.patchRect.x + DefaultAnchor.x * desc.patchRect.width,
					desc.patchRect.y + DefaultAnchor.y * desc.patchRect.height,
                    desc.patchLayer) - transform.localPosition;
			}
		}
		
		public static PassiveItem Create (GameObject containerGO, HiddenItemDesc desc)
		{
			GameObject passiveItemGO = new GameObject (desc.name);
			
            PassiveItem passiveItem = passiveItemGO.AddComponent<PassiveItem> ();
			SpriteRenderer spriteRenderer = passiveItemGO.GetComponent<SpriteRenderer> ();
			
			//spriteRenderer.Sprite = desc.mainImage;
			//spriteRenderer.Anchor = DefaultAnchor;
			
            passiveItem.transform.parent = containerGO.transform;
            passiveItem.transform.localPosition = new Vector3 (
										desc.mainRect.x + DefaultAnchor.x * desc.mainRect.width,
										desc.mainRect.y + DefaultAnchor.y * desc.mainRect.height,
										desc.mainLayer);

			passiveItem.CreateShadow (desc);
			passiveItem.CreatePatch (desc);

			return passiveItem;
		}
	}
}
*/