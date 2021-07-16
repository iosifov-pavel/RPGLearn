using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class TargetingStrategy : ScriptableObject {
    public delegate void Targets(IEnumerable<GameObject> targets);
    public abstract void StartTargeting(GameObject user, Targets finished);
}
