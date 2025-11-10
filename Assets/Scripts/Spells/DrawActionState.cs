using Events.Float;
using StateMachine;
using UnityEngine;

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
            Debug.Log("Drawing Cards");
            _onCardsDrawn.Raise(_numberOfCardsToDraw);
            Object.Destroy(_state.gameObject);
        }
    }
}