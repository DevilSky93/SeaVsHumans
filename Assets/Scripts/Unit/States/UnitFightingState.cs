using Player;
using StateMachine;

namespace Unit.States
{
    public class UnitFightingState : BaseState
    {
        private readonly UnitStateMachine _state;
        private readonly HealthController _healthController;

        public UnitFightingState(UnitStateMachine state, HealthController healthController) : base(state, "Unit Fighting State")
        {
            _state = state;
            _healthController = healthController;
        }

        public override void UpdateLogics()
        {
            if (_healthController.IsDead)
            {
                StateMachine.ChangeState(_state.DyingState);
            }
        }
    }
}