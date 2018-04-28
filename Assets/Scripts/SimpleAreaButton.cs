using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleAreaButton : MonoBehaviour,  IPointerDownHandler, IPointerUpHandler {

	private bool canFire;
	private bool touched;
	private int pointerID;


	private void Awake()
	{
		canFire = false;
		touched = false;
	}

    public void OnPointerDown(PointerEventData eventData)
    {
		if(touched) return;

		canFire = true;
		touched = true;
		pointerID = eventData.pointerId;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
		if(eventData.pointerId != pointerID) return;

		canFire = false;
		touched = false;
    }

	public bool CanFire()
	{
		return canFire;
	}
}
