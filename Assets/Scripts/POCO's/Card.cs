
using UnityEngine;
namespace InterviewSolution
{
    /// <summary>
    /// An object representing a simple playing card. 
    /// </summary>
    [System.Serializable]
    public class Card
    {
        /// <summary>
        /// the card's numerical value. 11 is read as Jack, 1 as Ace etc...
        /// </summary>
        public int value { get; private set; }
        /// <summary>
        /// the card's suit symbol. traditionally clubs, hearts, etc...
        /// </summary>
        public Suit suit { get; private set; }
        /// <summary>
        /// A sprite to be used when visually representing the card in the game. 
        /// </summary>
        public Sprite sprite { get; private set; }
        /// <summary>
        /// constructor for setting readonly fields
        /// </summary>
        /// <param name="value"></param>
        /// <param name="suit"></param>
        /// <param name="sprite"></param>
        public Card(int value, Suit suit, Sprite sprite)
        {
            this.value = value;
            this.suit = suit;
            this.sprite = sprite;
        }
    }

    public enum Suit
    {
        Hearts, Spades, Clubs, Diamonds
    }
}