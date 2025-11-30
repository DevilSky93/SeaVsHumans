using Events.Float;
using Events.Trigger;
using GameManager.States;
using JetBrains.Annotations;
using StateMachine;
using TMPro;
using UnityEngine;

namespace GameManager
{
    public class GameManagerStateMachine : StateMachine.StateMachine
    {
        [SerializeField] private EventTrigger onRoundEnd;
        [SerializeField] private EventFloat onRoundTimerUpdate;
        [SerializeField] private GameObject gameOver;
        public BaseState PreparingPhaseState { get; private set; }
        public FightingPhaseState FightingPhaseState { get; private set; }
        public BaseState PausedPhaseState { get; private set; }
        public BaseState GameOverPhaseState { get; private set; }

        protected override void Awake()
        {
            PreparingPhaseState = new PreparingPhaseState(this);
            FightingPhaseState = new FightingPhaseState(this, onRoundEnd, onRoundTimerUpdate);
            PausedPhaseState = new PausedPhaseState(this);
            GameOverPhaseState = new GameOverPhaseState(this, gameOver);
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
            FightingPhaseState.ResetTimer();
            ChangeState(PreparingPhaseState);
        }

        [UsedImplicitly]
        public void OnGameOver()
        {
            ChangeState(GameOverPhaseState);
        }
    }
}