using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragDropV2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] GameObject frontSide, backSide;
    GameObject playerField, currentField;
    Vector2 startPosition;
    bool isDragging = false;
    bool isPlaced = false;


    void Awake()
    {
        playerField = GameObject.FindWithTag("PlayerField");
        startPosition = transform.localPosition;
        frontSide.SetActive(true);
        backSide.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isPlaced)
            return;

        if (other.CompareTag("PlayerField"))
            currentField = other.gameObject;

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerField") && currentField != null)
        {
            currentField = null;
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPlaced)
            return;

        else if(MainController.PlayerCardLock)
            return;

        startPosition = transform.localPosition;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPlaced || !isDragging) { return; }

        transform.position = eventData.position;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(isPlaced)
            return;
        
        if (currentField == playerField)
        {
            playerField.GetComponent<FieldHandler>().AddCard(gameObject);
            isPlaced = true;
            frontSide.SetActive(false);
            backSide.SetActive(true);
        }
        else if (currentField == null)
            transform.localPosition = startPosition;
            
        isDragging = false;
    }

}