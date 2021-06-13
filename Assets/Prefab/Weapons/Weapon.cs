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

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator){
            if(weaponPrefab != null){
                if(isRightHand) Instantiate(weaponPrefab, rightHand);
                else Instantiate(weaponPrefab, leftHand);
            }
            if(animatorOverride != null){
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        public float Damage{
            get{ return damage;}
        }

        public float WeaponRange{
            get{ return weaponRange;}
        }


    }
}