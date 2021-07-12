using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] Quest quest;

    public void GiveQuest(){
        PlayerQuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerQuestList>();
        questList.AddQuest(quest);
    }
}
