using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement{

public class Mover : MonoBehaviour, IAction
{
    // Start is called before the first frame upda
    [SerializeField] Animator animator;
    Health health;
    NavMeshAgent nav;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        nav.enabled = !health.IsDead();
        SetAnimation();
    }

    void SetAnimation(){
        float speed = transform.InverseTransformDirection(nav.velocity).z;
        animator.SetFloat("Speed", speed);
    }



    public void StartMoveAction(Vector3 destination){
        GetComponent<Scheduler>().StartAction(this);
        MoveTo(destination);
    }

    public void MoveTo(Vector3 destination)
    {
        nav.SetDestination(destination);
        nav.isStopped = false;
    }

    public void Cancel(){
        nav.isStopped = true;
    }
}
}