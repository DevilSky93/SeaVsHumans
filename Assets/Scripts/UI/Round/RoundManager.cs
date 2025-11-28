using GameManager.States;
using Grid;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using EventTrigger = Events.Trigger.EventTrigger;

namespace UI.Round
{
    public class RoundManager : MonoBehaviour
    {
        [SerializeField] private EventTrigger onRoundStart;
        [SerializeField] private EventTrigger onRoundEnd;
        [SerializeField] private Button roundButton;
        [SerializeField] private TMP_Text roundText;
        [SerializeField] private int maxRound;
        private const string FightingRoundText = "Fighting round {0}s";

        private int _roundCount = 1;

        public void StartRound()
        {
            onRoundStart.Raise();
            roundText.text = string.Format(FightingRoundText, FightingPhaseState.RoundTimeLimit);
            roundButton.interactable = false;
        }

        [UsedImplicitly]
        public void OnRoundTimerUpdate(float timeLeft)
        {
            if (!roundButton.interactable)
            {
                roundText.text = string.Format(FightingRoundText, (int)timeLeft);
            }
        }
        
        [UsedImplicitly]
        public void OnRoundEnd()
        {
            roundText.text = "Start round";
            roundButton.interactable = true;
            _roundCount++;
            if (_roundCount >= maxRound)
            {
                Debug.Log("Game over");
            }
        }

        [UsedImplicitly]
        public void OnCheckUnitStillOnField()
        {
            Debug.Log("Checking if there are still units on field...");
            bool areFieldEmpty = GridManager.Instance.IsFieldEmpty();
            if (areFieldEmpty)
            {
                Debug.LogWarning("No more units on field");
                onRoundEnd.Raise();
            }
            else
            {
                Debug.LogWarning("Still units on field");
            }
        }
    }
}