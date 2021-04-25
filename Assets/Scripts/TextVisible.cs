using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextVisible : MonoBehaviour
{
    void Start()
    {
	GetComponent<TMP_SubMesh>().renderer.sortingOrder = 1;        
    }

    void Update()
    {
        
    }
}
