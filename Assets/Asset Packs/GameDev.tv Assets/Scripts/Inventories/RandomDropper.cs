using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;
using UnityEngine.AI;

public class RandomDropper : ItemDropper
{
    [SerializeField] float randomRadius = 2f;
    [SerializeField] InventoryItem[] dropLibrary;
    [SerializeField] int numberOfDrops = 2;
    protected override Vector3 GetDropLocation()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * randomRadius;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint, out hit, 0.1f, NavMesh.AllAreas)){
            return hit.position;
        }
        return base.GetDropLocation();
    }

    public void RandomDrop(){
        for (int i = 0; i < numberOfDrops; i++)
        {
            var item = dropLibrary[Random.Range(0, dropLibrary.Length)];
            DropItem(item, 1);
        }
    }
}
