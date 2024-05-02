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
        [SerializeField] DeckContentHandler deckContentHandler;
        [SerializeField] TMP_Dropdown loadDeckDropdown;
        BasicDropDownListener loadDeckDropDownListener;
        LoadJsonFile loadJsonFile;
        SaveJsonFile saveJsonFile;
        
        int currentDeck = 0;


        private void Awake()
        {
            SetLoadMenuButtonLock(true);
            loadDeckDropDownListener = new BasicDropDownListener(loadDeckDropdown, GetPlayerDeck);
            loadJsonFile = new LoadJsonFile();
            saveJsonFile = new SaveJsonFile();
        }

        void GetPlayerDeck(int value)
        {
            if (value <= 0 || value >= 5) // If value is not 1-4
                return;

            LoadPlayerDeckByIndex(value);
            SetLoadMenuButtonLock(false);
            OnClickCloseLoadMenu();
            
        }

        void LoadPlayerDeckByIndex(int value)
        {
            currentDeck = value;
            CardData[] cards = loadJsonFile.LoadCardsData(value);
            if (cards != null)
                deckContentHandler.CreateAllCards(cards);
            else
                deckContentHandler.DeleteAllCards();
        }

        public override void OnClickSaveDeck()
        {
            Debug.Log("Saving");
            CardData[] cardData = deckContentHandler.GetAllCardData();
            saveJsonFile.SaveAllCardData(currentDeck, cardData);
        }

        public override void OnClickOpenDataPathFolder()
        {
            string dataPath = Application.persistentDataPath;
            System.Diagnostics.Process.Start(dataPath);
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

        public virtual void OnClickSaveDeck()
        {

        }

        public void OnClickLoadDeck()
        {
            showLoadUI = !showLoadUI;
            LoadMenuCanvas.SetActive(showLoadUI);
        }

        public virtual void OnClickOpenDataPathFolder()
        {

        }
    }

}