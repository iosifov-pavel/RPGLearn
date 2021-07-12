using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompletion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Quest quest;
    [SerializeField] string task;
    public void CompleteObjective(){
        PlayerQuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerQuestList>();
        questList.CheckQuests(quest,task);
    }
}
