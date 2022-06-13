
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
namespace InterviewSolution
{

    /// <summary>
    /// monobehaviour responsible for managing the single button at the bottom of the screen
    /// </summary>
    public class ActionButton : MonoBehaviour
    {
        [Header("Scene Dependencies")]
        public Button button;
        [Header("Configurable")]
        public UnityEvent onBet;
        public UnityEvent onSwap;
        public UnityEvent onFinish;
        public enum ActionType { Bet, Swap, Finish }

        /// <summary>
        /// set initial button state
        /// </summary>
        private void Start()
        {
            SwitchAction(ActionType.Bet);
        }

        /// <summary>
        /// change button's behaviour depending on what stage of the game we're in. 
        /// </summary>
        /// <param name="actionType"></param>
        private void SwitchAction(ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.Bet:
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(Bet);
                    break;
                case ActionType.Swap:
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(Swap);
                    break;
                case ActionType.Finish:
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(Finish);
                    break;
                default:
                    break;
            }
        }
        //invoke a unityevent, then set the button to trigger the next stage in the game. 
        public void Bet()
        {
            onBet?.Invoke();
            SwitchAction(ActionType.Swap);
        }

        public void Swap()
        {
            onSwap?.Invoke();
            SwitchAction(ActionType.Finish);
        }

        public void Finish()
        {
            onFinish?.Invoke();
            SwitchAction(ActionType.Bet);
        }

    }

}
