using StateMachine;

namespace GameManager.States
{
    public class PausedPhaseState : BaseState
    {
        private readonly GameManagerStateMachine _state;

        public PausedPhaseState(GameManagerStateMachine state) : base(state, "Paused Phase State")
        {
            _state = state;
        }
    }
}