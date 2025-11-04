using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unit.States
{
    public class UnitPlacingState : BaseState
    {
        private readonly UnitStateMachine _state;

        public UnitPlacingState(UnitStateMachine state) : base(state, "Unit Placing State")
        {
            _state = state;
        }

        public override void UpdateLogics()
        {
            Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _state.transform.position = new Vector3(screenToWorldPoint.x, screenToWorldPoint.y, 0);
            // if (Keyboard.current.mKey.wasPressedThisFrame)
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Debug.Log("released");
                StateMachine.ChangeState(_state.IdleState);
            }
        }
    }
}