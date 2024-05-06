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
    public class DeckBuilderController : DeckBuilderButtons
    {
        public DeckContentHandler deckContentHandler; // Handler for player's deck
        public TMP_Dropdown loadDeckDropdown; // Unity Dropdown that contain player decks
        public DrawText drawText;
        JsonLoad loadJsonFile;
        JsonSave saveJsonFile;
        readonly DeckCountsData deckCountsData = new();
        readonly float waitTime = 2f; // How many seconds to show info text in UI
        int currentDeck = 0;


        private void Awake()
        {
            SetLoadMenuButtonLock(true);
            BasicDropDownListener listen = new BasicDropDownListener(loadDeckDropdown, GetPlayerDeck);
            loadJsonFile = new JsonLoad();
            saveJsonFile = new JsonSave();
        }

        public void CardWasDeleted()
        {

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

            deckContentHandler.CreateAllCards(cards);

            drawText.ShowInfoOnScreen($"Loaded from deck {currentDeck}", waitTime);
        }


        public override void OnClickSaveDeck()
        {
            int count = deckContentHandler.GetTotalChildCount();

            if (count < deckCountsData.GetMinCardsCount())
            {
                drawText.ShowInfoOnScreen("Not enough cards in your deck", waitTime);
                return;
            }

            CardData[] cardData = deckContentHandler.GetAllCardData();
            saveJsonFile.SaveAllCardData(currentDeck, cardData);
            drawText.ShowInfoOnScreen($"Saved to deck {currentDeck}", waitTime);
        }

        public override void OnClickOpenDataPathFolder()
        {
            string dataPath = Application.persistentDataPath;
            System.Diagnostics.Process.Start(dataPath);
        }
    }

    public class DeckBuilderButtons: MonoBehaviour
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

        public void OnClickLoadDeck()
        {
            showLoadUI = !showLoadUI;
            LoadMenuCanvas.SetActive(showLoadUI);
        }

        public virtual void OnClickSaveDeck() { }

        public virtual void OnClickOpenDataPathFolder() { }
    }

}