using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using System;
using UnityEngine.EventSystems;

namespace RPG.Control{
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Health health;

    enum Cursors{
        None,
        Movement,
        Combat,
        UI
    }
    [System.Serializable]
    struct CursorMaping{
        public Cursors type;
        public Texture2D texture;
        public Vector2 hotspot;
    }


    [SerializeField] CursorMaping[] cursorsMap = null;
        private void Awake()
    {
        health = GetComponent<Health>();
    }



    // Update is called once per frame
    void Update()
        {
            if(InteractWithUI()){
                SetCursor(Cursors.UI);
                return;
            }
            if(health.IsDead()){
                SetCursor(Cursors.None);
                return;
            } 
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
            SetCursor(Cursors.None);
        }

        private bool InteractWithUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
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
                    SetCursor(Cursors.Combat);
                    return true;
                }
            }
            return false;
        }

        private void SetCursor(Cursors cursor)
        {
            CursorMaping mapping = GetCursorsMap(cursor);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMaping GetCursorsMap(Cursors type){
            foreach(CursorMaping Ccursor in cursorsMap){
                if(Ccursor.type==type){
                    return Ccursor;
                }
            }
            return cursorsMap[0];
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
                SetCursor(Cursors.Movement);
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