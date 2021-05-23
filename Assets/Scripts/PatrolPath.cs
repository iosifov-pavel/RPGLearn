using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control{
public class PatrolPath : MonoBehaviour
{
    // Start is called before the first frame update
    const float waypointGizmosRadius = 0.4f;
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void OnDrawGizmos() {
        for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextWayPoint(i);
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(GetWayPoint(i), waypointGizmosRadius);
                Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(j));
            }
        }

        public int GetNextWayPoint(int i)
        {
            if(i==transform.childCount-1) return 0;
            else return i + 1;
        }

        public Vector3 GetWayPoint(int i)
        {
            return transform.GetChild(i).position;
        }


}
}
