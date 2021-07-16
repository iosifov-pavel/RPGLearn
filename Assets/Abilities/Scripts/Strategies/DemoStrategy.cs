using UnityEngine;

[CreateAssetMenu(fileName = "DemoStrategy", menuName = "RPGLearn/Abilities/Targeting/Demo", order = 0)]
public class DemoStrategy : TargetingStrategy {
    public override void StartTargeting()
    {
        Debug.Log("AAAAAAAAA");
    }
}
