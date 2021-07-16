using System.Collections.Generic;
using UnityEngine;
public abstract class EffectStrategy : ScriptableObject {
    
    public delegate void Applied();
    public abstract void StartEffect(GameObject user, IEnumerable<GameObject> targets, Applied finished);
}