using UnityEngine;
using GameDevTV.Inventories;

[CreateAssetMenu(fileName = "New Ability", menuName = "RPGLearn/Abilities/Ability", order = 0)]
public class Ability : ActionItem {
    [SerializeField] TargetingStrategy strategy;
    public override void Use(GameObject user)
    {
        strategy.StartTargeting();
    }
}
