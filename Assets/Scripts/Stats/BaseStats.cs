using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Stats{


public class BaseStats : MonoBehaviour
{
    [Range(1,99)][SerializeField] int startingLevel = 1;
    [SerializeField] CharacterClasses characterClass;
    [SerializeField] Progression progression;

    public float GetStat(Stat stat){
        return progression.GetStat(stat,characterClass, startingLevel-1);
    }

    //public float GetExpierenceReward(){
    //    return progression.GetStat(Stat.Expierence,characterClass, startingLevel-1);
    //}
}



}
