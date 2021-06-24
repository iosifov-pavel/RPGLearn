using System;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] Weapon weaponPrefab = null;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float damage = 5f;
        [SerializeField] float perecentBonus = 0f;
        [SerializeField] bool isRightHand = true;
        [SerializeField] Projectile projectile = null;
        const string weaponName = "Weapon";

        public Weapon Spawn(Transform rightHand, Transform leftHand, Animator animator){
            DestroyOldWeapon(rightHand,leftHand);
            Weapon weapon = null;
            if(weaponPrefab != null)
            {
                Transform hand = GetHand(rightHand, leftHand);
                weapon = Instantiate(weaponPrefab,hand);
                weapon.gameObject.name = weaponName;
            }
            if (animatorOverride != null){
                animator.runtimeAnimatorController = animatorOverride;
            }
            else{
                var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
                if(overrideController!=null){
                    animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
                }
            }
            return weapon;
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform curentWeapon = rightHand.Find(weaponName);
            if(curentWeapon==null){
                curentWeapon = leftHand.Find(weaponName);
            }
            if(curentWeapon==null) return;
            curentWeapon.name = "DESTROYING";
            Destroy(curentWeapon.gameObject);
        }

        private Transform GetHand(Transform rightHand, Transform leftHand)
        {
            if (isRightHand) return rightHand;
            else return leftHand;
        }

        public bool HasProjectile(){
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage){
            Projectile projectileInstance = Instantiate(projectile, GetHand(rightHand,leftHand).position,Quaternion.identity);
            projectileInstance.SetTarget(target, calculatedDamage, instigator);
        }

        public float Damage{
            get{ return damage;}
        }

        public float WeaponRange{
            get{ return weaponRange;}
        }

        public float GetPercentBonus(){
            return perecentBonus;
        }
    }
}