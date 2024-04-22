using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CrystalOpacity : MonoBehaviour
{
    public float targetOpacity = 0.5f; //This is the opacity value, but it can also be adjusted in the inspector window.

    void Start()
    {
        ChangeOpacity(targetOpacity);
    }

    public void ChangeOpacity(float opacity)
    {
        //This will ensure opacity value is between 0 and 1.
        opacity = Mathf.Clamp01(opacity);

        //Gets all the child images (crystals).
        Image[] childImages = GetComponentsInChildren<Image>();

        //Goes through each child image and changes its opacity.
        foreach (Image childImage in childImages)
        {
            Color currentColor = childImage.color;
            currentColor.a = opacity;
            childImage.color = currentColor;
        }
    }
}
