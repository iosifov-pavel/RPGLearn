using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using UnityEngine.Events;

namespace RPG.Core{
    public class Health : MonoBehaviour, ISaveable
    {
    // Start is called before the first frame update
        float health = -1f;
        [SerializeField] TakeDamageEvent takeDamage;
        [SerializeField] TakeDamageEvent updateBar;
        bool isDead = false;
        BaseStats stats;
        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>{

        }

        public void TakeDamage(GameObject instigator, float damage){
            if(isDead) return;
            print(gameObject.name + " D "+ damage);
            health = Mathf.Max(health-damage,0);
            float percent = GetPercentage()/100f;
            takeDamage.Invoke(damage);
            updateBar.Invoke(percent);
            if(health<=0)
            {
                if(instigator.GetComponent<Expierence>()){
                    instigator.GetComponent<Expierence>().GainExpierence(stats.GetStat(Stat.ExpierenceReward));
                }
                Die();
            }
            print("HP: "+health);
        }

        private void Die()
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("dead");
            GetComponent<Scheduler>().CancelCurrentAction();
        }

        public float GetPercentage(){
            return (health / stats.GetStat(Stat.Health))*100;
        }

        public float GetHP(){
            return health;
        }

        public float GetMAXHp(){
            return stats.GetStat(Stat.Health);
        }

        public bool IsDead(){
            return isDead;
        }
        void Start()
        {
            stats = GetComponent<BaseStats>();
            if(health<0){
                health = stats.GetStat(Stat.Health);
            }
            stats.onLevelUp+=HealOnLevelUp;
        }

        void HealOnLevelUp(){
            health = stats.GetStat(Stat.Health);
        }
        // Update is called once per frame
        void Update()
        {

        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float)state ;
            if(health==0){
                Die();
            }
        }
    }
}
