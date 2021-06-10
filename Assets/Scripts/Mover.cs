using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;

namespace RPG.Movement{

public class Mover : MonoBehaviour, IAction, ISaveable
{
    // Start is called before the first frame upda
    [SerializeField] Animator animator;
    [SerializeField] float maxSpeed = 6f;
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



    public void StartMoveAction(Vector3 destination, float speedMultiplier){
        GetComponent<Scheduler>().StartAction(this);
        MoveTo(destination, speedMultiplier);
    }

    public void MoveTo(Vector3 destination, float speedMultiplier)
    {
        nav.SetDestination(destination);
        nav.speed = maxSpeed * speedMultiplier;
        nav.isStopped = false;
    }

    public void Cancel(){
        nav.isStopped = true;
    }



        public object CaptureState()
        {
            Dictionary<string,object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            Dictionary<string,object> data = (Dictionary<string,object>) state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = ((SerializableVector3)data["position"]).ToVector();
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<Scheduler>().CancelCurrentAction();
        }
    }
}