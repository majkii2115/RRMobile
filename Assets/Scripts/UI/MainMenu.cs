using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RPG.UI
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject loadingScreen;
        public Slider slider;
        public void PlayGame()
        {
            StartCoroutine(LoadAsynchronously());
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
        public IEnumerator LoadAsynchronously()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

            loadingScreen.SetActive(true);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                slider.value = progress;
                yield return null;
            }
        }
    }
}
