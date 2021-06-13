using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 3f;
    Health target = null;
    float damage =0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target==null) return;
        transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }

    public void SetTarget(Health targetIN, float damageIN){
        target = targetIN;
        damage = damageIN;
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCC = target.GetComponent<CapsuleCollider>();
        if(targetCC==null){
            return target.transform.position;
        }
        return target.transform.position+ Vector3.up*targetCC.height/2;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject==target.gameObject){
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
