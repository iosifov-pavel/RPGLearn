using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Stats{


public class BaseStats : MonoBehaviour
{
    [Range(1,99)][SerializeField] int startingLevel = 1;
    [SerializeField] CharacterClasses characterClass;
    [SerializeField] Progression progression;
    [SerializeField] GameObject LvlUpEffect;
    public event Action onLevelUp;
    int currentLevel = 0;
    private void Start() {
        currentLevel = CalculateLevel();
        Expierence expierence = GetComponent<Expierence>();
        if(expierence!=null){
            expierence.onExpGained+=UpdateLevel;
        }
    }

    public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1f + GetPercentModifier(stat)/100f);
        }

        private float GetPercentModifier(Stat stat)
        {
            float result =0;
            foreach(IModifierProvider provider in GetComponents<IModifierProvider>()){
                foreach(float modifiers in provider.GetPercentModifier(stat)){
                    result += modifiers;
                }
            }
            return result;
        }

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        private float GetAdditiveModifier(Stat stat)
        {
            float result =0;
            foreach(IModifierProvider provider in GetComponents<IModifierProvider>()){
                foreach(float modifiers in provider.GetAditiveModifier(stat)){
                    result += modifiers;
                }
            }
            return result;
        }

        private void UpdateLevel() {
        if(characterClass!=CharacterClasses.Player) return;
        int newLevel = CalculateLevel();
        if(newLevel>currentLevel){
            currentLevel = newLevel;
            LevelUpEffect();
            onLevelUp();
        }
    }

        private void LevelUpEffect()
        {
            Instantiate(LvlUpEffect,transform);
        }

        public int GetLevel(){
        if(currentLevel<1){
            currentLevel = CalculateLevel();
        }
        return currentLevel;
    }

    public int CalculateLevel(){
            Expierence expierence = GetComponent<Expierence>();
            if(expierence==null) return startingLevel;
            float currentXP = expierence.GetExpValue();
            int maxLevel = progression.GetLevels(Stat.ExpToLevelUp, characterClass);
            for (int levels = 1; levels <= maxLevel; levels++){
                var XPToUp = progression.GetStat(Stat.ExpToLevelUp,characterClass, levels);
                if(XPToUp > currentXP) {
                    return levels;
                } 
            }
            return maxLevel+1;
    }

    //public float GetExpierenceReward(){
    //    return progression.GetStat(Stat.Expierence,characterClass, startingLevel-1);
    //}
}



}
