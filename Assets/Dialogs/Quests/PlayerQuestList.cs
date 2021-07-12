using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using GameDevTV.Saving;
using UnityEngine;

public class PlayerQuestList : MonoBehaviour, ISaveable, IPredicateEvaluator
{
    List<QuestContainer> quests = new List<QuestContainer>();
    public event Action OnChange;
    

    public IEnumerable GetQuests(){
        return quests;
    }

    public void AddQuest(Quest newQuest){
        if(!HasQuest(newQuest))quests.Add(new QuestContainer(newQuest));
        OnChange();
    }

    bool HasQuest(Quest quest){
        foreach(QuestContainer qc in quests){
            if(qc.GetQuest()==quest){
                return true;
            }
        }
        return false;
    }

    public void CheckQuests(Quest quest, string task){
        foreach(QuestContainer qc in quests){
            if(qc.GetQuest()==quest){
                if(quest.IsTaskExist(task)){
                    qc.CompleteTask(task);
                    if(qc.IsComplete()){
                        GetRewards(quest);
                    }
                    OnChange();
                }
            }
        }
    }

    private void GetRewards(Quest quest)
    {
        foreach(Quest.Reward reward in quest.GetRewards()){
            bool wasAdded = GetComponent<Inventory>().AddToFirstEmptySlot(reward.item, reward.number);
            if(!wasAdded){
                GetComponent<ItemDropper>().DropItem(reward.item, reward.number);
            }
        }
    }

    public object CaptureState()
    {
        List<object> states = new List<object>();
        foreach(QuestContainer qc in quests){
            states.Add(qc.CaptureState());
        }
        return states;
    }

    public void RestoreState(object state)
    {
        List<object> stateList = state as List<object>;
        if(stateList==null) return;
        quests.Clear();
        foreach(object obj in stateList){
            quests.Add(new QuestContainer(obj));
        }
    }

    public bool? Evaluate(string predicate, string[] parameters)
    {
        switch(predicate){
            case "HasQuest":
                return HasQuest(Quest.GetByName(parameters[0]));
            case "!HasQuest":
                return !HasQuest(Quest.GetByName(parameters[0]));
        }
        return null;
    }
}
