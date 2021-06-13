using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Combat{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;
        Weapon currentWeapon = null;

        Health target=null;
        float timeFromLastAttack = Mathf.Infinity;

        private void Start() {
            EquipWeapon(defaultWeapon);
        }
        private void Update() {
            timeFromLastAttack+=Time.deltaTime;
            if(target!=null){
                if(target.GetComponent<Health>().IsDead()){
                    StopAttack();
                    return;
                } 
                LookAtEnemy();
                float range = (target.transform.position-transform.position).magnitude;
                if(range<=currentWeapon.WeaponRange)
                {
                    GetComponent<Mover>().Cancel();
                    if(timeFromLastAttack>=timeBetweenAttacks){
                        AttackBehaviuor();
                        timeFromLastAttack = 0;
                        //trigger Hit
                    }    
                }
                else GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
        }

        public bool CanAttack(GameObject target){
            if(target.GetComponent<Health>().IsDead() || target==null) return false;
            else return true;
        }

        void LookAtEnemy(){
            transform.LookAt(target.transform);
        }

        private void AttackBehaviuor()
        {
            GetComponent<Animator>().ResetTrigger("stop attack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        public void Attack(GameObject combatTarget){
            GetComponent<Scheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
            Debug.Log("Attack");
        }

        void StopAttack(){
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stop attack");
        }

        public void Cancel(){
            StopAttack();
            target=null;
            GetComponent<Mover>().Cancel();
        }

        void Hit(){
            if(target==null) return;
            target.TakeDamage(currentWeapon.Damage);
        }

        void Shoot(){
            if (target == null) return;
            if(currentWeapon.HasProjectile()){
                currentWeapon.LaunchProjectile(rightHandTransform,leftHandTransform,target);
            }
        }

        public void EquipWeapon(Weapon weapon){
            if(weapon == null) return;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
            currentWeapon = weapon;
        }
    }
}