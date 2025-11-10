using GameManager.States;
using JetBrains.Annotations;
using StateMachine;

namespace GameManager
{
    public class GameManagerStateMachine : StateMachine.StateMachine
    {
        public PreparingPhaseState PreparingPhaseState { get; private set; }
        public FightingPhaseState FightingPhaseState { get; private set; }
        public PausedPhaseState PausedPhaseState { get; private set; }

        protected override void Awake()
        {
            PreparingPhaseState = new PreparingPhaseState(this);
            FightingPhaseState = new FightingPhaseState(this);
            PausedPhaseState = new PausedPhaseState(this);
        }

        protected override BaseState GetInitialState()
        {
            return PreparingPhaseState;
        }
        
        [UsedImplicitly]
        public void OnRoundStart()
        {
            ChangeState(FightingPhaseState);
        }
        
        [UsedImplicitly]
        public void OnRoundEnd()
        {
            ChangeState(PreparingPhaseState);
        }
    }
}