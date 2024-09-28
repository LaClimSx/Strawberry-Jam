using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    [HideInInspector] public RectTransform rectTransform;
    
    private CanvasGroup canvasGroup;

    [HideInInspector] public Vector2 defaultPosition;
    [HideInInspector] public bool isDragged;

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
    { }

    /*
     *  Fire on start dragging
     */
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragged = true;
    }

    /*
     * Fire on release
     */
    public abstract void OnEndDrag(PointerEventData eventData);

    /*
     * Fire while dragging
     */
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
