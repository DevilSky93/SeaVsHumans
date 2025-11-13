using Events.Float;
using Events.Trigger;
using GameManager.States;
using JetBrains.Annotations;
using StateMachine;
using UnityEngine;

namespace GameManager
{
    public class GameManagerStateMachine : StateMachine.StateMachine
    {
        [SerializeField] private EventTrigger onRoundEnd;
        [SerializeField] private EventFloat onRoundTimerUpdate;
        public PreparingPhaseState PreparingPhaseState { get; private set; }
        public FightingPhaseState FightingPhaseState { get; private set; }
        public PausedPhaseState PausedPhaseState { get; private set; }

        protected override void Awake()
        {
            PreparingPhaseState = new PreparingPhaseState(this);
            FightingPhaseState = new FightingPhaseState(this, onRoundEnd, onRoundTimerUpdate);
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