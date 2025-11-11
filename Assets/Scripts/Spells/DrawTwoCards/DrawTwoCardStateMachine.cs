using Events.Float;
using UnityEngine;

namespace Spells.DrawTwoCards
{
    public class DrawTwoCardStateMachine : SpellCardStateMachineBase
    {
        [SerializeField] private int numberOfCardsToDraw;
        [SerializeField] private EventFloat onCardsDrawn;

        protected override void Awake()
        {
            base.Awake();
            SpellActionState = new DrawActionState(this, numberOfCardsToDraw, onCardsDrawn);
        }
    }
}