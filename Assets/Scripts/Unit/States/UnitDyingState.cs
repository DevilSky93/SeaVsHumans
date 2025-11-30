using StateMachine;
using Object = UnityEngine.Object;

namespace Unit.States
{
    public class UnitDyingState : BaseState
    {
        private readonly UnitStateMachineBase _state;

        public UnitDyingState(UnitStateMachineBase state) : base(state, "Card Dying State")
        {
            _state = state;
        }

        public override void Enter()
        {
            _state.UnitAnimation.DiedAnimation();
            Object.Destroy(_state.gameObject);
        }
    }
}