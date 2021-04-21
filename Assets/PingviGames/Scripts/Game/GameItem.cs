using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes
{ 
    EraseCorrect = 0,
    EraseWrong = 1,
    Passive = 2,
    CheckMarkMarker = 3,
    ParticleMarker = 4,
    EraseNeutral = 5,
}

public class GameItem : MonoBehaviour
{

    [SerializeField] private ItemTypes itemType = ItemTypes.Passive;
    [SerializeField] private string itemName = string.Empty;
    [SerializeField] private bool isText = false;
    [SerializeField] private string lang = string.Empty;
    [SerializeField] private int stepNum = 0;

    public Sprite CurrentSprite => GetComponent<SpriteRenderer>().sprite;
    public ItemTypes CurType => itemType;
    public string ItemName => itemName;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    [ExecuteInEditMode]
    public void SetType(ItemTypes newType)
    {
        itemType = newType;
    }

    [ExecuteInEditMode]
    public void ReinitWithSprites(Sprite newSprite)
    {
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    [ExecuteInEditMode]
    public void InitInEditor(string nameToParse)
    {
        string[] fullnames = nameToParse.ToLower().Split('.');
        string name = fullnames[fullnames.Length - 2]; //.Replace(".png",string.Empty);

        //checkmark
        if (name.ToLower().Contains("checkmark_"))
        {
            itemName = name.Split('_')[1];
            itemType = ItemTypes.CheckMarkMarker;
            var comp = GetComponent<SpriteRenderer>();
            DestroyImmediate(comp);
        }
        else
        //particleplace
        if (name.ToLower().Contains("particleplace_"))
        {
            itemName = name.Split('_')[1];
            itemType = ItemTypes.ParticleMarker;
            var comp = GetComponent<SpriteRenderer>();
            DestroyImmediate(comp);
        }
        else
        //erase neutral
        if (name.ToLower().Contains("erase_neutral"))
        {
            string[] names = name.Split('_');
            itemName = names[0];
            itemType = ItemTypes.EraseNeutral;
        }
        else
        //erase wrong
        if (name.ToLower().Contains("erase_wrong"))
        {
            string[] names = name.Split('_');
            itemName = names[0];
            itemType = ItemTypes.EraseWrong;

            if (name.Contains("_text["))
            {
                int startindx = name.IndexOf("text[");
                int endindx = name.IndexOf("]");
                string textpart = name.Substring(startindx, endindx - startindx);
                isText = true;
                lang = textpart.Replace("text[", string.Empty).Replace("]", string.Empty);
                itemName += "_" + lang;
            }

        }
        else
        //erase correct
        if (name.ToLower().Contains("erase_correct"))
        {
            string[] names = name.Split('_');
            itemName = names[0];
            itemType = ItemTypes.EraseCorrect;

            for (int i = 0; i < names.Length; i++)
            {
                string part = names[i];
                if (part.IndexOf("step") == 0)
                {
                    stepNum = Convert.ToInt32(part.Replace("step", string.Empty));
                }                
            }

            if (name.Contains("_text["))
            {
                int startindx = name.IndexOf("text[");
                int endindx = name.IndexOf("]");
                string textpart = name.Substring(startindx, endindx - startindx);
                isText = true;
                lang = textpart.Replace("text[", string.Empty).Replace("]", string.Empty);
                itemName += "_" + lang;
            }

            /*
            if (name.ToLower().Contains("_step"))
            {
                stepNum = Convert.ToInt32(names[names.Length - 2].Replace("step", ""));
            }
            */
        }
        //passive
        else
        {
            itemName = name.Split('_')[0];
            itemType = ItemTypes.Passive;
        }
    }
    
}
