using Events.Float;
using StateMachine;
using UnityEngine;

namespace Spells
{
    public class DrawTwoCardStateMachine : SpellStateMachineBase
    {
        [SerializeField] private int numberOfCardsToDraw;
        [SerializeField] private EventFloat onCardsDrawn;
        private BaseState _spellNoTargetState;

        protected override void Awake()
        {
            numberOfCardsToDraw = 2;
            _spellNoTargetState = new DrawActionState(this, numberOfCardsToDraw, onCardsDrawn);
        }

        protected override BaseState GetInitialState()
        {
            return _spellNoTargetState;
        }
    }
}