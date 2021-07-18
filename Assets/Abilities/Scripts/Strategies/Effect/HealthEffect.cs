using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthEffect", menuName = "RPGLearn/Abilities/Effects/HealthEffect", order = 0)]
public class HealthEffect : EffectStrategy
{
    [SerializeField] float healthAmount = 10;
    [SerializeField] bool isHealing = false;
    public override void StartEffect(AbilityData data, Applied finished)
    {
        foreach(GameObject target in data.GetTargets()){
            Health targetHealth = target.GetComponent<Health>();
            if(targetHealth){
                healthAmount = isHealing ? -healthAmount : healthAmount;
                targetHealth.TakeDamage(data.GetUser(),healthAmount);
            }
        }
    }
}