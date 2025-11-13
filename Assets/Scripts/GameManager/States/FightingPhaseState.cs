using Events.Float;
using Events.Trigger;
using StateMachine;
using UnityEngine;

namespace GameManager.States
{
    public class FightingPhaseState : BaseState
    {
        private readonly GameManagerStateMachine _state;
        private readonly EventTrigger _onRoundEnd;
        private readonly EventFloat _onRoundTimerUpdate;
        private float _roundTimer;
        private int _lastDisplayedSecond = -1;

        public const float RoundTimeLimit = 30f;

        public FightingPhaseState(GameManagerStateMachine state, EventTrigger onRoundEnd, EventFloat onRoundTimerUpdate) : base(state, "Fighting Phase State")
        {
            _state = state;
            _onRoundEnd = onRoundEnd;
            _onRoundTimerUpdate = onRoundTimerUpdate;
            _roundTimer = RoundTimeLimit;
        }

        public override void UpdateLogics()
        {
            if (_roundTimer <= 0)
            {
                _state.ChangeState(_state.PreparingPhaseState);
                _onRoundEnd.Raise();
                _roundTimer = RoundTimeLimit;
            }

            _roundTimer -= Time.deltaTime;

            int currentSecond = Mathf.CeilToInt(_roundTimer);
            if (currentSecond != _lastDisplayedSecond)
            {
                _lastDisplayedSecond = currentSecond;
                _onRoundTimerUpdate?.Raise(_roundTimer);
            }
        }
    }
}