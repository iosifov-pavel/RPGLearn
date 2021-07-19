using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Delay", menuName = "RPGLearn/Abilities/Effects/Delay", order = 0)]
public class Delay : EffectStrategy
{
    [SerializeField] float delay = 0;
    [SerializeField] EffectStrategy[] effects;
    [SerializeField] bool neetToCancel = false;
    public override void StartEffect(AbilityData data, Applied finished)
    {
        data.StartCoroutine(DelayedEffect(data,finished));
    }

    private IEnumerator DelayedEffect(AbilityData data, Applied finish){
        yield return new WaitForSeconds(delay);
        if(data.IsCancelled()&& neetToCancel) yield break; 
        foreach(EffectStrategy effect in effects){
            effect.StartEffect(data,finish);
        }
    }
}
