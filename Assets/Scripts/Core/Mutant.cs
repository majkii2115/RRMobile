using RPG.Resources;
using UnityEngine;

namespace RPG.Core
{   
    public class Mutant : MonoBehaviour 
    {
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
                final.SetActive(true);
            }    
        }
    }
}