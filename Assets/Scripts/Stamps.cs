using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Stamps : Draggable
{
    [Range(1,50)]
    [SerializeField] private int speed;
    [SerializeField] private float DELTA = 0.2f;
    private bool move = true;
    private Vector3 position;

    [SerializeField] public Sprite newSprite; // Sprite to change to on drop
    public Image stampImage; 

    public override void OnEndDrag(PointerEventData eventData)
    {
        isDragged = false;
        if (stampImage != null && position.x > 200 && position.x < 900 && position.y < 1200 && position.y > 100)
        {
            stampImage.sprite = newSprite; // Change the sprite to the new one
            move = false;
        }
    }

    private void MoveTowards(Vector2 destination)
    {
        Vector3 direction = (destination - rectTransform.anchoredPosition).normalized;
        rectTransform.position += direction * speed;
    }

    private void Update()
    {
        if (!isDragged && move)
        {
            if (Vector2.Distance(defaultPosition, rectTransform.anchoredPosition) > DELTA)
                MoveTowards(defaultPosition);
        }
        position= transform.position;
    }
}
