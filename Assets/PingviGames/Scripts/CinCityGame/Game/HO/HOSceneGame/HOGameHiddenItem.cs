using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HOGameHiddenItem :HOGameBaseItem, IPointerDownHandler {

    public event Action<HOGameHiddenItem> OnItemClickEvent = null;  

    public float Width { get { return _renderer.sprite.textureRect.width; } }
    public float Height { get { return _renderer.sprite.textureRect.height; } }


    public Vector3 CenteredWorldPos
    {
        get
        {
            return new Vector3(transform.position.x + _renderer.sprite.textureRect.width / 2f, transform.position.y + _renderer.sprite.textureRect.height / 2f, transform.position.z);
        }
    }

    public string ItemName { get { return itemName; } }
    [SerializeField] private string itemName = "";

    public int StandardGroup { get { return standardGroup; } }
    [SerializeField] private int standardGroup = -1;

    public int BombGroup { get { return bombGroup; } }
    [SerializeField] private int bombGroup = -1;

    public int SilhouetteGroup { get { return silhouetteGroup; } }
    [SerializeField] private int silhouetteGroup = -1;



    



    public bool IsSearchable
    {
        get { return (_collider!=null && _collider.enabled); }
    }


    public GameObject PatchItemGO { get { return patchItemGO; } }
    public Sprite PopupSprite { get { return popupSprite; } }
        

    [SerializeField] private Sprite popupSprite;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private PolygonCollider2D _collider;

    [SerializeField] private GameObject patchItemGO;
    //[SerializeField] private GameObject imageWrongGO;
    [SerializeField] private GameObject imageCorrectGO;
    [SerializeField] private GameObject circleGO;
    [SerializeField] private Vector3 circleWorldPos = Vector3.zero;

    public Sprite GetSprite()
    {
        return _renderer.sprite;
    }

    public void EnablePairEffect()
    {
        _renderer.sharedMaterial = pairSpriteMat;
    }
    public void DisablePairEffect()
    {
        _renderer.sharedMaterial = defSpriteMat;
    }
        

    //выключим взаимодействие
    public void DisableSearch()
    {
        _collider.enabled = false;
    }
    //включим взаимодействие
    public void EnableSearch()
    {
        _collider.enabled = true;
    }

    //делаем предмет пассивным(в поиске в данную сессию не участвует)
    public void SetPassive()
    {        
        Destroy(_collider);
        _collider = null;
    }

    public void HidePatchImage()
    {
        if (patchItemGO != null)
        {
            patchItemGO.SetActive(false);
            Destroy(patchItemGO);
            patchItemGO = null;
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {        
        Debug.Log("item pointer down:" + itemName);
        if (OnItemClickEvent != null) OnItemClickEvent(this);
    }
    

    [ExecuteInEditMode]
    public void Init(string _name, GameObject _patchItemGO, Sprite _popupSprite, /*GameObject _wrongImgGO,*/ GameObject _correctImgGO, GameObject _circleGO)
    {
        parseName(_name);

        patchItemGO = _patchItemGO;
        popupSprite = _popupSprite;
        _collider = GetComponent<PolygonCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();

        //imageWrongGO = _wrongImgGO;
        imageCorrectGO = _correctImgGO;
        circleGO = _circleGO;

        SpriteRenderer circleSR = circleGO.GetComponent<SpriteRenderer>();
        Rect r = circleSR.sprite.textureRect;
        circleWorldPos = new Vector3(circleGO.transform.position.x + r.width / 2f, circleGO.transform.position.y + r.height / 2f, circleGO.transform.position.z);
        DestroyImmediate(circleSR);


        //_mats = GetComponent<HOGameItemMaterials>();
    }

    [ExecuteInEditMode]
    private void parseName(string _name)
    {
        string[] names = _name.ToLower().Split('/'); //0 - name, 1 - layer(case/popupimage), 2 - type(image, patch)

        if (names[0].IndexOf("_item") > -1) //searchable item
        {
            itemName = names[0].Substring(0, names[0].IndexOf("_item"));

            string typesStr = names[0].Replace(itemName+"_item01_", "");

            string[] typeNames = typesStr.Split('_');
            //смотрим name, может содержать standart, bomb, silhouette
            for (int i = 0; i < typeNames.Length; i++)
            {
                string nm = typeNames[i];
                if (nm.Contains("standart"))
                {
                    string standardStr = nm.Substring(nm.IndexOf("standart")).Replace("standart", "");
                    standardGroup = Convert.ToInt32(standardStr);
                }
                else if (nm.Contains("bomb"))
                {
                    string bombStr = nm.Substring(nm.IndexOf("bomb")).Replace("bomb", "");
                    bombGroup = Convert.ToInt32(bombStr);
                }
                else if (nm.Contains("silhouette"))
                {
                    string silhouetteStr = nm.Substring(nm.IndexOf("silhouette")).Replace("silhouette", "");
                    silhouetteGroup = Convert.ToInt32(silhouetteStr);
                }

            }
        }
        
    }

    
}
