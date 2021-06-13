using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;

public class weaponPickup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Weapon weaponPickUp = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag=="Player"){
            other.GetComponent<Fighter>().EquipWeapon(weaponPickUp);
            Destroy(gameObject);
        }
    }
}
