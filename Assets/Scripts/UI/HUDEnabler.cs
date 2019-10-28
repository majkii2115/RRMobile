using UnityEngine;

namespace RPG.UI
{
    public class HUDEnabler : MonoBehaviour 
    {
        [SerializeField] GameObject HUD;
        [SerializeField] GameObject secondHUD;
        bool isEnabled = false;
        private void OnTriggerEnter(Collider other) 
        {
            if(!isEnabled)
            {
            HUD.SetActive(true);
            secondHUD.SetActive(false);
            isEnabled = true;
            }
        }
        public void DisableHUD()
        {
            secondHUD.SetActive(true);
            HUD.SetActive(false);
        }
    }
}