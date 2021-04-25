using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
	public List<GameObject> texts;

    void Start()
    {
	InvokeRepeating("ShowTexts", 1f, 1f);
    }

    public void ShowTexts()
    {
	if(texts[0].transform.childCount >= 1){
		foreach(var text in texts){
			text.transform.GetChild(0).gameObject.AddComponent<TextVisible>();
		}
		CancelInvoke();
	}
    }
}
