using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Saving;
using UnityEngine;

public class Expierence : MonoBehaviour, ISaveable
{
    // Start is called before the first frame update
    [SerializeField] float expierencePoints = 0;
    //public delegate void ExpierenceGainedDelegate();
    //public event ExpierenceGainedDelegate onExpGained;
    public event Action onExpGained;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainExpierence(float xp){
        expierencePoints+=xp;
        onExpGained();
    }

    public object CaptureState()
    {
        return expierencePoints;
    }

    public void RestoreState(object state)
    {
        expierencePoints = (float)state;
    }

    public float GetExpValue(){
        return expierencePoints;
    }
}
