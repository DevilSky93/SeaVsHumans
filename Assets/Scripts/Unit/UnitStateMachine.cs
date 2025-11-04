using System;
using Cards.Models;
using JetBrains.Annotations;
using Player;
using StateMachine;
using Unit.Interfaces;
using Unit.States;
using UnityEngine;

namespace Unit
{
    public class UnitStateMachine : StateMachine.StateMachine
    {
        [SerializeField] private CardData cardData;
        [SerializeField] private LayerMask enemyLayerMask;
        private Unit _unit;
        public UnitMovementState MovementState { get; private set; }
        public UnitIdleState IdleState { get; private set; }
        public UnitFightingState FightingState { get; private set; }
        public UnitDyingState DyingState { get; private set; }
        
        private HealthController _healthController;

        private void Awake()
        {
            Initialize(cardData);
        }

        private void Initialize(CardData cd)
        {
            _healthController = GetComponent<HealthController>();
            _unit = new Unit(cd);
            MovementState = new UnitMovementState(this, transform, cardData.speed);
            IdleState = new UnitIdleState(this);
            FightingState = new UnitFightingState(this, _healthController);
            DyingState = new UnitDyingState(this);
        }

        protected override BaseState GetInitialState()
        {
            return IdleState;
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