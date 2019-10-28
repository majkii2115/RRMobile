using UnityEngine;
using RPG.Resources;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject 
    {
    [SerializeField] AnimatorOverrideController animatorOverride = null;
    [SerializeField] Weapon weapon = null;
    [SerializeField] float range = 2f;
    [SerializeField] float weaponDamage = 5f;
    [SerializeField] float percentageBonus = 0;
    [SerializeField] bool isRightHanded = true;
    [SerializeField] Projectile projectile = null;
    const string weaponName = "Weapon";
        public Weapon Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            Weapon weapoon = null;
            DestroyOldWeapon(rightHand, leftHand);
            if(weapon != null)
            {
                Transform hand = GetTransform(rightHand, leftHand);
                weapoon = Instantiate(weapon, hand);
                weapoon.gameObject.name = weaponName;
            }
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if(overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
            return weapon;
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if(oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if(oldWeapon == null)
            {
                return;
            }
            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);        
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform hand;
            if (isRightHanded) hand = rightHand;
            else hand = leftHand;
            return hand;
        }
        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void SpawnProjectile(GameObject instigator, Transform rightHand, Transform leftHand, Health target, float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(instigator, target, calculatedDamage);
        }

        public float GetDamage()
        {
            return weaponDamage;
        } 

        public float GetPercentageBonus()
        {

            return percentageBonus;
        }
        public float GetRange()
        {
            return range;
        }
    }
}