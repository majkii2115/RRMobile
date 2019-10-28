using UnityEngine;

namespace RPG.UI
{
    public class HUDDisabler : MonoBehaviour 
    {
        [SerializeField] GameObject HUD;
        private void OnTriggerEnter(Collider other) 
        {
            HUD.SetActive(false);
        }
        public void DisableHUD()
        {
            HUD.SetActive(false);
        }
    }
}