using StateMachine;
using TMPro;
using UnityEngine;

namespace GameManager.States
{
    public class GameOverPhaseState : BaseState
    {
        private readonly TMP_Text _gameOver;
        private GameManagerStateMachine _state;

        public GameOverPhaseState(GameManagerStateMachine state, TMP_Text gameOver) : base(state, "Game Over Phase State")
        {
            _state = state;
            _gameOver = gameOver;
        }

        public override void Enter()
        {
            _gameOver.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}