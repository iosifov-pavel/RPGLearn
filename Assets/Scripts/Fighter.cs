using UnityEngine;
using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using RPG.Stats;
using System.Collections.Generic;

namespace RPG.Combat{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] WeaponConfig defaultWeapon = null;
        WeaponConfig currentWeaponConfig = null;
        Weapon currentWeapon = null;

        Health target=null;
        float timeFromLastAttack = Mathf.Infinity;

        private void Awake() {
            currentWeaponConfig = defaultWeapon;
            currentWeapon = EquipWeapon(defaultWeapon);
        }

        private void Start() {
            //EquipWeapon(currentWeaponConfig);
        }
        private void Update() {
            timeFromLastAttack+=Time.deltaTime;
            if(target!=null){
                if(target.GetComponent<Health>().IsDead()){
                    StopAttack();
                    return;
                } 
                LookAtEnemy();
                if(InRange(target.transform))
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

        bool InRange(Transform target){
            float range = (target.transform.position-transform.position).magnitude;
            return range<=currentWeaponConfig.WeaponRange;
        }

        public bool CanAttack(GameObject target){
            if(target.GetComponent<Health>().IsDead() || target==null) return false;
            if(!GetComponent<Mover>().CanMoveTo(target.transform.position) && !InRange(target.transform)) return false;
            else return true;
        }

        public Health GetTarget(){
            return target;
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
            if(currentWeapon!=null){
                currentWeapon.OnHit();
            }
            target.TakeDamage(gameObject, GetComponent<BaseStats>().GetStat(Stat.Damage));
        }

        void Shoot(){
            if (target == null) return;
            if (currentWeapon != null)
            {
                currentWeapon.OnHit();
            }
            if(currentWeaponConfig.HasProjectile()){
                currentWeaponConfig.LaunchProjectile(rightHandTransform,leftHandTransform,target, gameObject,GetComponent<BaseStats>().GetStat(Stat.Damage));
            }
        }

        public Weapon EquipWeapon(WeaponConfig weapon){
            Animator animator = GetComponent<Animator>();
            currentWeaponConfig = weapon;
            currentWeapon = weapon.Spawn(rightHandTransform, leftHandTransform, animator);
            return currentWeapon;
        }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            WeaponConfig weapon = Resources.Load<WeaponConfig>(weaponName);
            EquipWeapon(weapon);
        }

        public IEnumerable<float> GetAditiveModifier(Stat stat)
        {
            if(stat == Stat.Damage){
                yield return currentWeaponConfig.Damage;
            }
        }

        public IEnumerable<float> GetPercentModifier(Stat stat)
        {
            if(stat == Stat.Damage){
                yield return currentWeaponConfig.GetPercentBonus();
            }
        }
    }
}