using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Vector2 defaultPosition;

    /*
     * Setup rectTransform, canvasGroup and initial position
     */
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        defaultPosition = rectTransform.anchoredPosition;
    }

    /*
     * Fire on click 
     */
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("PointerDown");
    }

    /*
     *  Fire on start dragging
     */
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
    }

    /*
     * Fire on release
     */
    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = defaultPosition;
    }

    /*
     * Fire while dragging
     */
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        Debug.Log("Drag");
    }
}
