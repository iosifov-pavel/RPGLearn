using UnityEngine;

[CreateAssetMenu(fileName = "SelfTarget", menuName = "RPGLearn/Abilities/Targeting/Self", order = 0)]
public class SelfTarget : TargetingStrategy
{
    public override void StartTargeting(AbilityData data, Targets finished)
    {
        data.SetTargets(new GameObject[] {data.GetUser()});
        data.SetPoint(data.GetUser().transform.position);
        finished();
    }
}