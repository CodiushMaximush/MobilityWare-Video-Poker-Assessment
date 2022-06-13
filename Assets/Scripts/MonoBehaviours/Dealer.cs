
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InterviewSolution
{
    /// <summary>
    /// Monobehaviour representing the dealer at the table
    /// </summary>
    public class Dealer : MonoBehaviour
    {
        [Header("Asset Dependencies")]
        public StandardDeckData deckData;
        public GameObject cardPrefab;
        [Header("Scene Dependencies")]
        public Transform cardsLayoutTransform;

        public Text dealerMessageText;
        public Text currentBalanceText;
        public Text actionButtonText;

        public Button actionButton;

        [Header("Configurable")]
        public int playerBalance;

        private PokerDeck pokerDeck;

        /// <summary>
        /// called by Action Button when the game starts
        /// </summary>
        public void Bet()
        {
            StartGame();
        }
        /// <summary>
        /// called by Action Button when it's time to Redeal
        /// </summary>
        public void Swap()
        {
            ReDeal();
        }
        /// <summary>
        /// Called when game is over to smooth transition into next game
        /// </summary>
        public void Finish()
        {
            ClearAllCards();
            Prompt("All Cleaned Up, Lets Play Again!", "BET");
        }

        /// <summary>
        /// called before first frame update
        /// </summary>
        private void Start()
        {
            //resets label for balance
            UpdateBalance(0);
        }

        /// <summary>
        /// place a card from the deck into the playing area
        /// </summary>
        private void DealCard()
        {
            //draw card
            Card card = pokerDeck.DrawCard();
            //instantiate new card
            GameObject newCardGO = Instantiate(cardPrefab, cardsLayoutTransform);
            //set card's data
            newCardGO.GetComponent<CardUI>().SetCard(card);
        }
        /// <summary>
        /// updates the player's credit balance as well as the UI to reflect it
        /// </summary>
        /// <param name="balance">change in balance</param>
        private void UpdateBalance(int balance)
        {
            playerBalance += balance;
            currentBalanceText.text = playerBalance.ToString();
        }
        /// <summary>
        /// destroys all card prefabs in the play area
        /// </summary>
        private void ClearAllCards()
        {
            foreach (Transform t in cardsLayoutTransform)
            {
                if (t == cardsLayoutTransform) continue;
                Destroy(t.gameObject);
            }
        }
        /// <summary>
        /// updates UI elements to show the current state of the game
        /// </summary>
        /// <param name="message"></param>
        /// <param name="buttonPrompt"></param>
        private void Prompt(string message, string buttonPrompt)
        {
            //set the dealer message to something
            dealerMessageText.text = message;
            //set the action button to something
            actionButtonText.text = buttonPrompt;
        }
        /// <summary>
        /// first step of the game. 
        /// </summary>
        private void StartGame()
        {
            //player pays one credit
            UpdateBalance(-1);
            //get new deck
            pokerDeck = new PokerDeck(deckData);
            //shuffle deck
            pokerDeck.Shuffle();

            //deal 5 cards
            for (int i = 0; i < 5; i++)
            {
                DealCard();
            }
            //check hand
            int potentialWinnings = CheckHand();
            //prompt to reDeal
            if (potentialWinnings > 0)
            {
                Prompt(string.Format("I see a {0}. Maybe we can get something even better", PokerUtils.handValueToTitle[potentialWinnings]),
                    "SWAP");
            }
            else
            {
                Prompt("Select the cards you'd like to hold on to.", "SWAP");
            }
        }
        /// <summary>
        /// second step of the game
        /// </summary>
        private void ReDeal()
        {
            //collect dealt cards
            List<CardUI> dealtCards = new List<CardUI>();
            foreach (Transform t in cardsLayoutTransform)
            {
                if (t == cardsLayoutTransform) continue;
                dealtCards.Add(t.GetComponent<CardUI>());
            }

            //redeal cards not marked as HOLD
            foreach (var card in dealtCards)
            {
                //lock card to prevent unneccary tapping
                card.Lock();
                //don't replace the card if it's held
                if (card.held == true) continue;
                //replace card with new one from deck
                card.SetCard(pokerDeck.DrawCard());
            }

            //detect good hands
            int winnings = CheckHand();
            //prompt to end game
            if (winnings == 0) Prompt("Too bad. Let's try again?", "CLEAR");
            else Prompt(
                string.Format("And that's a {0} for {1} in winnings! Let's try again!", PokerUtils.handValueToTitle[winnings], winnings),
                "CLEAR");

            UpdateBalance(winnings);
        }
        /// <summary>
        /// identifies dealt cards on the table and sends them to be checked
        /// </summary>
        /// <returns>winnings </returns>
        private int CheckHand()
        {
            //card objects to pass into PokerUtils
            List<Card> cards = new List<Card>();
            //ui representation of cards for updating visuals
            List<CardUI> cardUIs = new List<CardUI>();

            //collect cards in playing area
            foreach (Transform t in cardsLayoutTransform)
            {
                if (t == cardsLayoutTransform) continue;
                CardUI cardUI = t.GetComponent<CardUI>();
                cards.Add(cardUI.card);
                cardUIs.Add(cardUI);
            }
            //send the hand to be processed
            PokerUtils.WinningHand winningHand = PokerUtils.CheckHand(cards);

            if (winningHand.winnings > 0)
            {
                //highlight winning cards
                foreach (var cardUI in cardUIs)
                {
                    if (winningHand.relevantCards.Contains(cardUI.card))
                    {
                        cardUI.onCardHighlighted?.Invoke();
                    }
                }

            }
            return winningHand.winnings;
        }
    }
}