using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat{
    public class Health : MonoBehaviour
    {
    // Start is called before the first frame update
        [SerializeField]float health = 100f;
        bool isDead = false;

        public void TakeDamage(float damage){
            if(isDead) return;
            health = Mathf.Max(health-damage,0);
            if(health<=0){
                isDead=true;
                GetComponent<Animator>().SetTrigger("dead");
            } 
            print("HP: "+health);
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
    }
}
