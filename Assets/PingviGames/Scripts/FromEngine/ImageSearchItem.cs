/*using UnityEngine;
using System.Collections;

namespace CyberCradle
{
	public class ImageSearchItem : SearchItem  
	{
		public static float fadeTime = 0.5f;
		public static Vector2 designScale = new Vector2(0.6f, 0.6f);
		public static Vector2 anchor = new Vector2(0.5f, 0.5f);
		public static Color32 silhouetteColor = new Color32 (57, 33, 33, 255);
		public static Color32 borderColor = new Color32 (110, 103, 103, 255);

        //SpriteRenderer spriteRenderer = null;


        public  void ConstructCap(HiddenItemDesc desc )
        {
            //spriteRenderer = gameObject.AddComponent<SpriteRenderer>( );
            //spriteRenderer.Sprite = desc.mainImage;
            //spriteRenderer.Anchor = anchor;
            transform.localScale =  Vector3.one;
        }



		public override void Construct(SearchModeDesc modeDesc, SearchItemDesc desc, HiddenItemDesc hiddenItemDesc)
		{
			//SpriteRenderer spriteRenderer = null;

			switch (modeDesc.searchMode) {
            case SearchMode.Difference:
			case SearchMode.Pair:
            case SearchMode.Group:
				//spriteRenderer = 
					//gameObject.AddComponent<SpriteRenderer> ();
				break;
			case SearchMode.Silhouette:
                    
				break;
			}

			//spriteRenderer.Sprite = hiddenItemDesc.mainImage;
			//spriteRenderer.Anchor = anchor;

			
			transform.localScale =  Vector3.one;
		}

        
		
		public void OnAppearanceStarted()
		{
            

		}
		
		public void OnAppearanceEnded()
		{
            
		}
		
		public void OnDisappearanceStarted()
		{
            
		}
		
		public void OnDisappearanceEnded()
		{
			SetState(SearchItemState.Found);
		}
	}
}
*/