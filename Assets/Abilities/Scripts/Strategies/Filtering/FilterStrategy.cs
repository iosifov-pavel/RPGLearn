using System.Collections.Generic;
using UnityEngine;

public abstract class FilterStrategy : ScriptableObject {
    
    //public delegate IEnumerable<GameObject> FilteredTargets(IEnumerable<GameObject> targets);
    public abstract IEnumerable<GameObject> Filter(IEnumerable<GameObject> toFilter);
}
