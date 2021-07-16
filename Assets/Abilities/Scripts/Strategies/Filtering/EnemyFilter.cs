using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFilter", menuName = "RPGLearn/Abilities/Filtering/EnemyFilter", order = 0)]
public class EnemyFilter : FilterStrategy
{
    [SerializeField] string filterTag = "Enemy";
    public override IEnumerable<GameObject> Filter(IEnumerable<GameObject> toFilter)
    {
        foreach(GameObject obj in toFilter){
            if(obj.CompareTag(filterTag)){
                yield return obj;
            }
        }
    }
}
