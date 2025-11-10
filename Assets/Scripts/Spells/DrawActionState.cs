using Events.Float;
using StateMachine;

namespace Spells
{
    public class DrawActionState : BaseState
    {
        private readonly DrawTwoCardStateMachine _state;
        private readonly int _numberOfCardsToDraw;
        private readonly EventFloat _onCardsDrawn;

        public DrawActionState(DrawTwoCardStateMachine state, int numberOfCardsToDraw, EventFloat onCardsDrawn) : base(state, "Spell No Target State")
        {
            _state = state;
            _numberOfCardsToDraw = numberOfCardsToDraw;
            _onCardsDrawn = onCardsDrawn;
        }
        
        public override void Enter()
        {
            _onCardsDrawn.Raise(_numberOfCardsToDraw);
        }
    }
}