using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardVisualScript : MonoBehaviour
{
    [SerializeField] int elementID; // 0 = water, 1 = fire, 2 = leaf
    [SerializeField] GameObject elementsParent;
    [SerializeField] TMP_Text mainTMP, infoTMP;
    bool isCardInitialized = false;

    void Awake()
    {
        if (isCardInitialized) { return ; }

        int childCount = elementsParent.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject child = elementsParent.transform.GetChild(i).gameObject;
            bool isSameIndex = i == elementID;
            child.SetActive(isSameIndex);
        }

        SetIconInfo();
        isCardInitialized = true;
        
    }

    void SetIconInfo()
    {
        string name, info;
        if(elementID == 0) // Water element
        {
            name = "Hydro";
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

        mainTMP.text = name;
        infoTMP.text = info;
    }

    public int GetElementID()
    {
        return elementID;
    }

    public void SetElementID(int id)
    {
        elementID = id;
        isCardInitialized = true;
    }

}
