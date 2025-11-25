using Grid;
using Spells.States;
using StateMachine;
using Unit;
using Unit.Interfaces;
using UnityEngine;

namespace Spells
{
    public class TrapStateMachine : UnitStateMachineBase
    {
        public override BaseState IdleState { get; set; }

        public void OnPlaceTrap()
        {
            IdleState = new HitActionState(this);
            ChangeState(IdleState);
        }

        protected override BaseState GetInitialState()
        {
            return IdleState;
        }

        public override void DestroyUnit()
        {
            GridManager.Instance.SetPositionOccupiedInGrid(transform.position.x, transform.position.y, false);
            Destroy(gameObject);
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