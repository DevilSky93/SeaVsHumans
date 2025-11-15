using Events.Float;
using StateMachine;
using UnityEngine;

namespace Spells
{
    public class GainEssenceMarineState : BaseState
    {
        private readonly SpellCardStateMachineBase _state;
        private readonly EventFloat _onEssenceMarineGain;

        public GainEssenceMarineState(SpellCardStateMachineBase state, EventFloat onEssenceMarineGain) : base(state, "Gain Essence Marine State")
        {
            _state = state;
            _onEssenceMarineGain = onEssenceMarineGain;
        }

        public override void Enter()
        {
            Debug.Log("Gain one essence");
            _onEssenceMarineGain.Raise(1);
        }
    }
}