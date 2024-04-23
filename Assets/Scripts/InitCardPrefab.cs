using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Script for the card prefab
// elementID determines which type of element the card is
public class InitCardPrefab : MonoBehaviour
{
    [SerializeField] int elementID; // 0 = water, 1 = fire, 2 = leaf
    [SerializeField] bool showInfoText = true; // Text info can be set hidden from the editor
    [SerializeField] GameObject elementObj;
    [SerializeField] Sprite[] elementIcons; // Sprite array of the elements: 0 = water, 1 = fire, 2 = leaf
    [SerializeField] TMP_Text nameTMP, infoTMP; // Card name and info

    private void Awake()
    {
        elementObj.GetComponent<Image>().sprite = elementIcons[elementID]; // Change icon sprite by id
        SetIconInfo();
        
    }

    private void SetIconInfo()
    {
        string name, info;
        if(elementID == 0) // Water element
        {
            name = "Hydro Blast";
            info = "Type: Water\nEffect: Deals +1 water damage";
        }
        else if (elementID == 1) // Fire element
        {
            name = "Firebreath";
            info = "Type: Fire\nEffect: Deals +1 fire damage";
        }
        else // Leaf element
        {
            name = "Leaf Storm";
            info = "Type: Leaf\nEffect: Deals +1 leaf damage";
        }

        nameTMP.text = name;
        infoTMP.text = info;

        infoTMP.gameObject.SetActive(showInfoText); // Hides card info if wanted (test)
    }


    // Get the element id of the card this script is attached to
    public int GetElementID()
    {
        return elementID;
    }

    // Set or change the element id values
    public void SetElement(int id)
    {
        elementID = id;
        elementObj.GetComponent<Image>().sprite = elementIcons[elementID]; // Change icon sprite by id
        SetIconInfo();
    }

    public void SetCardVisibleToOthers()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }

}