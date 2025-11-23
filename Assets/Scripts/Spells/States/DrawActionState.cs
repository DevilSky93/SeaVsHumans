using Events.Float;
using StateMachine;
using UnityEngine;

namespace Spells.States
{
    public class DrawActionState : BaseState
    {
        private readonly SpellCardStateMachineBase _state;
        private readonly int _numberOfCardsToDraw;
        private readonly EventFloat _onCardsDrawn;

        public DrawActionState(SpellCardStateMachineBase state, int numberOfCardsToDraw, EventFloat onCardsDrawn) : base(state, "Draw Action State")
        {
            _state = state;
            _numberOfCardsToDraw = numberOfCardsToDraw;
            _onCardsDrawn = onCardsDrawn;
        }
        
        public override void Enter()
        {
            Debug.Log("Drawing Cards");
            _onCardsDrawn.Raise(_numberOfCardsToDraw);
        }
    }
}