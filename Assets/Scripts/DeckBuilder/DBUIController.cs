using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace DeckBuilder
{
    public class UIController : UIButtons
    {
        [SerializeField] TMP_Dropdown loadDeckDropdown;
        BasicDropDownListener loadDeckDropDownListener;


        private void Awake()
        {
            SetLoadMenuButtonLock(true);
            loadDeckDropDownListener = new BasicDropDownListener(loadDeckDropdown, GetPlayerDeck);
            
        }

        void GetPlayerDeck(int value)
        {
            if (value <= 0 || value >= 5) // If value is not 1-4
                return;

            LoadPlayerDeckJson(value);
            SetLoadMenuButtonLock(false);
            OnClickCloseLoadMenu();
            Debug.Log(value);
        }

        void LoadPlayerDeckJson(int value)
        {

        }


    }

    public class UIButtons: MonoBehaviour
    {
        public GameObject LoadMenuCanvas;
        bool showLoadUI = false;
        bool isButtonLock = true;

        public void SetLoadMenuButtonLock(bool isButtonLock)
        {
            this.isButtonLock = isButtonLock;

            if (this.isButtonLock)
                OnClickLoadDeck();
        }

        public void OnClickReturn()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void OnClickCloseLoadMenu()
        {
            if (isButtonLock)
                return;

            showLoadUI = !showLoadUI;
            LoadMenuCanvas.SetActive(showLoadUI);
        }

        public void OnClickSaveDeck()
        {

        }

        public void OnClickLoadDeck()
        {
            showLoadUI = !showLoadUI;
            LoadMenuCanvas.SetActive(showLoadUI);
        }
    }

}