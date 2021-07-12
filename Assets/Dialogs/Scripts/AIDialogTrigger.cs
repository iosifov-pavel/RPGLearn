using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIDialogTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] UnityEvent Attack;
    [SerializeField] UnityEvent GiveQuest;
    [SerializeField] UnityEvent CompleteQuest;

    public void TriggerActions(List<DialogActions> actions){
        foreach(DialogActions action in actions){
            if(action==DialogActions.Attack){
                Attack.Invoke();
            }
            if(action==DialogActions.QuestStarted){
                GiveQuest.Invoke();
            }
            if(action==DialogActions.QuestComplete){
                CompleteQuest.Invoke();
            }
        }
    }
}

