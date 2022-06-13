using System;
using System.Collections.Generic;
using System.Linq;


namespace InterviewSolution
{
    /// <summary>
    /// class for detecting winning hands and scoring them accordingly. 
    /// </summary>
    internal class PokerUtils
    {
        /// <summary>
        /// nested class representing a winning poker hand
        /// </summary>
        public class WinningHand
        {
            /// <summary>
            /// amount won by the player
            /// </summary>
            public int winnings { get; private set; }
            /// <summary>
            /// title of the hand for display purposes
            /// </summary>
            public string handTitle { get; private set; }
            /// <summary>
            /// the cards that make this hand a winning hand. 
            /// </summary>
            public List<Card> relevantCards { get; private set; }
            /// <summary>
            /// constructor for setting readonly fields
            /// </summary>
            /// <param name="winnings"></param>
            /// <param name="handTitle"></param>
            /// <param name="relevantCards"></param>
            public WinningHand(int winnings, string handTitle, List<Card> relevantCards)
            {

                this.winnings = winnings;
                this.handTitle = handTitle;
                this.relevantCards = relevantCards;
            }
        }

        /// <summary>
        /// a table of winning hand's values and titles
        /// </summary>
        public static readonly Dictionary<int, string> handValueToTitle = new Dictionary<int, string> {
            {800, "Royal Flush"},
            {50, "Straight Flush"},
            {25, "Four of a Kind"},
            {9, "Full House"},
            {6, "Flush"},
            {4, "Straight"},
            {3, "Three of a Kind"},
            {2, "Two Pair"},
            {1, "Jacks or Better"}
        };

        /// <summary>
        /// Checks for each winning hand from biggest to smallest, ending early if a winning hand is found
        /// </summary>
        /// <param name="cards">list of cards representing the player's hand</param>
        /// <returns>an integer representing the amount won from the hand</returns>
        public static WinningHand CheckHand(List<Card> cards)
        {
            List<Card> winningCards = new List<Card>();
            if (CheckRoyalFlush(cards, out winningCards)) return new WinningHand(800, handValueToTitle[800], winningCards);
            if (CheckStraightFlush(cards, out winningCards)) return new WinningHand(50, handValueToTitle[50], winningCards);
            if (CheckFourOfAKind(cards, out winningCards)) return new WinningHand(25, handValueToTitle[25], winningCards);
            if (CheckFullHouse(cards, out winningCards)) return new WinningHand(9, handValueToTitle[9], winningCards);
            if (CheckFlush(cards, out winningCards)) return new WinningHand(6, handValueToTitle[6], winningCards);
            if (CheckStraight(cards, out winningCards)) return new WinningHand(4, handValueToTitle[4], winningCards);
            if (CheckThreeOfAKind(cards, out winningCards)) return new WinningHand(3, handValueToTitle[3], winningCards);
            if (CheckTwoPair(cards, out winningCards)) return new WinningHand(2, handValueToTitle[2], winningCards);
            if (CheckJacks(cards, out winningCards)) return new WinningHand(1, handValueToTitle[1], winningCards);


            return new WinningHand(0, "Nothing", null);
        }

        /*
         * A bunch of methods for checking different poker hands. 
         */


        public static bool CheckRoyalFlush(List<Card> hand, out List<Card> winningCards)
        {
            //set winningCards to null since it's required to be set in every code path by the compiler.
            winningCards = null;
            //cards should all be the same suit
            Suit suit = hand[0].suit;
            if (hand.Where(c => c.suit != suit).Count() > 0) return false;
            //hand should have an ace
            if (hand.Where(c => c.value == 1).Count() != 1) return false;
            //hand should have a king
            if (hand.Where(c => c.value == 13).Count() != 1) return false;
            //hand should have a queen
            if (hand.Where(c => c.value == 12).Count() != 1) return false;
            //hand should have a jack
            if (hand.Where(c => c.value == 11).Count() != 1) return false;
            //hand should have a 10
            if (hand.Where(c => c.value == 10).Count() != 1) return false;
            //in this case the winning cards will always be the whole hand. 
            winningCards = hand;
            return true;
        }

