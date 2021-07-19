using System.Collections;
using System.Collections.Generic;
using GameDevTV.Saving;
using UnityEngine;

public class Mana : MonoBehaviour, ISaveable
{
    // Start is called before the first frame update
    [SerializeField] float maxManaPoints=200f;
    [SerializeField] float manaRegenRate = 2f;
    float currentMana;
    private void Awake() {
        currentMana = maxManaPoints;
    }
    public float GetMana(){
        return currentMana;
    }

    public float GetMaxMana(){
        return maxManaPoints;
    }

    public bool UseMana(float mana){
        if(mana>currentMana){
            return false;
        }
        currentMana-=mana;
        return true;
    }

    private void Update() {
        if(currentMana<maxManaPoints){
            currentMana+= manaRegenRate * Time.deltaTime;
            if(currentMana>maxManaPoints){
                currentMana = maxManaPoints;
            }
        }
    }

    public object CaptureState()
    {
        return currentMana;
    }

    public void RestoreState(object state)
    {
        currentMana = (float)state;
    }
}
