using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragDropV2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] GameObject Frontside, Backside;
    GameObject PlayerField, CurrentField;
    Vector2 startPosition;
    bool isDragging = false;
    bool isPlaced = false;


    void Awake()
    {
        PlayerField = GameObject.FindWithTag("PlayerField");
        startPosition = transform.localPosition;
        Frontside.SetActive(true);
        Backside.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isPlaced)
            return;

        if (other.CompareTag("PlayerField"))
            CurrentField = other.gameObject;

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerField") && CurrentField != null)
        {
            CurrentField = null;
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPlaced || !MainController.isPlayersTurn)
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
        
        if (CurrentField == PlayerField)
        {
            PlayerField.GetComponent<FieldHandler>().AddCard(gameObject);
            isPlaced = true;
            Frontside.SetActive(false);
            Backside.SetActive(true);
        }
        else if (CurrentField == null)
            transform.localPosition = startPosition;
            
        isDragging = false;
    }

}