using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DemoStrategy", menuName = "RPGLearn/Abilities/Targeting/Demo", order = 0)]
public class DemoStrategy : TargetingStrategy {
    public override void StartTargeting(GameObject user, Targets finished)
    {
        Debug.Log("AAAAAAAAA");
        finished(null);
    }
}
