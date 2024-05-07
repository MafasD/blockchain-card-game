using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Card data model used when loading a player's deck in JSON format.

[Serializable]
public class CardData
{
    public int cardID;
    public int element;
    public bool isNFT; // Not implemented (can be changed)
    public Sprite sprite; // Not implemented (can be changed)

}
