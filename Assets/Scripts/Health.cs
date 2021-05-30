using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Core{
    public class Health : MonoBehaviour, ISaveable
    {
    // Start is called before the first frame update
        [SerializeField]float health = 100f;
        bool isDead = false;

        public void TakeDamage(float damage){
            if(isDead) return;
            health = Mathf.Max(health-damage,0);
            if(health<=0)
            {
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

        public bool IsDead(){
            return isDead;
        }
        void Start()
        {

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
