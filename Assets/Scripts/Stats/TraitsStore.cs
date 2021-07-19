using System.Collections;
using System.Collections.Generic;
using GameDevTV.Saving;
using RPG.Stats;
using UnityEngine;

public class TraitsStore : MonoBehaviour, ISaveable
{
    Dictionary<Traits, int> traits = new Dictionary<Traits, int>();
    Dictionary<Traits, int> assignedTraits = new Dictionary<Traits, int>();
    [System.Serializable]
    class TraitsData{
        public Dictionary<Traits, int> traits = new Dictionary<Traits, int>();
        public int pointsLeft=0;
    }
    [SerializeField] int unassignedPoints = 10;
    BaseStats stats=null;
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
        TraitsData data = (TraitsData) state;
        assignedTraits = data.traits;
        unassignedPoints = data.pointsLeft;
    }
}
