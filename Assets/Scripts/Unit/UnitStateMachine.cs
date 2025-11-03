using System;
using Cards.Models;
using JetBrains.Annotations;
using StateMachine;
using Unit.States;
using UnityEngine;

namespace Unit
{
    public class UnitStateMachine : StateMachine.StateMachine
    {
        [SerializeField] private CardData cardData;
        private Unit _unit;
        public UnitMovementState MovementState { get; private set; }
        public UnitIdleState IdleState { get; private set; }

        private void Awake()
        {
            Initialize(cardData);
        }

        private void Initialize(CardData cd)
        {
            _unit = new Unit(cd);
            MovementState = new UnitMovementState(this);
            IdleState = new UnitIdleState(this);
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
    }
}