using UnityEngine;
using GameDevTV.Inventories;
using System.Collections.Generic;
using RPG.Core;

[CreateAssetMenu(fileName = "New Ability", menuName = "RPGLearn/Abilities/Ability", order = 0)]
public class Ability : ActionItem {
    [SerializeField] TargetingStrategy targeting;
    [SerializeField] FilterStrategy[] filterings;
    [SerializeField] EffectStrategy[] effects;
    [SerializeField] float cooldown = 2f;
    [SerializeField] float manaCost = 20;
    AbilityData data=null;
    Mana playerMana = null;
    public override void Use(GameObject user)
    {
        var store = user.GetComponent<CooldownStore>();
        playerMana = user.GetComponent<Mana>();
        if(playerMana==null) return;
        if(playerMana.GetMana()<manaCost) return;
        if(store.GetCooldownTime(this)!=0) return;
        data = new AbilityData(user);
        Scheduler scheduler = user.GetComponent<Scheduler>();
        scheduler.StartAction(data);
        targeting.StartTargeting(data, ()=>{
            TargetAqired(data);
        });
    }

    private void TargetAqired(AbilityData data){
        if(data.IsCancelled()) return;
        if(!playerMana.UseMana(manaCost)) return;
        var store = data.GetUser().GetComponent<CooldownStore>();
        store.StartCooldown(this, cooldown);
        if(data.GetTargets()==null){
            Debug.Log("Zero obj");
        }
        foreach(FilterStrategy strategy in filterings){
            data.SetTargets(strategy.Filter(data.GetTargets()));
        }
        foreach(EffectStrategy effect in effects){
            effect.StartEffect(data, EffectFinished);
        }
        
    }

    private void EffectFinished(){
    }
}
