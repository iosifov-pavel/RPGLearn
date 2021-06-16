using UnityEngine;
using RPG.Stats;

[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
public class Progression : ScriptableObject {
    [SerializeField] ProgressionCharacterClass[] characterClasses = null;

    public float GetHealth(CharacterClasses cClass, int level){
        float health = 0;
        foreach(ProgressionCharacterClass cls in characterClasses){
            if(cls.characterClass==cClass){
                health = cls.GetHealthProg(level-1);
            }
        }
        return health;
    }


    [System.Serializable]
    public class ProgressionCharacterClass{
        public CharacterClasses characterClass;
        public int[] health;

        public int GetHealthProg(int index){
            if(index<0) return 0;
            return health[index];
        }
    }
    
}