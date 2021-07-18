using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class TargetingStrategy : ScriptableObject {
    public delegate void Targets();
    public abstract void StartTargeting(AbilityData data, Targets finished);
}
