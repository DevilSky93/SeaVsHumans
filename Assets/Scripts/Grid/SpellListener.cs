using Events.Trigger;
using JetBrains.Annotations;
using Player;
using UnityEngine;

namespace Grid
{
    public class SpellListener : MonoBehaviour
    {
        [SerializeField] private PlayerInputControls playerInputControls;
        [SerializeField] private EventTrigger onTriggerCard;
        
        private bool _canPlaySpell;

        private void Awake()
        {
            playerInputControls.PlayerInput.Player.PlaceUnit.Enable();
        }

        private void Update()
        {
            if (_canPlaySpell && playerInputControls.PlayerInput.Player.PlaceUnit.WasReleasedThisFrame())
            {
                onTriggerCard.Raise();
                _canPlaySpell = false;
            }
        }

        [UsedImplicitly]
        public void OnPlaySpell()
        {
            _canPlaySpell = true;
        }
    }
}