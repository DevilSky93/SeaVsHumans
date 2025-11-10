using Player;
using StateMachine;

namespace Unit.States
{
    public class UnitFightingState : BaseState
    {
        private readonly UnitStateMachineBase _state;
        private readonly HealthController _healthController;

        public UnitFightingState(UnitStateMachineBase state, HealthController healthController) : base(state, "Card Fighting State")
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