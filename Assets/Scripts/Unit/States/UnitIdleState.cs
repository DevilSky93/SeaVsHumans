using StateMachine;

namespace Unit.States
{
    public class UnitIdleState : BaseState
    {
        private readonly UnitStateMachineBase _state;

        public UnitIdleState(UnitStateMachineBase state) : base(state, "Card Idle State")
        {
            _state = state;
        }
    }
}