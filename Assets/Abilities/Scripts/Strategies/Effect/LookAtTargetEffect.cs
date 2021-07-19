using UnityEngine;

[CreateAssetMenu(fileName = "LookAtTargetEffect", menuName = "RPGLearn/Abilities/Effects/LookAtTarget", order = 0)]
public class LookAtTargetEffect : EffectStrategy
{
    public override void StartEffect(AbilityData data, Applied finished)
    {
        Transform player = data.GetUser().transform;
        player.LookAt(data.GetPoint());
    }
}