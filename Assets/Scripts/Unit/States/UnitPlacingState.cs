using Events.Trigger;
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unit.States
{
    public class UnitPlacingState : BaseState
    {
        private readonly UnitStateMachineBase _state;
        private readonly GameEventListener _onDestroyUnit;

        public UnitPlacingState(UnitStateMachineBase state, GameEventListener onDestroyUnit) : base(state, "Card Placing State")
        {
            _state = state;
            _onDestroyUnit = onDestroyUnit;
        }

        public override void UpdateLogics()
        {
            Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _state.transform.position = new Vector3(screenToWorldPoint.x, screenToWorldPoint.y, 0);
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Debug.Log("released");
                StateMachine.ChangeState(_state.IdleState);
            }
        }
        
        public void OnDestroy()
        {
            Object.Destroy(_state.gameObject);
        }

        public override void Exit()
        {
            _onDestroyUnit.enabled = false;
        }
    }
}