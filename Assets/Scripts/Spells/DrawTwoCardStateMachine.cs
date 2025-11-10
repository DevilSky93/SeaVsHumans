using Events.Float;
using StateMachine;
using UnityEngine;

namespace Spells
{
    public class DrawTwoCardStateMachine : SpellStateMachineBase
    {
        [SerializeField] private int numberOfCardsToDraw;
        [SerializeField] private EventFloat onCardsDrawn;

        public BaseState SpellNoTargetState { get; private set; }
        public BaseState DrawActionState { get; private set; }

        protected override void Awake()
        {
            SpellNoTargetState = new SpellNoTargetState(this);
            DrawActionState = new DrawActionState(this, numberOfCardsToDraw, onCardsDrawn);
        }
        
        public void OnTriggerCard()
        {
            ChangeState(DrawActionState);
        }

        protected override BaseState GetInitialState()
        {
            return SpellNoTargetState;
        }
    }
}