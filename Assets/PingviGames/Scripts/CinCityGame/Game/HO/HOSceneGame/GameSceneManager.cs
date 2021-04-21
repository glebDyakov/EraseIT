using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameSceneManager : MonoBehaviour 
{    
    
    

    public static Camera GameCamera;
    [SerializeField] Camera gameCamera;

    [SerializeField] private int SceneWidth = 512;
    [SerializeField] private int SceneHeight = 512;

    [SerializeField] private GameObject GameLayerGO, AnimationLayerGO;

    //case/image.png объекты
    [SerializeField] private List<GameObject> gameLayerItems = new List<GameObject>();    
    //passive items
    [SerializeField] private List<GameObject> animationLayerItems = new List<GameObject>();
    



    //[SerializeField] private List<HOGameHiddenItem> activeInSessionItems = new List<HOGameHiddenItem>(); //предметы, участвующие в поиске за сессию

    private void Awake()
    {
        GameCamera = gameCamera;
    }

    // Use this for initialization
    void Start () 
    {
		
	}


    [ExecuteInEditMode]
    public void FixTypes()
    {
        GameItem[] items = GameLayerGO.transform.GetComponentsInChildren<GameItem>(true);
        foreach (var item in items)
        {
            if (item.name.ToLower().Contains("erase_wrong") && item.CurType != ItemTypes.EraseWrong)
            {
                item.SetType(ItemTypes.EraseWrong);
                Debug.Log("[Fixed type] name:" + item.name);
            }
            else
            if (item.name.ToLower().Contains("erase_neutral") && item.CurType != ItemTypes.EraseNeutral)
            {
                item.SetType(ItemTypes.EraseNeutral);
                Debug.Log("[Fixed type] name:" + item.name);
            }
        }
    }

    //----------------------------------------------------------
    //editor import methods
    [ExecuteInEditMode]
    public void SetGameLayerItems(List<GameObject> _items)
    {
        gameLayerItems.Clear();
        gameLayerItems.AddRange(_items);        
    }
    
    [ExecuteInEditMode]
    public void SetAnimationLayerItems(List<GameObject> _items)
    {
        animationLayerItems.Clear();
        animationLayerItems.AddRange(_items);
        //hiddenItems = _items;
    }
    

    [ExecuteInEditMode]
    public void SetSceneSize(int w, int h)
    {
        SceneWidth = w;
        SceneHeight = h;

        //initCameraSizeAndPos();
    }

    [ExecuteInEditMode]
    private void initCameraSizeAndPos()
    {
        gameCamera.orthographicSize = SceneHeight / 2f;
        gameCamera.transform.localPosition = new Vector3(SceneWidth/2f,SceneHeight/2f, gameCamera.transform.localPosition.z);
    }
	
}
