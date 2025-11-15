using Events.Float;
using UnityEngine;

namespace Spells
{
    public class DrawTwoCardStateMachine : SpellCardStateMachineBase
    {
        [SerializeField] private EventFloat onCardsDrawn;
        private const int NumberOfCardsToDraw = 2;

        protected override void Awake()
        {
            base.Awake();
            SpellActionState = new DrawActionState(this, NumberOfCardsToDraw, onCardsDrawn);
        }
    }
}