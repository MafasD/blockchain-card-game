using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInitScript : MonoBehaviour
{
    [SerializeField] int elementID; // 0 = water, 1 = fire, 2 = leaf
    [SerializeField] GameObject elementObj;
    [SerializeField] Sprite[] elementIcons; // 0 = water, 1 = fire, 2 = leaf
    [SerializeField] TMP_Text nameTMP, infoTMP; // Card name and info

    void Awake()
    {
        elementObj.GetComponent<Image>().sprite = elementIcons[elementID]; // Change icon sprite by id
        SetIconInfo();
        
    }

    void SetIconInfo()
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
    }

    public int GetElementID()
    {
        return elementID;
    }

    public void SetElementID(int id)
    {
        elementID = id;
    }

}