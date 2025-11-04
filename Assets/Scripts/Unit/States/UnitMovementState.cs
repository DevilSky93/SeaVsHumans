using StateMachine;
using Unit.Interfaces;
using UnityEngine;

namespace Unit.States
{
    public class UnitMovementState : BaseState, IPhysicsEventHandler
    {
        private readonly UnitStateMachine _state;
        private readonly Transform _unitTransform;
        private readonly float _unitSpeed;

        public UnitMovementState(UnitStateMachine state, Transform unitTransform, float unitSpeed) : base(state,
            "Unit Movement State")
        {
            _state = state;
            _unitTransform = unitTransform;
            _unitSpeed = unitSpeed;
        }

        public override void UpdateLogics()
        {
            _unitTransform.transform.Translate(Vector3.right * (_unitSpeed * Time.deltaTime));
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            _state.ChangeState(_state.FightingState);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            // throw new System.NotImplementedException();
        }
    }
}