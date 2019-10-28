using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.UI
{
    public class DeadMenu : MonoBehaviour 
    {
        public void LoadMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}