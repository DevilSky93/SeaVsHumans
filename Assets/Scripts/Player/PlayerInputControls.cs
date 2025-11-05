using UnityEngine;

namespace Player
{
    public class PlayerInputControls : MonoBehaviour
    {
        private InputSystem_Actions _playerInput;
        
        public InputSystem_Actions PlayerInput
        {
            get
            {
                _playerInput ??= new InputSystem_Actions();
                return _playerInput;
            }   
        }

        private void OnDisable()
        {
            _playerInput?.Disable();
        }
    }
}