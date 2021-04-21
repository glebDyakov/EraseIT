using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HOGameBaseItem : MonoBehaviour {

    protected Material defSpriteMat = null;
    protected Material pairSpriteMat = null;

    // Use this for initialization
    protected virtual void Start () {
		
	}


    public virtual void SetMaterials(Material _defSpriteMat, Material _pairSpriteMat)
    {
        defSpriteMat = _defSpriteMat;
        pairSpriteMat = _pairSpriteMat;

        GetComponent<SpriteRenderer>().sharedMaterial = defSpriteMat;
    }

}
