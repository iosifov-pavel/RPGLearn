using UnityEngine;
using RPG.Movement;

namespace RPG.Combat{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;
        Transform target=null;
        private void Update() {
            if(target!=null){
                float range = (target.position-transform.position).magnitude;
                if(range<=weaponRange){
                    GetComponent<Mover>().StopMove();
                } 
                else GetComponent<Mover>().MoveTo(target.position);
            }
        }
        public void Attack(CombatTarget combatTarget){
            target = combatTarget.transform;
            Debug.Log("Attack");
        }

        public void CancelAttack(){
            target=null;
        }
    }
}