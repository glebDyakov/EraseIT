using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HOGameBackground : HOGameBaseItem, IPointerDownHandler {

    public event Action OnPointerDownEvent = null;

    

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("background clicked");
        if (OnPointerDownEvent != null)
            OnPointerDownEvent();
    }
}
