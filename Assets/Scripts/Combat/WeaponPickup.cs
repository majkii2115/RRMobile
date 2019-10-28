using System.Collections;
using RPG.Control;
using RPG.Resources;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weapon = null;
        [SerializeField] GameObject player = null;
        [SerializeField] float healthToRestore = 0;
        private void OnTriggerEnter(Collider other) 
        {
        if(other.gameObject.tag == "Player")
            {
                Pickup(other.gameObject);
            }
        }
        private void Pickup(GameObject subject)
        {
            if(weapon != null)
            {
                subject.GetComponent<Fighter>().EquipWeapon(weapon);
            }
            if(healthToRestore > 0)
            {
                subject.GetComponent<Health>().Heal(healthToRestore);
            }
            ShowPickup(false);
        }

        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        public bool HandleRaycast(PlayerController controller)
        {
            if(Input.GetMouseButton(0))
            {
                Pickup(controller.gameObject);
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }    
}