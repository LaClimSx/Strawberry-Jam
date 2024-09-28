using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Stamps : Draggable
{
    [Range(1,50)]
    [SerializeField] private int speed;
    [SerializeField] private float DELTA = 0.2f;
    private bool move = true;
    private Vector3 position;
    private DayManager dayManager;

    [SerializeField] public Sprite newSprite; // Sprite to change to on drop
    public Image stampImage;
    public int choice;

    private void Start()
    {
        dayManager = FindObjectOfType<DayManager>();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        isDragged = false;
        if (stampImage != null && position.x > 200 && position.x < 900 && position.y < 1200 && position.y > 100)
        {
            stampImage.sprite = newSprite; // Change the sprite to the new one
            move = false;

            StartCoroutine(WaitAndProceed());
        }
    }

    private IEnumerator WaitAndProceed()
    {
        yield return new WaitForSeconds(1f);

        dayManager.SetChoice(choice); // Set the choice
        SceneManager.LoadScene("DayEnd"); // Load the new scene
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
