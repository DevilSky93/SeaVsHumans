using StateMachine;

namespace Unit.States
{
    public class UnitIdleState : BaseState
    {
        private readonly UnitStateMachine _state;

        public UnitIdleState(UnitStateMachine state) : base(state, "Unit Idle State")
        {
            _state = state;
        }
    }
}