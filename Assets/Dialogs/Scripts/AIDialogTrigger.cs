using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIDialogTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] UnityEvent Attack;

    public void TriggerActions(List<DialogActions> actions){
        foreach(DialogActions action in actions){
            if(action==DialogActions.Attack){
                Attack.Invoke();
            }
        }
    }
}
