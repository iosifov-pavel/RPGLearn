using UnityEngine;
using RPG.Stats;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
public class Progression : ScriptableObject {
    [SerializeField] ProgressionCharacterClass[] characterClasses = null;
    Dictionary<CharacterClasses, Dictionary<Stat, float[]>> statsTable = null;

    public float GetStat(Stat stat,CharacterClasses cClass, int level){
        BuildTable();
        if(statsTable.ContainsKey(cClass)){
            if(statsTable[cClass].ContainsKey(stat)){
                if(level>=statsTable[cClass][stat].Length) level = statsTable[cClass][stat].Length-1;
                return statsTable[cClass][stat][level-1];
            }
        }
        return 0;
    }

    private void BuildTable(){
        if(statsTable!=null) return;
        statsTable = new Dictionary<CharacterClasses, Dictionary<Stat, float[]>>();
        foreach(ProgressionCharacterClass cls in characterClasses){
            Dictionary<Stat, float[]> statDict = new Dictionary<Stat, float[]>();
            foreach(ProgressionStat statP in cls.stats){
                    statDict[statP.stat] = statP.levels;
            }
            statsTable[cls.characterClass] = statDict;
        }
    }

    public int GetLevels(Stat stat, CharacterClasses classs){
        BuildTable();
        float[] levels = statsTable[classs][stat];
        return levels.Length;
    }

    [System.Serializable]
    public class ProgressionCharacterClass{
        public CharacterClasses characterClass;
        public ProgressionStat[] stats;

        public int GetStat(int index){
            if(index<0) return 0;
            return 10;
        }
    }

    [System.Serializable]
    public class ProgressionStat{
        public Stat stat;
        public float[] levels;
    }
    
}