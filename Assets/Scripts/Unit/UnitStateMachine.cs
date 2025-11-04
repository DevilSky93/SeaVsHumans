using Cards.Models;
using JetBrains.Annotations;
using Player;
using StateMachine;
using Unit.Interfaces;
using Unit.States;
using UnityEngine;

namespace Unit
{
    public abstract class UnitStateMachine : StateMachine.StateMachine
    {
        [SerializeField] private LayerMask enemyLayerMask;
        [SerializeField] protected CardData unitCardData;
        protected HealthController HealthController;

        private Unit _unit;
        public Unit Unit => _unit ??= new Unit(unitCardData);

        public abstract UnitMovementState MovementState { get; set; }
        public abstract UnitIdleState IdleState { get; set; }
        public abstract UnitFightingState FightingState { get; set; }
        public abstract UnitDyingState DyingState { get; set; }
        public abstract UnitPlacingState PlacingState { get; set; }


        public virtual void Initialize()
        {
            HealthController = GetComponent<HealthController>();
        }

        protected override BaseState GetInitialState()
        {
            return PlacingState;
        }
        
        [UsedImplicitly]
        public void RoundStart()
        {
            ChangeState(MovementState);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((enemyLayerMask.value & (1 << other.gameObject.layer)) > .1f && CurrentBaseState is IPhysicsEventHandler physicsHandler)
            {
                physicsHandler.OnTriggerEnter2D(other);
            }
        }
    }
}