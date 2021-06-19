using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using System;

namespace RPG.Control{
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Health health;
    private void Awake()
    {
        health = GetComponent<Health>();
    }



    // Update is called once per frame
    void Update()
        {
            if(health.IsDead()) return;
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] rays =  Physics.RaycastAll(GetRay());
            foreach(RaycastHit hit in rays){
                CombatTarget target= hit.transform.GetComponent<CombatTarget>();
                if(target!=null){
                    if(!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;
                    if(Input.GetMouseButton(0)){
                        GetComponent<Fighter>().Attack(target.gameObject);
                    }
                    return true;
                }
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetRay(), out hit);
            if (hasHit)
            {
                if(Input.GetMouseButton(0)){
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }
                return true;
            }
            return false;
        }

        private static Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}