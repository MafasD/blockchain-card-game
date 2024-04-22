using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hides enemy deck when game starts

public class HideDeckAtStart : MonoBehaviour
{
    [SerializeField] GameObject Frontside, Backside;

    private void Awake()
    {
        Frontside.SetActive(false);
        Backside.SetActive(true);
    }
}
