using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnVisualsEffect", menuName = "RPGLearn/Abilities/Effects/SpawnVisualsEffect", order = 0)]
public class SpawnVisualsEffect : EffectStrategy {
    [SerializeField] Transform visualPrefab=null;
    [SerializeField] float destroyDelay = -1;
    [SerializeField] bool isParent = false;

    public override void StartEffect(AbilityData data, Applied finished)
    {
        data.StartCoroutine(Effect(data,finished));
    }

    private IEnumerator Effect(AbilityData data, Applied finished){
        Transform visual = Instantiate(visualPrefab);
        if(isParent) visual.SetParent(data.GetUser().transform, false);
        visual.position = data.GetPoint();
        if(destroyDelay>0){
            yield return new WaitForSeconds(destroyDelay);
            Destroy(visual.gameObject);
        }
        finished();
    }
}