using UnityEngine;

namespace RPG.Control
{
    
    public class simpleHealth : MonoBehaviour 
    {
        public int healthPoints = 100;

        public void Damage()
        {
            Debug.Log("dziala DMG");
            healthPoints -= 10;
        }
        private void Update() 
        {
            Debug.Log(healthPoints);
            if(healthPoints <= 0 )
            {
                GetComponent<Animator>().SetTrigger("die");
            }    
        }
    }
}