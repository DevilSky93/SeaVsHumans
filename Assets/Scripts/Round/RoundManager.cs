using TMPro;
using UnityEngine;
using UnityEngine.UI;
using EventTrigger = Events.Trigger.EventTrigger;

namespace Round
{
    public class RoundManager : MonoBehaviour
    {
        [SerializeField] private EventTrigger startRoundEvent;
        [SerializeField] private Button roundButton;
        [SerializeField] private TMP_Text roundText;
        
        public void StartRound()
        {
            startRoundEvent.Raise();
            roundText.text = "Fighting round";
            roundButton.interactable = false;
        }
        
        public void EndRound()
        {
            roundText.text = "Start round";
            roundButton.interactable = true;
        }
    }
}