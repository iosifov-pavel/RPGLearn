using UnityEngine;

namespace RPG.Combat{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject weaponPrefab = null;

        public void Spawn(Transform hand, Animator animator){
            Instantiate(weaponPrefab, hand);
            animator.runtimeAnimatorController = animatorOverride;
        }
    }
}