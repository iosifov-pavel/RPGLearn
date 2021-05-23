using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Combat{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float damage = 5f;
        Health target=null;
        float timeFromLastAttack = Mathf.Infinity;
        private void Update() {
            timeFromLastAttack+=Time.deltaTime;
            if(target!=null){
                if(target.GetComponent<Health>().IsDead()){
                    StopAttack();
                    return;
                } 
                LookAtEnemy();
                float range = (target.transform.position-transform.position).magnitude;
                if(range<=weaponRange)
                {
                    GetComponent<Mover>().Cancel();
                    if(timeFromLastAttack>=timeBetweenAttacks){
                        AttackBehaviuor();
                        timeFromLastAttack = 0;
                        //trigger Hit
                    }    
                }
                else GetComponent<Mover>().MoveTo(target.transform.position);
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
        }

        void Hit(){
            if(target==null) return;
            target.TakeDamage(damage);
        }
    }
}