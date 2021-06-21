using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Control;
using UnityEngine;

public class weaponPickup : MonoBehaviour, IRaycastable
{
    // Start is called before the first frame update
    [SerializeField] Weapon weaponPickUp = null;
    [SerializeField] float respawnTime = 4f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag=="Player")
        {
            Pickup(other.GetComponent<Fighter>());
        }
    }

    private void Pickup(Fighter fighter)
    {
        fighter.EquipWeapon(weaponPickUp);
        StartCoroutine(HideForSeconds(respawnTime));
    }

    IEnumerator HideForSeconds(float sec){
        GetComponent<SphereCollider>().enabled = false;
        foreach(Transform entity in transform){
            entity.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(sec);
        GetComponent<SphereCollider>().enabled = true;
        foreach(Transform entity in transform){
            entity.gameObject.SetActive(true);
        }
    }

    public bool HandleRaycast(PlayerController controler)
    {
        if(Input.GetMouseButtonDown(0)){
            Pickup(controler.GetComponent<Fighter>());
        }
        return true;
    }

    public Cursors GetCursorType()
    {
        return Cursors.Pickup;
    }
}
