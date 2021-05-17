using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core{


public class Scheduler : MonoBehaviour
{
    // Start is called before the first frame update
    IAction previousAction = null;
    public void StartAction(IAction action){
        if(previousAction==action) return;
        if(previousAction!=null){
            print("Canceling " + previousAction);
            previousAction.Cancel();
        }
        previousAction = action;
    }
}
}
