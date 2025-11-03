using UnityEngine;
using UnityEngine.InputSystem;
using EventTrigger = Events.Trigger.EventTrigger;

namespace Round
{
    public class RoundManager : MonoBehaviour
    {
        [SerializeField] private EventTrigger startRoundEvent;

        private void Update()
        {
            if (Keyboard.current.gKey.wasPressedThisFrame)
            {
                startRoundEvent.Raise();
            }
        }
    }
}