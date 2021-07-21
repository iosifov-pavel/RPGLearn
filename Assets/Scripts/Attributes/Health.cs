using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Saving;
using RPG.Stats;
using UnityEngine.Events;

namespace RPG.Core{
    public class Health : MonoBehaviour, ISaveable
    {
    // Start is called before the first frame update
        float health = -1f;
        [SerializeField] TakeDamageEvent takeDamage;
        [SerializeField] TakeDamageEvent updateBar;
        [SerializeField] public UnityEvent onDie;
        bool wasDeadLastFrame = false;
        BaseStats stats;
        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>{

        }

        public void TakeDamage(GameObject instigator, float damage){
            if(IsDead()) return;
            print(gameObject.name + " D "+ damage);
            health = Mathf.Max(health-damage,0);
            float percent = GetPercentage()/100f;
            if(health<=0)
            {
                if(instigator.GetComponent<Expierence>()){
                    instigator.GetComponent<Expierence>().GainExpierence(stats.GetStat(Stat.ExpierenceReward));
                }
                onDie.Invoke();
            }
            else{
                takeDamage.Invoke(damage);
            }
            UpdateState();
            updateBar.Invoke(percent);
            print("HP: "+health);
        }

        public void Heal(GameObject instigator, float amount){
            health += amount;
            if(health>GetMAXHp()) health = GetMAXHp();
            UpdateState();
        }

        private void UpdateState()
        {
            if(!wasDeadLastFrame && IsDead()){
                GetComponent<Animator>().SetTrigger("dead");
                GetComponent<Scheduler>().CancelCurrentAction();
            }
            if(wasDeadLastFrame && !IsDead()){
                GetComponent<Animator>().Rebind();
            }
            wasDeadLastFrame = IsDead();
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
            return health<=0;
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
                UpdateState();
            }
        }
    }
}
