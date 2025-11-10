using JetBrains.Annotations;
using Unit.States;

namespace Unit.Types
{
    public class Wave : UnitStateMachineBase
    {
        public override UnitMovementState MovementState { get; set; }
        public override UnitIdleState IdleState { get; set; }
        public override UnitFightingState FightingState { get; set; }
        public override UnitDyingState DyingState { get; set; }
        public override UnitPlacingState PlacingState { get; set; }

        protected override void Awake()
        {
            base.Awake();
            MovementState = new UnitMovementState(this, transform, unitCardData.speed, enemyLayerMask, tileMask);
            IdleState = new UnitIdleState(this);
            FightingState = new UnitFightingState(this, HealthController);
            DyingState = new UnitDyingState(this);
            PlacingState = new UnitPlacingState(this, onDestroyUnit);
        }

        [UsedImplicitly]
        public void OnDestroyUnit()
        {
            PlacingState.OnDestroy();
        }
    }
}