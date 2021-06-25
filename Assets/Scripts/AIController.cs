using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace  RPG.Control
{
public class AIController : MonoBehaviour
{
    [SerializeField] float chaseDistance = 4f;
    Fighter fighter;
    Mover mover;
    GameObject player;
    Health health;
    Vector3 guardPosition;
    float timeSinseLastSawPlayer = Mathf.Infinity;
    float timeAtPoint = Mathf.Infinity;
    float timeSinseAggro = Mathf.Infinity;
    [SerializeField] float timeWaitAtPoint = 1.5f;
    [SerializeField] float suspiciousTime = 2f;
    [SerializeField] float aggroTime =5f;
    [SerializeField] PatrolPath path = null;
    [SerializeField] float wayPointTolerance = 1f;
    [Range(0,1f)] [SerializeField] float patrolSpeedMultiplier = 0.2f;
    int currentPointNumber = 0;
    [SerializeField] float agroradius = 5;

        // Start is called before the first frame update
        private void Awake() {
        fighter = GetComponent<Fighter>();
        mover = GetComponent<Mover>();
        player = GameObject.FindWithTag("Player");
        health = GetComponent<Health>();
    }
    void Start()
    {
        guardPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if(health.IsDead()) return;
        if (DistanceToPlayer(player) && fighter.CanAttack(player))
        {
            print(gameObject.name + "Start chasing");
            timeSinseLastSawPlayer = 0;
            fighter.Attack(player);
            AggroFriends();
        }
        else if(timeSinseLastSawPlayer<=suspiciousTime){
            GetComponent<Scheduler>().CancelCurrentAction();
        }
        else{
            fighter.Cancel();
            Vector3 nextPosition = guardPosition;
            if(path!=null){
                if(AtWaypoint()){
                    timeAtPoint = 0;
                    ToNextPoint();
                }
                nextPosition = GetCurrentWayPoint();
            }
            if(timeAtPoint>=timeWaitAtPoint){
                mover.StartMoveAction(nextPosition, patrolSpeedMultiplier);
            }
        }
        timeSinseLastSawPlayer += Time.deltaTime;
        timeAtPoint += Time.deltaTime;
        timeSinseAggro +=Time.deltaTime;
    }

        private void AggroFriends()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, agroradius, Vector3.up, 0);
            foreach(RaycastHit hit in hits){
                AIController enemy = hit.transform.GetComponent<AIController>();
                if(enemy){
                    enemy.Aggro();
                }
            }
        }

        public void Aggro(){
        timeSinseAggro = 0f;
    }

    private Vector3 GetCurrentWayPoint()
    {
        return path.GetWayPoint(currentPointNumber);
    }
    private void ToNextPoint()
    {
        currentPointNumber = path.GetNextWayPoint(currentPointNumber);
    }
    private bool AtWaypoint()
    {
        float distanceToPoint = Vector3.Distance(transform.position,GetCurrentWayPoint());
        return distanceToPoint<= wayPointTolerance;
    }
    private bool DistanceToPlayer(GameObject player)
    {
            bool v = Vector3.Distance(player.transform.position, transform.position) <= chaseDistance;
            bool w = timeSinseAggro < aggroTime;
            return v||w;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,chaseDistance);
    }
}
}
