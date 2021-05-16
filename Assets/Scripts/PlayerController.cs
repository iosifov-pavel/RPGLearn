using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using System;

namespace RPG.Control{
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
        {
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] rays =  Physics.RaycastAll(GetRay());
            foreach(RaycastHit hit in rays){
                if(hit.transform.GetComponent<CombatTarget>()){
                    if(Input.GetMouseButtonDown(0)){
                        GetComponent<Fighter>().Attack(hit.transform.GetComponent<CombatTarget>());
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
                    GetComponent<Mover>().StartMoveAction(hit.point);
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