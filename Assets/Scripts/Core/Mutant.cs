using RPG.Resources;
using UnityEngine;

namespace RPG.Core
{   
    public class Mutant : MonoBehaviour 
    {
        [SerializeField] GameObject hud;
        [SerializeField] GameObject final;
        Health health;
        private void Awake() 
        {
            health = GetComponent<Health>();
        }
        private void Update() 
        {
            if(health.IsDead())
            {
                hud.SetActive(false);
                final.SetActive(true);
            }    
        }
    }
}