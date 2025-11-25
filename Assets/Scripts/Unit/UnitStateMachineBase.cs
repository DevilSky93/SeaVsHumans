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
        [SerializeField] protected GameEventFloatFloatListener onPlaceUnit;
        private Card _card;

        public UnitHealthController HealthController { get; private set; }
        public Card Card => _card ??= new Card(unitCardData);

        public virtual BaseState MovementState { get; set; }
        public virtual BaseState IdleState { get; set; }
        public virtual BaseState FightingState { get; set; }
        public virtual BaseState DyingState { get; set; }
        public virtual BaseState PlacingState { get; set; }

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
        public virtual void OnPlaceUnit(float x, float y)
        {
            Vector2? position = GridManager.Instance.GetPositionInGrid(x, y);
            if (!position.HasValue) return;
            GridManager.Instance.SetPositionOccupiedInGrid(x, y, true);
            Vector3 newPos = new(position.Value.x, position.Value.y, 0);
            transform.position = newPos;
            onPlaceUnit.enabled = false;

            ChangeState(IdleState);
        }

        public void OnRoundEnd()
        {
            Vector2? gridPos = GridManager.Instance.GetPositionInGrid(transform.position.x, transform.position.y);
            if (!gridPos.HasValue)
            {
                Debug.LogWarning("Can't reset position");
                return;
            }
            GridManager.Instance.SetPositionOccupiedInGrid(gridPos.Value.x, gridPos.Value.y, false);
            ChangeState(IdleState);
            GridManager.Instance.SetPositionOccupiedInGrid(transform.position.x, transform.position.y, true);
        } 

        public virtual void DestroyUnit()
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