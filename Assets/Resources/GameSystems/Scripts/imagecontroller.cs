﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class imagecontroller : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {
    public void OnPointerEnter(PointerEventData eventData)
    {

        Debug.Log("enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
