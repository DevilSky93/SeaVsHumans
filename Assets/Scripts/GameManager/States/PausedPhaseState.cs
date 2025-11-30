using StateMachine;
using UnityEngine;

namespace GameManager.States
{
    public class PausedPhaseState : BaseState
    {
        private readonly GameManagerStateMachine _state;

        public PausedPhaseState(GameManagerStateMachine state) : base(state, "Paused Phase State")
        {
            _state = state;
        }

        public override void Enter()
        {
            Time.timeScale = 0;
        }

        public override void Exit()
        {
            Time.timeScale = 1;
        }
    }
}