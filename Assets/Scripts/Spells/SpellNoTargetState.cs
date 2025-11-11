using StateMachine;

namespace Spells
{
    public class SpellNoTargetState : BaseState
    {
        private readonly SpellCardStateMachineBase _state;

        public SpellNoTargetState(SpellCardStateMachineBase state) : base(state, "Spell No Target State")
        {
            _state = state;
        }
    }
}