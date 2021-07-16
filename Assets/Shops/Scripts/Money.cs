using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] float startingBalance = 400f;
    float balance = 0;
    public event Action OnChange;

    private void Awake() {
        balance = startingBalance;
        print("Balance: "+balance);
    }

    public float GetBalance(){
        return balance;
    }

    public void UpdateBalance(float amount){
        balance+=amount;
        OnChange();
    }
}