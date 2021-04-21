/*
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CyberCradle
{
	public enum SearchItemState
	{
		None,
		InActive,
		InQueue,
		OnPanel,
		Found,
		Appearance,
		Disappearance,
	}
	
	public class SearchItem : MonoBehaviour
	{
        public HiddenItem hiddenItem = null;
		public SearchItemState state = SearchItemState.None;
        public SearchMode mode = SearchMode.None;

        //public string ID = "";

        public int ID = 0;

		public static SearchItem Create (SearchModeDesc modeDesc, SearchItemDesc desc, HiddenItemDesc hiddenItemDesc)
		{
			GameObject searchItemGO = new GameObject (desc.name);
			SearchItem searchItem = null;
			switch (modeDesc.searchMode) {
			case SearchMode.Pair:
            case SearchMode.Difference:
			case SearchMode.Silhouette:
            case SearchMode.Group:
				{
					searchItem = searchItemGO.AddComponent<ImageSearchItem> ();
					searchItem.Construct(modeDesc, desc, hiddenItemDesc);
				}
				break;
			case SearchMode.Text:
			case SearchMode.Anagram:
				{
					//searchItem = searchItemGO.AddComponent<TextSearchItem> ();
					searchItem.Construct(modeDesc, desc, hiddenItemDesc);
				}
				break;
			}

            searchItem.ID = searchItem.GetHashCode();//desc.name + searchItem.GetInstanceID();
			//searchItem.transform.parent = SearchPanel.TransformPanel;
            searchItem.mode = modeDesc.searchMode;

			return searchItem;
		}
		
        

        public virtual  int GetID()
        {
            return ID;
        }

		public virtual void Construct(SearchModeDesc modeDesc, SearchItemDesc desc, HiddenItemDesc hiddenItemDesc)
		{
			
		}
			
		public void SetPositionOnPanel (Vector3 position)
		{
			if (transform.localPosition != position) {
				transform.localPosition = position;
			}
		}
		
// 		public void SetState (SearchItemState newState)
// 		{
// 
//             Debug.LogError( "=============================" + newState );
// 
// 			if (newState == state)
//             {
// 				return;
// 			}
// 			
// 			if (newState == SearchItemState.InQueue) {
// 				gameObject.SetActive (false);
//                 SetStateOfHiddenItem (HiddenItemState.OnLocation);
// 			} else if (newState == SearchItemState.OnPanel) {
// 				gameObject.SetActive (true);
//                 SetStateOfHiddenItem (HiddenItemState.Hidden);
// 			} else if (newState == SearchItemState.Appearance) {
// 				gameObject.SetActive (true);
//                 SetStateOfHiddenItem (HiddenItemState.Hidden);
// 				
// 				gameObject.SendMessage ("OnAppearanceStarted");
// 			} else if (newState == SearchItemState.Disappearance) {
// 				gameObject.SetActive (true);
//                 SetStateOfHiddenItem (HiddenItemState.Found);
//                 SearchPanel.OnDisappearanceStarted(ID);
// 
// 				gameObject.SendMessage ("OnDisappearanceStarted");
// 			} else if (newState == SearchItemState.Found) {
// 				gameObject.SetActive (false);
//                 SetStateOfHiddenItem (HiddenItemState.Found);
// 
// 
// 				SearchPanel.TransformPanel.SendMessage ("ReplaceSearchItem", this);
// 			} else if (newState == SearchItemState.InActive) {
// 				gameObject.SetActive (false);
//                 SetStateOfHiddenItem (HiddenItemState.InActive);
// 			}
// 			
// 			state = newState;
// 		}



        public void SetState( SearchItemState newState )
        {

          //  Debug.LogError( "=============================" + newState );

            if ( newState == state )
            {
                return;
            }

            switch ( newState )
            {
                case SearchItemState.InQueue:
                    gameObject.SetActive( false );
                    SetStateOfHiddenItem( HiddenItemState.OnLocation );
                    break;
                case SearchItemState.OnPanel:
                    gameObject.SetActive( false );
                    SetStateOfHiddenItem( HiddenItemState.Hidden );
                    break;

                case SearchItemState.Appearance:
                    gameObject.SetActive( true );
                    SetStateOfHiddenItem( HiddenItemState.Hidden );
                    gameObject.SendMessage( "OnAppearanceStarted" );
                    break;

                case SearchItemState.Disappearance:
                    gameObject.SetActive( true );
                    SetStateOfHiddenItem( HiddenItemState.Found );
                    //SearchPanel.OnDisappearanceStarted( ID );
                    gameObject.SendMessage( "OnDisappearanceStarted" );
                    break;

                case SearchItemState.Found:
                    gameObject.SetActive( false );
                    SetStateOfHiddenItem( HiddenItemState.Found );
                    //SearchPanel.TransformPanel.SendMessage( "ReplaceSearchItem" , this );
                    break;

                case SearchItemState.InActive:
                    gameObject.SetActive( false );
                    SetStateOfHiddenItem( HiddenItemState.InActive );
                    break;
            }
            state = newState;
        }


















        private void SetStateOfHiddenItem (HiddenItemState state)
        {
            //hiddenItem.SetState(state);
        }
		
		public SearchItemState GetState ()
		{
			return state;
		}
	}
}
*/