using UnityEngine;
using GameDevTV.Inventories;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Ability", menuName = "RPGLearn/Abilities/Ability", order = 0)]
public class Ability : ActionItem {
    [SerializeField] TargetingStrategy targeting;
    [SerializeField] FilterStrategy[] filterings;
    [SerializeField] EffectStrategy[] effects;
    public override void Use(GameObject user)
    {
        targeting.StartTargeting(user, (IEnumerable<GameObject> targets)=>{
            TargetAqired(user,targets);
        });
    }

    private void TargetAqired(GameObject user, IEnumerable<GameObject> targets){
        if(targets==null){
            Debug.Log("Zero obj");
        }
        foreach(FilterStrategy strategy in filterings){
            targets = strategy.Filter(targets);
        }
        foreach(EffectStrategy effect in effects){
            effect.StartEffect(user, targets, EffectFinished);
        }
        
    }

    private void EffectFinished(){

    }
}
