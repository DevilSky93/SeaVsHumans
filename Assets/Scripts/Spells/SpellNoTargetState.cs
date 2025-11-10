using StateMachine;

namespace Spells
{
    public class SpellNoTargetState : BaseState
    {
        private readonly DrawTwoCardStateMachine _state;

        public SpellNoTargetState(DrawTwoCardStateMachine state) : base(state, "Spell No Target State")
        {
            _state = state;
        }
    }
}