using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

public class StatsEquipment : Equipment, IModifierProvider
{
    public IEnumerable<float> GetAditiveModifier(Stat stat)
    {
        foreach(var slot in GetAllPopulatedSlots()){
            var item = GetItemInSlot(slot) as IModifierProvider;
            if(item==null) continue;
            foreach (float modifier in item.GetAditiveModifier(stat))
            {
                yield return modifier;
            }
        }
    }

    public IEnumerable<float> GetPercentModifier(Stat stat)
    {
        foreach (var slot in GetAllPopulatedSlots())
        {
            var item = GetItemInSlot(slot) as IModifierProvider;
            if (item == null) continue;
            foreach (float modifier in item.GetPercentModifier(stat))
            {
                yield return modifier;
            }
        }
    }

}
