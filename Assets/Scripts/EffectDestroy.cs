using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject target = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GetComponent<ParticleSystem>().IsAlive()){
            if(target!=null){
                Destroy(target);
            }
            else Destroy(gameObject);
        }
    }
}
