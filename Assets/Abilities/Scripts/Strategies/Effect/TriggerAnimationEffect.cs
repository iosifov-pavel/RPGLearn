using UnityEngine;

[CreateAssetMenu(fileName = "TriggerAnimationEffect", menuName = "RPGLearn/Abilities/Effects/AnimationTrigger", order = 0)]
public class TriggerAnimationEffect : EffectStrategy
{
    [SerializeField] string trigger = "casting";
    public override void StartEffect(AbilityData data, Applied finished)
    {
        Animator player = data.GetUser().GetComponent<Animator>();
        player.SetTrigger(trigger);
    }
}
