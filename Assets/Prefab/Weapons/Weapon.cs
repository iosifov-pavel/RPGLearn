using System;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float damage = 5f;
        [SerializeField] bool isRightHand = true;
        [SerializeField] Projectile projectile = null;
        const string weaponName = "Weapon";

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator){
            DestroyOldWeapon(rightHand,leftHand);
            if(weaponPrefab != null)
            {
                Transform hand = GetHand(rightHand, leftHand);
                GameObject weapon = Instantiate(weaponPrefab,hand);
                weapon.name = weaponName;
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

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator){
            Projectile projectileInstance = Instantiate(projectile, GetHand(rightHand,leftHand).position,Quaternion.identity);
            projectileInstance.SetTarget(target, damage, instigator);
        }

        public float Damage{
            get{ return damage;}
        }

        public float WeaponRange{
            get{ return weaponRange;}
        }


    }
}