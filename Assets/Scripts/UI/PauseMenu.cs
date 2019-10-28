using RPG.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.UI
{
    public class PauseMenu : MonoBehaviour 
    {
        public static bool isPaused = false;
        public GameObject pauseMenuUI;
        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(isPaused)
                {
                    Resume();
                } else
                {
                    Pause();
                }

            }
        }
        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }

        public void Save()
        {
            FindObjectOfType<SavingWrapper>().GetComponent<SavingWrapper>().Save();
        }
        public void Load()
        {
            FindObjectOfType<SavingWrapper>().GetComponent<SavingWrapper>().Load();
        }
        public void Delete()
        {
            print("deleted");
            FindObjectOfType<SavingWrapper>().GetComponent<SavingWrapper>().Delete();
        }


        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
        public void LoadMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    }
}