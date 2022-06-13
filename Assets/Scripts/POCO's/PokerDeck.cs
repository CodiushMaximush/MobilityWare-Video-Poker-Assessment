
using System.Collections.Generic;

using System.Linq;
namespace InterviewSolution
{
    /// <summary>
    /// object representing a deck of playing cards for poker.
    /// </summary>
    public class PokerDeck
    {
        /// <summary>
        /// random member for shuffling RNG
        /// </summary>
        private static System.Random random = new System.Random();
        /// <summary>
        /// gets a copy of the deck from the deck data. 
        /// </summary>
        /// <param name="deckData">Scriptable Object representing a deck of standard playing cards</param>
        public PokerDeck(StandardDeckData deckData)
        {
            cards = deckData.GetDeck().ToList();
        }

        /// <summary>
        /// a list of cards representing the deck. 
        /// </summary>
        public List<Card> cards { get; private set; }
        /// <summary>
        /// draws a card from the deck
        /// </summary>
        /// <returns>a card, or null if deck is empty</returns>
        public Card DrawCard()
        {
            if (cards.Count <= 0) return null;
            else
            {
                Card drawnCard = cards[0];
                cards.RemoveAt(0);
                return drawnCard;
            }
        }


        /// <summary>
        /// Modified fisher-yates shuffle from dotnetperls.com Shuffles the deck of cards in a realistic way. 
        /// </summary>
        public void Shuffle()
        {
            int n = cards.Count;
            for (int i = 0; i < (n - 1); i++)
            {
                int r = i + random.Next(n - i);
                Card c = cards[r];
                cards[r] = cards[i];
                cards[i] = c;
            }
        }


    }

}

