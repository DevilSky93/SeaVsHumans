using System.Collections.Generic;
using Events.Trigger;
using JetBrains.Annotations;
using StateMachine;
using Unit.States;
using UnityEngine;

namespace Unit.Types
{
    public class Wave : UnitStateMachineBase
    {
        [SerializeField] private GameEventListener onDestroyUnit;
        public override BaseState MovementState { get; set; }
        public override BaseState IdleState { get; set; }
        public override BaseState FightingState { get; set; }
        public override BaseState DyingState { get; set; }
        public override BaseState PlacingState { get; set; }

        protected override void Awake()
        {
            base.Awake();
            MovementState = new UnitMovementState(this, transform, unitCardData.speed, enemyLayerMask, tileMask, allyMask);
            IdleState = new UnitIdleState(this);
            FightingState = new UnitFightingState(this, HealthController, enemyLayerMask);
            DyingState = new UnitDyingState(this);
            PlacingState = new UnitPlacingState(this, onDestroyUnit);
        }

        [UsedImplicitly]
        public void OnDestroyUnit()
        {
            Destroy(gameObject);
        }
    }
}