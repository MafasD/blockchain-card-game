using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Script for the player and enemy card prefabs.
// elementID determines which type of element the card is.
public class InitCardPrefab : MonoBehaviour
{
    [SerializeField] int elementID; // 0 = water, 1 = fire, 2 = leaf
    [SerializeField] GameObject elementObj; // Child object where the element icon/art is located.
    [SerializeField] Sprite[] elementIcons; // Sprite array of the elements: 0 = water, 1 = fire, 2 = leaf
    [SerializeField] TMP_Text nameTMP, infoTMP; // Card name and info
    [SerializeField] bool showInfoText = true; // Text info can be set hidden from the editor
    [SerializeField] bool isEnemyCard;

    private void Awake()
    {
        elementObj.GetComponent<Image>().sprite = elementIcons[elementID]; // Change icon sprite by id
        transform.GetChild(0).gameObject.SetActive(isEnemyCard); // Backside of the card -> show if is enemy.
        transform.GetChild(1).gameObject.SetActive(!isEnemyCard); // Frontside of the card -> hide if is enemy.

        SetBasicIconInfo(); // Quick demonstration method for the card style.

    }
    // Sets the card information based on the element id.
    private void SetBasicIconInfo()
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
        SetBasicIconInfo();
    }

    public void SetCardVisibleToOthers()
    {
        transform.GetChild(0).gameObject.SetActive(false); // Set backside of the card not visible.
        transform.GetChild(1).gameObject.SetActive(true); // Set frontside of the card visible.
    }

}