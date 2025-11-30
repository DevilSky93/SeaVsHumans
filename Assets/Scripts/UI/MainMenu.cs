using UnityEngine;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public void ExitGame()
        {
            Application.Quit();
        }
        
        public void StartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        }
    }
}
