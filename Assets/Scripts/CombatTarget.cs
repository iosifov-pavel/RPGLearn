using UnityEngine;
using RPG.Core;
using RPG.Control;

namespace RPG.Combat{
    //[RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        private void Start() {
            if(GetComponent<AISpeaker>()){
                this.enabled=false;
            }
        }
        public Cursors GetCursorType()
        {
            return Cursors.Combat;
        }

        // public void Attack()
        // {
        //     Debug.Log("Attack");
        // }
        public bool HandleRaycast(PlayerController controler)
        {
            if(!enabled) return false;
            if(!controler.GetComponent<Fighter>().CanAttack(gameObject)) return false;
            if(Input.GetMouseButton(0)){
                controler.GetComponent<Fighter>().Attack(gameObject);
            }                   
            return true; 
        }
    }
}