        public static bool CheckStraightFlush(List<Card> hand, out List<Card> winningCards)
        {
            winningCards = null;
            //cards should all be the same suit
            Suit suit = hand[0].suit;
            if (hand.Where(c => c.suit != suit).Count() > 0) return false;
            //cards should be a straight
            hand = hand.OrderBy(c => c.value).ToList();
            for (int i = 1; i < hand.Count; i++)
            {   //each card should have one more value than the one before it, no wraparounds I guess. 
                if (hand[i].value != hand[i - 1].value + 1) return false;
            }

            winningCards = hand;
            return true;
        }

        public static bool CheckFourOfAKind(List<Card> hand, out List<Card> winningCards)
        {
            winningCards = null;
            //group cards by value, then select groups where there are more than 4 of a value
            var grouping = hand.GroupBy(c => c.value).Where(d => d.Count() == 4);
            //if we didn't find any, we can end early. 
            if (grouping.Count() == 0) return false;

            winningCards = grouping.FirstOrDefault().ToList();

            return true;
        }

        public static bool CheckFullHouse(List<Card> hand, out List<Card> winningCards)
        {
            winningCards = null;
            //probably the weirdest section of code I've written in a long time

            //group by value, then sort descending by frequency
            var groups = hand.GroupBy(c => c.value).OrderByDescending(g => g.Count()).ToArray();
            //if there's not two groups of values, stop
            if (groups.Count() != 2) return false;
            //if the first group doesn't have 3 cards, or the second doesn't have 2 cards, stop
            if (groups[0].Count() != 3 || groups[1].Count() != 2) return false;

            //if we made it here we have a full house

            //add winning cards
            winningCards = new List<Card>();
            winningCards.AddRange(groups[0].ToList());
            winningCards.AddRange(groups[1].ToList());


            return true;
        }

        public static bool CheckFlush(List<Card> hand, out List<Card> winningCards)
        {
            winningCards = null;
            //if there aren't 5 cards of the same suit
            if (hand.GroupBy(c => c.suit).Where(d => d.Count() == 5).Count() == 0) return false;
            winningCards = hand;
            return true;
        }
        public static bool CheckStraight(List<Card> hand, out List<Card> winningCards)
        {
            winningCards = null;
            //sort by value
            hand = hand.OrderBy(c => c.value).ToList();
            for (int i = 1; i < hand.Count; i++)
            {   //each card should have one more value than the one before it, no wraparounds I guess. 
                if (hand[i].value != hand[i - 1].value + 1) return false;
            }
            winningCards = hand;
            return true;
        }

        public static bool CheckThreeOfAKind(List<Card> hand, out List<Card> winningCards)
        {
            winningCards = null;
            //group hand by value but only select groups with 3 cards in them
            var grouping = hand.GroupBy(c => c.value).Where(d => d.Count() == 3);
            //if we didn't actually select any, we didn't find a 3OaK
            if (grouping.Count() == 0) return false;
            winningCards = grouping.First().ToList();
            return true;
        }

        public static bool CheckTwoPair(List<Card> hand, out List<Card> winningCards)
        {
            winningCards = null;
            //group the hand by value, then select groups where there are two cards. 
            var grouping = hand.GroupBy(c => c.value).Where(d => d.Count() == 2);
            //if there aren't two groups, we didnt' find the hand. 
            if (grouping.Count() != 2) return false;

            winningCards = grouping.SelectMany(g => g.ToList()).ToList();

            return true;
        }

        public static bool CheckJacks(List<Card> hand, out List<Card> winningCards)
        {
            winningCards = null;
            //group the hand by value where the value is Jacks or Better and the group size is 2
            var grouping = hand.Where(c => c.value >= 11).GroupBy(c => c.value).Where(g => g.Count() == 2);
            //if there isn't exactly one group, we don't have the hand
            if (grouping.Count() != 1) return false;
            winningCards = grouping.First().ToList();
            return true;
        }

    }
}
