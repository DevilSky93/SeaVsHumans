using StateMachine;

namespace GameManager.States
{
    public class FightingPhaseState : BaseState
    {
        private readonly GameManagerStateMachine _state;

        public FightingPhaseState(GameManagerStateMachine state) : base(state, "Fighting Phase State")
        {
            _state = state;
        }
    }
}