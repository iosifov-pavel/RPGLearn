using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Inventories;


[CreateAssetMenu(menuName = "RPGLearn/StatsEquipableItem")]
public class StatsEquipableItem : EquipableItem, IModifierProvider
{
    [SerializeField] Modifier[] additiveMod;
    [SerializeField] Modifier[] percentgeMod;

    [System.Serializable]
    struct Modifier
    {
        public Stat stat;
        public float value;
    }

    public IEnumerable<float> GetAditiveModifier(Stat stat)
    {
        foreach (var modifier  in additiveMod)
        {
            if(modifier.stat==stat){
                yield return modifier.value;
            }
        }
    }

    public IEnumerable<float> GetPercentModifier(Stat stat)
    {
        foreach (var modifier in percentgeMod)
        {
            if (modifier.stat == stat)
            {
                yield return modifier.value;
            }
        }
    }
}
