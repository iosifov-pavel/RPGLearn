using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using System;
using UnityEngine.EventSystems;
using UnityEngine.AI;

namespace RPG.Control{
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Health health;


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
            if(InteractWithComponent()) return;
            if(InteractWithMovement()) return;
            SetCursor(Cursors.None);
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] rays =  RayCastSorted();
            foreach(RaycastHit hit in rays){
                IRaycastable[] raycasts = hit.transform.GetComponents<IRaycastable>();
                foreach(IRaycastable raycast in raycasts){
                    if(raycast.HandleRaycast(this)){
                        SetCursor(raycast.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit[] RayCastSorted(){
            RaycastHit[] hits = Physics.RaycastAll(GetRay());
            float[] distances = new float[hits.Length];
            for(int i=0;i<hits.Length;i++){
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances,hits);
            return hits;
        }

        private bool InteractWithUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
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
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target);
            if (hasHit)
            {
                if(!GetComponent<Mover>().CanMoveTo(target)) return false;
                if(Input.GetMouseButton(0)){
                    GetComponent<Mover>().StartMoveAction(target, 1f);
                }
                SetCursor(Cursors.Movement);
                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target){
            target = new Vector3();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetRay(), out hit);
            if(!hasHit) return false;
            NavMeshHit navMeshHit;
            bool hasToNavMesh = NavMesh.SamplePosition(hit.point, out navMeshHit, 1f, NavMesh.AllAreas);
            if(!hasToNavMesh) return false;
            target = navMeshHit.position;

            return true;
        }



        private static Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}