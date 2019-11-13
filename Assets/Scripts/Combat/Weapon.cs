using UnityEngine;
using UnityEngine.Events;
using RPG.Control;

namespace RPG.Combat
{
    public class Weapon : MonoBehaviour 
    {
        [SerializeField] UnityEvent onHit;
        public void OnHit()
        {
            onHit.Invoke();
        }
    }
}