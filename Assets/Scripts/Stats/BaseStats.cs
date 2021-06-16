using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Stats{


public class BaseStats : MonoBehaviour
{
    [Range(1,99)][SerializeField] int startingLevel = 1;
    [SerializeField] CharacterClasses characterClass;
    [SerializeField] Progression progression;

    public float GetHealth(){
        return progression.GetHealth(characterClass, startingLevel);
    }
}



}
