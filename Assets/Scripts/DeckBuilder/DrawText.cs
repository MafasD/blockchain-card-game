using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Simple script to show text in UI for few seconds
// Scales the width of the background image to match the text width

public class DrawText : MonoBehaviour
{
    public TMP_Text screenInfo;
    public Image background;

    public void ShowInfoOnScreen(string infoText, float seconds)
    {
        StartCoroutine(ShowScreenInfoText(infoText, seconds));
    }

    private IEnumerator ShowScreenInfoText(string text, float seconds)
    {
        screenInfo.text = text;
        // Scale image width to match with the text
        background.rectTransform.sizeDelta = new Vector2(screenInfo.preferredWidth+20f, 30f);
        background.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        // Hide the info text and image.
        screenInfo.text = string.Empty;
        background.gameObject.SetActive(false);
    }
}
