using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;

        private bool _isPaused;
        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                PauseOrResume();
            }
        }

        [UsedImplicitly]
        public void PauseOrResume()
        {
            _isPaused = !_isPaused;
            pauseMenu.SetActive(_isPaused);
            Time.timeScale = _isPaused ? 0 : 1;
        }
    }
}