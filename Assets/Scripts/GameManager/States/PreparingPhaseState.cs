using StateMachine;

namespace GameManager.States
{
    public class PreparingPhaseState : BaseState
    {
        private readonly GameManagerStateMachine _state;

        public PreparingPhaseState(GameManagerStateMachine state) : base(state, "Preparing Phase State")
        {
            _state = state;
        }
    }
}