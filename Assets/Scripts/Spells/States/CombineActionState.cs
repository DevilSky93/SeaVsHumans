using StateMachine;

namespace Spells.States
{
    public class CombineActionState : BaseState
    {
        private readonly SpellCardStateMachineBase _state;
        private readonly BaseState[] _actions;

        public CombineActionState(SpellCardStateMachineBase state, params BaseState[] actions) : base(state, "Combine Action State")
        {
            _state = state;
            _actions = actions;
        }

        public override void Enter()
        {
            foreach (BaseState action in _actions)
            {
                action.Enter();
            }
        }
    }
}