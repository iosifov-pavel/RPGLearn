using System.Collections;
using System.Collections.Generic;
using GameDevTV.Saving;
using RPG.Stats;
using UnityEngine;

public class TraitsStore : MonoBehaviour, ISaveable, IModifierProvider
{
    Dictionary<Traits, int> traits = new Dictionary<Traits, int>();
    Dictionary<Traits, int> assignedTraits = new Dictionary<Traits, int>();
    Dictionary<Stat, Dictionary<Traits, float>> additiveBonus;
    Dictionary<Stat, Dictionary<Traits, float>> percentageBonus;
    [System.Serializable]
    class TraitsData{
        public Dictionary<Traits, int> traits = new Dictionary<Traits, int>();
        public int pointsLeft=0;
    }
    [System.Serializable]
    private class TraitBonus
    {
        public Traits trait;
        public Stat stat;
        public float additiveBonusPerPoint = 0;
        public float percentageBonusPerPoint = 5;
    }
    [SerializeField] TraitBonus[] config;
    [SerializeField] int unassignedPoints = 10;
    BaseStats stats=null;
    private void Awake() {
        additiveBonus = new Dictionary<Stat, Dictionary<Traits, float>>();
        percentageBonus = new Dictionary<Stat, Dictionary<Traits, float>>();
        foreach(TraitBonus bonus in config){
            if(!additiveBonus.ContainsKey(bonus.stat)){
                additiveBonus[bonus.stat] = new Dictionary<Traits, float>();
            }
            if(!percentageBonus.ContainsKey(bonus.stat)){
                percentageBonus[bonus.stat] = new Dictionary<Traits, float>();
            }
            additiveBonus[bonus.stat][bonus.trait] = bonus.additiveBonusPerPoint;
            percentageBonus[bonus.stat][bonus.trait] = bonus.percentageBonusPerPoint;  
        }
    }
    private void Start() {
        stats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        stats.onLevelUp+=LvlUpPoints;
    }

    public int GetProposedPoints(Traits trait){
        return GetPoints(trait) + GetAssignedPoints(trait);
    }

    public int GetPoints(Traits trait){
        if(!traits.ContainsKey(trait)) return 0;
        return traits[trait];
    }

    public int GetAssignedPoints(Traits trait){
        if(!assignedTraits.ContainsKey(trait)) return 0;
        return assignedTraits[trait];
    }

    public void SetTrait(Traits trait, int points){
        if(CanAssignPoints(trait,points)){
            traits[trait] = GetPoints(trait) + points;
            unassignedPoints-=points;
        }
    }

    public bool CanAssignPoints(Traits trait, int points){
        if(GetPoints(trait)+points < 0) return false;
        if(unassignedPoints<points) return false;
        return true;
    }

    public int GetUnPoints(){
        return unassignedPoints;
    }

    public void Confirm(){
        foreach(var trait in traits.Keys){
            if(!assignedTraits.ContainsKey(trait)) assignedTraits[trait] = traits[trait];
            else assignedTraits[trait] += traits[trait];
        }
        traits.Clear();
    }

    public void LvlUpPoints(){
        unassignedPoints = 10;
    }

    public object CaptureState()
    {
        TraitsData data = new TraitsData();
        data.pointsLeft = unassignedPoints;
        data.traits = assignedTraits;
        return data;
    }

    public void RestoreState(object state)
    {
        TraitsData data = (TraitsData)state;
        assignedTraits = data.traits;
        unassignedPoints = data.pointsLeft;
    }

    public IEnumerable<float> GetAditiveModifier(Stat stat)
    {
        if(!additiveBonus.ContainsKey(stat)) yield break;
        foreach(Traits trait in additiveBonus[stat].Keys){
            float bonus = additiveBonus[stat][trait];
            yield return bonus * GetAssignedPoints(trait);
        }
    }

    public IEnumerable<float> GetPercentModifier(Stat stat)
    {
        if(!percentageBonus.ContainsKey(stat)) yield break;
        foreach(Traits trait in percentageBonus[stat].Keys){
            float bonus = percentageBonus[stat][trait];
            yield return bonus * GetAssignedPoints(trait);
        }
    }

    
}
