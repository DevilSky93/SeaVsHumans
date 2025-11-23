using StateMachine;
using UnityEngine;

namespace Spells.States
{
    public class DestroyCardState : BaseState
    {
        private readonly SpellCardStateMachineBase _state;

        public DestroyCardState(SpellCardStateMachineBase state) : base(state, "Destroy card State")
        {
            _state = state;
        }

        public override void Enter()
        {
            Object.Destroy(_state.gameObject);
        }
    }
}