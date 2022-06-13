using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
namespace InterviewSolution
{
    /// <summary>
    ///A quick monobehavior for running tests for poker hand detection. Simply Right click the component and select "Run Tests" to execute the method. 
    /// </summary>
    internal class PokerTests : MonoBehaviour
    {
        [ContextMenu("Run Tests")]
        public void HandCheckingTests()
        {
            List<Card> cards = new List<Card>();
            List<Card> winningCards;
            int passed = 0;
            //test Royal Flush
            cards.Add(new Card(1, Suit.Hearts, null));
            cards.Add(new Card(10, Suit.Hearts, null));
            cards.Add(new Card(11, Suit.Hearts, null));
            cards.Add(new Card(12, Suit.Hearts, null));
            cards.Add(new Card(13, Suit.Hearts, null));

            if (!PokerUtils.CheckRoyalFlush(cards, out winningCards)) Debug.LogError("Check Royal Flush Failed");
            else passed++;
            cards.Clear();

            //test Straight Flush
            cards.Add(new Card(1, Suit.Hearts, null));
            cards.Add(new Card(2, Suit.Hearts, null));
            cards.Add(new Card(3, Suit.Hearts, null));
            cards.Add(new Card(4, Suit.Hearts, null));
            cards.Add(new Card(5, Suit.Hearts, null));

            if (!PokerUtils.CheckStraightFlush(cards, out winningCards)) Debug.LogError("Check Straight Flush Failed");
            else passed++;
            cards.Clear();

            //test 4 of a kind
            cards.Add(new Card(1, Suit.Hearts, null));
            cards.Add(new Card(1, Suit.Spades, null));
            cards.Add(new Card(1, Suit.Clubs, null));
            cards.Add(new Card(1, Suit.Diamonds, null));
            cards.Add(new Card(4, Suit.Hearts, null));

            if (!PokerUtils.CheckFourOfAKind(cards, out winningCards)) Debug.LogError("Check 4 Of a Kind Failed");
            else passed++;
            cards.Clear();

            //test full house
            cards.Add(new Card(1, Suit.Hearts, null));
            cards.Add(new Card(1, Suit.Spades, null));
            cards.Add(new Card(4, Suit.Clubs, null));
            cards.Add(new Card(4, Suit.Diamonds, null));
            cards.Add(new Card(4, Suit.Hearts, null));

            if (!PokerUtils.CheckFullHouse(cards, out winningCards)) Debug.LogError("Check Full House Failed");
            else passed++;
            cards.Clear();

            //test flush
            cards.Add(new Card(1, Suit.Hearts, null));
            cards.Add(new Card(3, Suit.Hearts, null));
            cards.Add(new Card(5, Suit.Hearts, null));
            cards.Add(new Card(7, Suit.Hearts, null));
            cards.Add(new Card(9, Suit.Hearts, null));

            if (!PokerUtils.CheckFlush(cards, out winningCards)) Debug.LogError("Check Flush Failed");
            else passed++;
            cards.Clear();

            //test straight
            cards.Add(new Card(3, Suit.Hearts, null));
            cards.Add(new Card(4, Suit.Diamonds, null));
            cards.Add(new Card(5, Suit.Hearts, null));
            cards.Add(new Card(6, Suit.Spades, null));
            cards.Add(new Card(7, Suit.Clubs, null));

            if (!PokerUtils.CheckStraight(cards, out winningCards)) Debug.LogError("Check Straight Failed");
            else passed++;
            cards.Clear();

            //test 3ok
            cards.Add(new Card(1, Suit.Hearts, null));
            cards.Add(new Card(1, Suit.Spades, null));
            cards.Add(new Card(1, Suit.Clubs, null));
            cards.Add(new Card(4, Suit.Spades, null));
            cards.Add(new Card(5, Suit.Clubs, null));

            if (!PokerUtils.CheckThreeOfAKind(cards, out winningCards)) Debug.LogError("Check 3 Of a Kind Failed");
            else passed++;
            cards.Clear();

            //test 2 pair 
            cards.Add(new Card(1, Suit.Hearts, null));
            cards.Add(new Card(1, Suit.Diamonds, null));
            cards.Add(new Card(2, Suit.Hearts, null));
            cards.Add(new Card(2, Suit.Spades, null));
            cards.Add(new Card(5, Suit.Clubs, null));

            if (!PokerUtils.CheckTwoPair(cards, out winningCards)) Debug.LogError("Check 2 Pair Failed");
            else passed++;
            cards.Clear();

            //test jacks plus
            cards.Add(new Card(11, Suit.Hearts, null));
            cards.Add(new Card(11, Suit.Diamonds, null));
            cards.Add(new Card(2, Suit.Hearts, null));
            cards.Add(new Card(2, Suit.Spades, null));
            cards.Add(new Card(5, Suit.Clubs, null));

            if (!PokerUtils.CheckJacks(cards, out winningCards)) Debug.LogError("Check Jacks or Better Failed");
            else passed++;
            cards.Clear();

            Debug.Log(string.Format("Ran Poker Tests: {0}/9 tests successful", passed));

        }

    }
}
