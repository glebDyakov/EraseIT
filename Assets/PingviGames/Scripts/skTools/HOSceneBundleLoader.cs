using CyberCradle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOSceneBundleLoader : MonoBehaviour {

    public GameObject rootGO;
    public GameObject hogStartupGO;
    public GameObject AnimationLayer;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

#if UNITY_EDITOR
    [ExecuteInEditMode]
    public void StartInEditor(AssetBundle bundle)
    {
        /*
        
        foreach (SearchItemDesc _item in hogStartupGO.GetComponent<HogStartup>().searchItemDescriptions)
        {
            SearchItemDesc searchItemDesc = _item;
            HiddenItemDesc hiddenItemDesc = _item.hiddenItemOptions[0];

            PassiveItem passiveItem = PassiveItem.Create(
                                          hogStartupGO,
                                          hiddenItemDesc);           
        }

        if (hogStartupGO != null)
            hogStartupGO.GetComponent<HogStartup>().StartInEditor(bundle);

        CyberCradle.SpriteRenderer[] arr = hogStartupGO.GetComponentsInChildren<CyberCradle.SpriteRenderer>();
        foreach (var v in arr)
        {
            v.UpdateInEditor(bundle);
        }
        */

    }
#endif
}
