
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace InterviewSolution
{
    /// <summary>
    /// monobehaviour responsible for managing the UI for a playing card
    /// </summary>
    public class CardUI : MonoBehaviour
    {
        [Header("Scene Dependencies")]
        public GameObject heldIndicator;
        public Button button;
        public UnityEvent onCardChanged;
        public UnityEvent onCardHighlighted;

        /// <summary>
        /// current card data
        /// </summary>
        public Card card { get; private set; }
        /// <summary>
        /// is card being 'held' by the player during the redeal
        /// </summary>
        public bool held { get; private set; }
        /// <summary>
        /// should this card be able to be interacted with right now?
        /// </summary>
        private bool locked = false;

        /// <summary>
        /// update card data, invoke a UnityEvent for animations and such
        /// </summary>
        /// <param name="card"></param>
        public void SetCard(Card card)
        {
            this.card = card;
            button.image.sprite = this.card.sprite;
            onCardChanged?.Invoke();

        }
        /// <summary>
        /// set card to locked and disabled held indicator
        /// </summary>
        public void Lock()
        {
            locked = true;
            button.interactable = false;
            heldIndicator.SetActive(false);
        }
        /// <summary>
        /// called when the player taps the card. 
        /// </summary>
        public void OnTap()
        {
            if (locked) return;
            held = !held;
            heldIndicator.SetActive(held);
        }
    }
}
