using StateMachine;
using UnityEngine;

namespace Unit.States
{
    public class UnitIdleState : BaseState
    {
        private readonly UnitStateMachineBase _state;
        private Vector2? _originalPosition;

        public UnitIdleState(UnitStateMachineBase state) : base(state, "Card Idle State")
        {
            _state = state;
            // #if UNITY_EDITOR
            //     GridManager.Instance.SetPositionOccupiedInGrid(state.transform.position.x, state.transform.position.y, true);
            // #endif
        }

        public override void Enter()
        {
            if (_originalPosition != null)
            {
                _state.transform.position = (Vector3)_originalPosition;
            }
            else
            {
                _originalPosition = _state.transform.position;
            }
        }
    }
}