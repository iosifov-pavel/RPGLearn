using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 3f;
    [SerializeField] bool isHoming = false;
    [SerializeField] float lifeTime = 4f;
    [SerializeField] GameObject hitEffect = null;
    Health target = null;
    GameObject instigator = null;
    float damage =0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -=Time.deltaTime;
        if(lifeTime<0) Destroy(gameObject);
        if(target==null) return;
        if(isHoming && !target.IsDead()){
            transform.LookAt(GetAimLocation());
        }
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }

    public void SetTarget(Health targetIN, float damageIN, GameObject instigatorIN){
        target = targetIN;
        damage = damageIN;
        instigator = instigatorIN;
        transform.LookAt(GetAimLocation());
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
        if(other.gameObject==target.gameObject && !target.IsDead()){
            target.TakeDamage(instigator, damage);
            if(hitEffect!=null){
                GameObject effect = Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }
            Destroy(gameObject);
        }
    }

}
