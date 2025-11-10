using StateMachine;

namespace Unit.States
{
    public class UnitDyingState : BaseState
    {
        private readonly UnitStateMachineBase _state;

        public UnitDyingState(UnitStateMachineBase state) : base(state, "Card Dying State")
        {
            _state = state;
        }
    }
}