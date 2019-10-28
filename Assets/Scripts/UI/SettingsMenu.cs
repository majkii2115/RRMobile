using UnityEngine;
using UnityEngine.Audio;

namespace RPG.UI
{   
    public class SettingsMenu : MonoBehaviour 
    {
        public AudioMixer audioMixer;
        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("volume", volume);
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }
    }
}