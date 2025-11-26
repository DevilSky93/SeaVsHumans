using Events.Trigger;
using Grid;
using Spells.States;
using StateMachine;
using Unit;
using Unit.Interfaces;
using Unit.States;
using UnityEngine;

namespace Spells
{
    public class TrapStateMachine : UnitStateMachineBase
    {
        protected override void Awake()
        {
            PlacingState = new UnitPlacingState(this, null);
        }

        public void OnPlaceTrap()
        {
            IdleState = new UnitIdleState(this);
            MovementState = new HitActionState(this);
        }

        protected override BaseState GetInitialState()
        {
            return PlacingState;
        }

        public override void DestroyUnit()
        {
            GridManager.Instance.SetPositionOccupiedInGrid(transform.position.x, transform.position.y, false);
            Destroy(gameObject);
        }

        public override void OnRoundStart()
        {
            ChangeState(MovementState);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (CurrentBaseState is IPhysicsEventHandler physicsHandler)
            {
                Debug.Log($"Enter {IdleState}");
                physicsHandler.OnTriggerEnter2D(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (CurrentBaseState is IPhysicsEventHandler physicsHandler)
            {
                physicsHandler.OnTriggerExit2D(other);
            }
        }
    }
}