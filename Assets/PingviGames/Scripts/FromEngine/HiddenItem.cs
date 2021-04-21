/*
using UnityEngine;
using System;
using System.Collections;

namespace CyberCradle
{
	public enum HiddenItemState
	{
		None,
		InActive,
		OnLocation,
		Hidden,
		Wait, 
		Disappearance,
		Found,
	}
	
	[RequireComponent(typeof(SpriteRenderer))]
	public class HiddenItem : MonoBehaviour
	{
        static internal event Action OnItemFinded = delegate { };

		public static float MovingTime = 1.5f;
		public static Vector2 DefaultAnchor = new Vector2 (0.5f, 0.5f);
		public static float UnderPanelLayer = 35.0f;
		public static float WaitTime = 2.0f;
		public static float DisappearanceTime = 1.0f;
		public static float DisappearanceScale = 1.5f;
		public static float DisappearanceAlpha = 0.0f;
		public SearchItem searchItem = null;
		public HiddenItemState state = HiddenItemState.None;
		public Transform searchPanel = null;
		public Transform container = null;
		public Vector3 positionOnLocation = Vector3.zero;
		public GameObject shadow = null;
		public GameObject patch = null;
        public GameObject infraredView = null;

        
        private void CreateSkanner (HiddenItemDesc desc)
        {
            infraredView = new GameObject ("Skanner");
            infraredView.SetActive(true);
            SpriteRenderer spriteRenderer = infraredView.AddComponent<SpriteRenderer> ();
            
            spriteRenderer.Sprite = desc.mainImage;
            spriteRenderer.Anchor = DefaultAnchor;
            spriteRenderer.Shader = "Custom/Infrared";
            spriteRenderer.Alpha = 0.0f;
            
            infraredView.transform.parent = transform;
            infraredView.transform.localPosition = Vector3.zero;
            infraredView.transform.localPosition = 
                VectorUtils.Vector3Local(infraredView.transform, UnderPanelLayer - desc.mainLayer);
        }
        
		
		private void CreateShadow (HiddenItemDesc desc)
		{
			if (!String.IsNullOrEmpty(desc.shadowImage.name)) {

				shadow = new GameObject ("Shadow");
				//SpriteRenderer spriteRenderer = shadow.AddComponent<SpriteRenderer> ();

				//spriteRenderer.Sprite = desc.shadowImage;
				//spriteRenderer.Anchor = DefaultAnchor;

				shadow.transform.parent = transform;
				shadow.transform.localPosition = new Vector3 (
					desc.shadowRect.x + DefaultAnchor.x * desc.shadowRect.width,
					desc.shadowRect.y + DefaultAnchor.y * desc.shadowRect.height,
                    desc.shadowLayer) - positionOnLocation;
			}
		}
		

		
		private void CreatePatch (HiddenItemDesc desc)
		{
			if (!String.IsNullOrEmpty(desc.patchImage.name)) {

				patch = new GameObject ("Patch");
				//SpriteRenderer spriteRenderer = patch.AddComponent<SpriteRenderer> ();

				//spriteRenderer.Sprite = desc.patchImage;
				//spriteRenderer.Anchor = DefaultAnchor;
			
				patch.transform.parent = transform;
				patch.transform.localPosition = new Vector3 (
					desc.patchRect.x + DefaultAnchor.x * desc.patchRect.width,
					desc.patchRect.y + DefaultAnchor.y * desc.patchRect.height,
                    desc.patchLayer) - positionOnLocation;
			}
		}
		

		
		private void CreateCollider (HiddenItemDesc desc)
		{

			PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
			collider.pathCount = desc.contours.Length;
			for (int i = 0; i < desc.contours.Length; ++i) 
            {
                if (desc.contours[i].points.Length > 20)
                {
                   Vector2[] tmp = new Vector2[20];
                   Array.Copy(desc.contours[i].points, tmp, 20);
                   desc.contours[i].points = tmp;
                 //   Debug.LogError("=============================================== ERROR !!!!" + desc.name + " " + desc.contours[i].points.Length );
                  //  continue;
                }
				collider.SetPath (i, desc.contours[i].points);
			}
		}
		
		
        public static HiddenItem Create (GameObject containerGO, HiddenItemDesc desc)
		{
			GameObject hiddenItemGO = new GameObject (desc.name);
			
			HiddenItem hiddenItem = hiddenItemGO.AddComponent<HiddenItem> ();
			SpriteRenderer spriteRenderer = hiddenItemGO.GetComponent<SpriteRenderer> ();
			
			spriteRenderer.Sprite = desc.mainImage;
			spriteRenderer.Anchor = DefaultAnchor;
			
			hiddenItem.container = containerGO.transform;
			hiddenItem.searchPanel = SearchPanel.TransformPanel;
			
			hiddenItem.positionOnLocation = new Vector3 (
										desc.mainRect.x + DefaultAnchor.x * desc.mainRect.width,
										desc.mainRect.y + DefaultAnchor.y * desc.mainRect.height,
										desc.mainLayer);
			
			hiddenItem.transform.localPosition = hiddenItem.positionOnLocation;

			hiddenItem.CreateShadow (desc);
			hiddenItem.CreatePatch (desc);
            hiddenItem.CreateSkanner (desc);

			hiddenItem.CreateCollider (desc);
			
			return hiddenItem;
		}
        
		
		protected void SetStateInContainer (bool active)
		{
			gameObject.SetActive (active);
			transform.parent = container;
			transform.localPosition = positionOnLocation;
		}
		
        
		public void SetState (HiddenItemState newState)
		{
            //Debug.LogError( "============================= >>> " + newState );


			if (state == newState)
            {
				return;
			}

			if (newState == HiddenItemState.InActive) {
				SetStateInContainer (false);
			} else if (newState == HiddenItemState.Hidden) {
				SetStateInContainer (true);
			} else if (newState == HiddenItemState.Wait) {
				SetStateInContainer (true);
                transform.localPosition = VectorUtils.Vector3Local (transform, UnderPanelLayer);
				Invoke ("MoveToPanel", WaitTime);
			} else if (newState == HiddenItemState.Disappearance) {

				gameObject.SetActive (true);
                transform.localPosition = VectorUtils.Vector3Local (transform, UnderPanelLayer);

				if (shadow != null)
					shadow.SetActive (false);

				if (patch != null)
					patch.SetActive (false);
				
				if (searchItem != null)
                {
                   
					SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> (); 

					TweenUtils.ScaleTo(transform, DisappearanceTime, DisappearanceScale);
					TweenUtils.AlphaTo(spriteRenderer, DisappearanceTime, DisappearanceAlpha, complete => 
                    {
						searchItem.SetState (SearchItemState.Disappearance);
					});
				}
			} else if (newState == HiddenItemState.Found) {
				SetStateInContainer (false);
			} else if (newState == HiddenItemState.OnLocation) {
				SetStateInContainer (true);
			}
			
			state = newState;
		}
	

		public HiddenItemState GetState ()
		{
			return state;
		}
		

		public void Start ()
		{
		   // gameObject.AddComponent<ClickableObject> ();
		}
		
        
		public void MoveToPanel ()
		{
			if (GetState () == HiddenItemState.Hidden || GetState () == HiddenItemState.Wait) 
            {
                SearchPanel.StartEffect( this );
                SearchPanel.PlaySound( );
				SetState (HiddenItemState.Disappearance);
                OnItemFinded( );
			}
		}
	

		public void OnClick ()
		{
			//MoveToPanel ();
		}
	}
}
*/