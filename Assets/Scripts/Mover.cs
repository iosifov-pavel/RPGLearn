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
    NavMeshAgent nav;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

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