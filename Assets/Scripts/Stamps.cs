using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

public class Stamps : Draggable
{
    [Range(1,50)]
    [SerializeField] private int speed;
    [SerializeField] private float DELTA = 0.2f;
    
    public void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        
    }

    private void MoveTowards(Vector2 destination)
    {
        Vector3 direction = (destination - rectTransform.anchoredPosition).normalized;
        rectTransform.position += direction * speed;
    }

    private void Update()
    {
        if (!isDragged)
        {
            if (Vector2.Distance(defaultPosition,rectTransform.anchoredPosition) > DELTA)
                MoveTowards(defaultPosition);
        }
    }
}
