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
        private readonly string _fightingRoundText = "Fighting round {0}s";

        public void StartRound()
        {
            onRoundStart.Raise();
            roundText.text = string.Format(_fightingRoundText, FightingPhaseState.RoundTimeLimit);
            roundButton.interactable = false;
        }

        [UsedImplicitly]
        public void OnRoundTimerUpdate(float timeLeft)
        {
            roundText.text = string.Format(_fightingRoundText, (int)timeLeft);
        }
        
        [UsedImplicitly]
        public void OnRoundEnd()
        {
            roundText.text = "Start round";
            roundButton.interactable = true;
        }

        [UsedImplicitly]
        public void OnCheckUnitStillOnField()
        {
            Debug.Log("Checking if there are still units on field...");
            bool areFieldEmpty = GridManager.Instance.IsFieldIsEmpty();
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