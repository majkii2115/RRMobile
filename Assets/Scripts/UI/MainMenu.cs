using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        public void QuitGame()
        {
            Debug.Log("QUIT");
            Application.Quit();
        }
        public void LoadMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
