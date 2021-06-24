using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] UnityEvent hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit(){
        hit.Invoke();
        print("Hit"+gameObject.name);
    }
}
