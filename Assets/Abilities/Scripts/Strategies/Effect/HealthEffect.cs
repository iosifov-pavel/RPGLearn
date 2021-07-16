using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthEffect", menuName = "RPGLearn/Abilities/Effects/HealthEffect", order = 0)]
public class HealthEffect : EffectStrategy
{
    [SerializeField] float healthAmount = 10;
    [SerializeField] bool isHealing = false;
    public override void StartEffect(GameObject user, IEnumerable<GameObject> targets, Applied finished)
    {
        foreach(GameObject target in targets){
            Health targetHealth = target.GetComponent<Health>();
            if(targetHealth){
                healthAmount = isHealing ? -healthAmount : healthAmount;
                targetHealth.TakeDamage(user,healthAmount);
            }
        }
    }
}