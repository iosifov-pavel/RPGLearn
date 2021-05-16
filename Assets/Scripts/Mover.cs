using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using UnityEngine.AI;

namespace RPG.Movement{

public class Mover : MonoBehaviour
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

    public void StopMove(){
        nav.isStopped = true;
    }

    public void StartMoveAction(Vector3 destination){
        GetComponent<Fighter>().CancelAttack();
        MoveTo(destination);
    }

    public void MoveTo(Vector3 destination)
    {
        nav.SetDestination(destination);
        nav.isStopped = false;
    }
}
}