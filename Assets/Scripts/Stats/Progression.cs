using UnityEngine;
using RPG.Stats;

[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
public class Progression : ScriptableObject {
    [SerializeField] ProgressionCharacterClass[] characterClasses = null;

    public float GetStat(Stat stat,CharacterClasses cClass, int level){
        foreach(ProgressionCharacterClass cls in characterClasses){
            if(cls.characterClass==cClass){
                foreach(ProgressionStat statP in cls.stats){
                    if(statP.stat==stat){
                        return statP.levels[level];
                    }
                }
            }
        }
        return 0;
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