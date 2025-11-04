using StateMachine;

namespace Unit.States
{
    public class UnitDyingState : BaseState
    {
        private readonly UnitStateMachine _state;

        public UnitDyingState(UnitStateMachine state) : base(state, "Unit Dying State")
        {
            _state = state;
        }
    }
}