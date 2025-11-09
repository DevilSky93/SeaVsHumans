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
        
        public void StartRound()
        {
            onRoundStart.Raise();
            roundText.text = "Fighting round";
            roundButton.interactable = false;
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