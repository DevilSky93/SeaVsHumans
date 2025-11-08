using Cards.Models;
using Events.FloatFloat;
using Events.Trigger;
using Grid;
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
        [SerializeField] protected GameEventListener onDestroyUnit;
        [SerializeField] protected GameEventFloatFloatListener onPlaceUnit;

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

        [UsedImplicitly]
        public void OnPlaceUnit(float x, float y)
        {
            Vector2? position = GridManager.Instance.GetPositionInGrid(x, y);
            if (!position.HasValue) return;
            ChangeState(IdleState);

            Vector3 newPos = new(position.Value.x, position.Value.y, 0);
            transform.position = newPos;
            onPlaceUnit.enabled = false;
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