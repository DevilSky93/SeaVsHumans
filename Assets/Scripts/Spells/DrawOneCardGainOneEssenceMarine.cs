using Events.Float;
using Spells.States;
using UnityEngine;

namespace Spells
{
    public class DrawOneCardGainOneEssenceMarine : SpellCardStateMachineBase
    {
        [SerializeField] private EventFloat onCardsDrawn;
        [SerializeField] private EventFloat onEssenceMarineGain;

        protected override void Awake()
        {
            base.Awake();
            SpellActionState = new CombineActionState(this,
                new DrawActionState(this, 1, onCardsDrawn),
                new GainEssenceMarineState(this, onEssenceMarineGain));
        }
    }
}