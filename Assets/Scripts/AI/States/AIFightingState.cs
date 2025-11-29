using StateMachine;

namespace AI.States
{
    public class AIFightingState : BaseState
    {
        private AiStateMachine _state;

        public AIFightingState(AiStateMachine state) : base(state, "AI Fighting State")
        {
            _state = state;
        }
    }
}