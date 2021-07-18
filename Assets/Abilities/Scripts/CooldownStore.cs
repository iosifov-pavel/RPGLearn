using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

public class CooldownStore : MonoBehaviour
{
    Dictionary<InventoryItem, float> timers = new Dictionary<InventoryItem, float>();
    Dictionary<InventoryItem, float> maxTimers = new Dictionary<InventoryItem, float>();

    private void Update() {
        var timersL = new List<InventoryItem>(timers.Keys);
        foreach(InventoryItem InventoryItem in timersL){
            timers[InventoryItem]-=Time.deltaTime;
            if(timers[InventoryItem]<=0) timers.Remove(InventoryItem);
        }
    }

    public void StartCooldown(InventoryItem inventoryItem, float time){
        timers[inventoryItem] = time;
        if(!maxTimers.ContainsKey(inventoryItem)){
            maxTimers[inventoryItem] = time;
        }
    }

    public float GetCooldownTime(InventoryItem InventoryItem){
        if(!timers.ContainsKey(InventoryItem)) return 0;
        return timers[InventoryItem];
    }

    public float GetPercentage(InventoryItem inventoryItem)
    {
        if(!timers.ContainsKey(inventoryItem)) return 0;
        return 1 - (maxTimers[inventoryItem] - timers[inventoryItem]) / maxTimers[inventoryItem];
    }
}
