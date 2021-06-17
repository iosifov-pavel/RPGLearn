using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Stats;

namespace RPG.Core{
    public class Health : MonoBehaviour, ISaveable
    {
    // Start is called before the first frame update
        [SerializeField]float health = 100f;
        bool isDead = false;
        BaseStats stats;

        public void TakeDamage(GameObject instigator, float damage){
            if(isDead) return;
            health = Mathf.Max(health-damage,0);
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

        public bool IsDead(){
            return isDead;
        }
        void Start()
        {
            stats = GetComponent<BaseStats>();
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
