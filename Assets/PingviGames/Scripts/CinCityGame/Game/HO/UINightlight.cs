/* not_used

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINightlight : MonoBehaviour 
{
    [SerializeField] private RectTransform ray;
    [SerializeField] private RectTransform flash;
    [SerializeField] private RectTransform lightRect;

    public float Radius
    {
        get { return (lightRect.sizeDelta.x/2f) * flash.localScale.x; }
    }

    private RectTransform rectTransform;
    private Camera cam;
    private Vector2 rayPosition;

    public Vector2 CurrentPos
    {
        get { return flash.anchoredPosition; }
    }


    private bool canMove = false; 
    public void ActivateFlashLight(bool _activate)
    {
        canMove = _activate;
    }
    
	// Use this for initialization
	void Start () 
    {
        rectTransform = GetComponent<RectTransform>();
        cam = Camera.main;
        rayPosition = (Vector2)ray.localPosition;
        

	}

    public void UpdateRadius(float zoomValue)
    {
        flash.localScale = new Vector3(zoomValue,zoomValue,zoomValue);
    }

    

    private void Update()
    {
        if (!canMove) return;

#if ((UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR)
            //SetPos(_mousePos);
#elif ((UNITY_ANDROID || UNITY_IOS) && UNITY_EDITOR)
            //SetTweenPos(_mousePos);
#else
        Vector3 mousePos = Input.mousePosition;
        SetPos(mousePos);
#endif
    }

    Sequence seq = null;
    public void SetTweenPos(Vector2 newPos)
    {
        if (seq != null && seq.IsPlaying()) seq.Kill();

        Vector2 mousePosUI = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, newPos, cam, out mousePosUI);
        Vector2 rayLength = (mousePosUI - rayPosition)*1.25f;
        float angle = Vector2.Angle(Vector2.up, rayLength);

        seq.Append(ray.DORotate(new Vector3(0f, 0f, angle),0.25f)).
            Join(ray.DOSizeDelta(new Vector2(ray.sizeDelta.x, rayLength.magnitude),0.25f)).
            Join(flash.DOAnchorPos(mousePosUI,0.25f)).
            Play();
        
    }

    public void SetPos(Vector2 newPos)
    {
        Vector2 mousePosUI = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, newPos, cam, out mousePosUI);
        Vector2 rayLength = (mousePosUI - rayPosition)*1.25f;
        float angle = Vector2.Angle(Vector2.up, rayLength);
        ray.eulerAngles = new Vector3(0f, 0f, angle);
        ray.sizeDelta = new Vector2(ray.sizeDelta.x, rayLength.magnitude);
        flash.anchoredPosition = mousePosUI;
    }


    public bool IsPosInFlashLight(Vector2 _Pos)
    {
       

        Vector2 clickPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, _Pos, Camera.main, out clickPos);

        bool res = false;
        if (Vector2.Distance(clickPos, CurrentPos) <= Radius)
        {
            res = true;
        }


        return res;
    }

}


*/