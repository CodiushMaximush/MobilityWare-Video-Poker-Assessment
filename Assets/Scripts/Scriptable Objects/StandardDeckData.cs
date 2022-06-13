
using System;
using System.Collections.Generic;
using UnityEngine;
namespace InterviewSolution
{   /// <summary>
    /// A Scriptable Object Class for containing information about a deck of standard playing cards.  
    /// </summary>
    [CreateAssetMenu(menuName = "CardGames/Deck")]
    public class StandardDeckData : ScriptableObject
    {   /// <summary>
        /// used when generating a path to a specific card's sprite. must be in the same order as the Suits enum.
        /// </summary>

        /// <summary>
        /// an array of card objects
        /// </summary>
        private Card[] cards;
        [Header("Assets should be marked as Sprites and named like 'img_card_d02' for 2 of Diamonds")]
        [SerializeField]
        ///A path from the resources folder to art assets for playing cards
        private string pathToArt;
        [Header("File names should be consistent.")]
        [Header("Example: 'img_card{0}{1}' where {0} is the suit, and {1} is the value")]
        [SerializeField]
        private string cardFileNameFormat; [Header("Symbol for suits in file names. Like 'c' for clubs")]
        [SerializeField]
        private string heartsSymbol;
        [SerializeField]
        private string spadesSymbol;
        [SerializeField]
        private string clubsSymbol;
        [SerializeField]
        private string diamondsSymbol;

        [Header("Symbol for the value of face cards in the file name.  Like 'K' or '13' for King")]
        [SerializeField]
        private string aceSymbol;
        [SerializeField]
        private string jackSymbol;
        [SerializeField]
        private string queenSymbol;
        [SerializeField]
        private string kingSymbol;

        [Header("Card Value Format Modifier like 'D2' for turning '2' into '02'")]
        [SerializeField]
        private string formatModifier;




        /// <summary>
        /// populates the card data with card values, suits, and assigns an appropriate sprite from a given folder of sprites
        /// </summary>
        [ContextMenu("Generate Deck Data")]
        private void GenerateDeck()
        {
            //array suit symbols so we can reference them by index later
            string[] suitSymbols = new string[] { heartsSymbol, spadesSymbol, clubsSymbol, diamondsSymbol };

            cards = new Card[52];
            // for every card in a standard deck of cards
            for (int i = 0; i < cards.Length; i++)
            {

                //determine which suit and value
                Suit suit = (Suit)(i / 13);
                int value = (i % 13) + 1;
                Sprite sprite = null;

                //determine what symbol to use for the card's value in the path
                string printValue = value.ToString(formatModifier);
                if (value == 1) printValue = aceSymbol;
                if (value == 11) printValue = jackSymbol;
                if (value == 12) printValue = queenSymbol;
                if (value == 13) printValue = kingSymbol;

                string path = "";
                try
                {
                    //try to format path
                    path = string.Format(pathToArt + "/" + cardFileNameFormat, suitSymbols[i / 13], printValue);

                }
                catch (Exception)
                {

                    Debug.LogError("There was an error while trying to format the path for " + value + " of " + suit.ToString() + ". Check your inputs for typos.");
                }

                try
                {
                    //try to find sprite
                    sprite = Resources.Load<Sprite>(path);
                }
                catch (Exception)
                {

                    Debug.LogError("Couldn't find sprite for " + path + " please ensure the path is correct and the file is named correctly");
                }

                //set default card to list
                cards[i] = new Card(value, suit, sprite);


            }
        }

        /// <summary>
        /// uses the deck data to generate a copy of a deck of cards suitable for playing a game. 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Card> GetDeck()
        {
            GenerateDeck();
            return cards;
        }


    }



}



