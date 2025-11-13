using Cards.Models;
using Events.FloatFloat;
using Events.Trigger;
using Grid;
using JetBrains.Annotations;
using StateMachine;
using Unit.Interfaces;
using Unit.States;
using UnityEngine;

namespace Unit
{
    public abstract class UnitStateMachineBase : StateMachine.StateMachine
    {
        [SerializeField] protected LayerMask enemyLayerMask;
        [SerializeField] protected LayerMask tileMask;
        [SerializeField] protected EventTrigger onCheckUnitStillOnField;
        [SerializeField] protected CardData unitCardData;
        [SerializeField] protected GameEventListener onDestroyUnit;
        [SerializeField] protected GameEventFloatFloatListener onPlaceUnit;

        public UnitHealthController HealthController { get; private set; }

        private Card _card;
        public Card Card => _card ??= new Card(unitCardData);

        public abstract UnitMovementState MovementState { get; set; }
        public abstract UnitIdleState IdleState { get; set; }
        public abstract UnitFightingState FightingState { get; set; }
        public abstract UnitDyingState DyingState { get; set; }
        public abstract UnitPlacingState PlacingState { get; set; }

        protected override void Awake()
        {
            HealthController = GetComponent<UnitHealthController>();
        }

        protected override BaseState GetInitialState()
        {
            return PlacingState;
        }
        
        [UsedImplicitly]
        public void OnRoundStart()
        {
            ChangeState(MovementState);
        }

        [UsedImplicitly]
        public void OnPlaceUnit(float x, float y)
        {
            Vector2? position = GridManager.Instance.GetPositionInGrid(x, y);
            if (!position.HasValue) return;
            GridManager.Instance.SetPositionOccupiedInGrid(x, y, true);
            ChangeState(IdleState);

            Vector3 newPos = new(position.Value.x, position.Value.y, 0);
            transform.position = newPos;
            onPlaceUnit.enabled = false;
        }

        public void DestroyUnit()
        {
            GridManager.Instance.SetPositionOccupiedInGrid(transform.position.x, transform.position.y, false);
            onCheckUnitStillOnField.Raise();
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (CurrentBaseState is IPhysicsEventHandler physicsHandler)
            {
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