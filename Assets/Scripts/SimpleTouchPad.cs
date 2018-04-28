using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	public float smoothingFactor;

	private Vector2 origin;
	private Vector2 direction;
	private Vector2 smoothingDirection;

	private bool touched;
	private int pointerID;


	private void Awake()
	{
		direction = Vector2.zero;
		touched = false;
	}

    public void OnPointerDown(PointerEventData eventData)
    {
		if(touched) return;

        origin = eventData.position;
		touched = true;
		pointerID = eventData.pointerId;
    }

    public void OnDrag(PointerEventData eventData)
    {
		if(eventData.pointerId != pointerID) return;

        Vector2 currentPosition = eventData.position;
		Vector2 directionRaw = currentPosition - origin;
		direction = directionRaw.normalized;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
		if(eventData.pointerId != pointerID) return;

        direction = Vector3.zero;
		touched = false;
    }

	public Vector2 GetDirection()
	{
		smoothingDirection = Vector2.MoveTowards(smoothingDirection, direction, smoothingFactor);
		return smoothingDirection;
	}
}
