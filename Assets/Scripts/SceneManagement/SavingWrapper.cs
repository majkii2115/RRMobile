using UnityEngine;
using RPG.Saving;
using System.Collections;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour 
    {
        public const string saveFile = "saveFile";
        [SerializeField] float fadeInTime = 0.2f;
        IEnumerator Start() 
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(saveFile);
            yield return fader.FadeIn(fadeInTime);
        }
        public void Save()
        {
            GetComponent<SavingSystem>().Save(saveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(saveFile);
        }
        public void Delete() 
        {
            GetComponent<SavingSystem>().Delete(saveFile);
        }
    }
}