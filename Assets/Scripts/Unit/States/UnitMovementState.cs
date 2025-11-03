using StateMachine;

namespace Unit.States
{
    public class UnitMovementState : BaseState {
        private readonly UnitStateMachine _state;

        public UnitMovementState(UnitStateMachine state) : base(state, "Unit Movement State")
        {
            _state = state;
            StateMachine.ChangeState(_state.MovementState);
        }
    }
}