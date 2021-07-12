using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Saving;
using System;

[System.Serializable]
public class QuestContainer 
{
    Quest quest;
    List<string> completedTasks = new List<string>();

    [System.Serializable]
    public class QuestContainerRecord{
        public string questName;
        public List<string> completedObjectives= new List<string>();
    }

    public QuestContainer(Quest newQuest)
    {
        quest = newQuest;
    }

    public QuestContainer(object obj)
    {
        QuestContainerRecord record = obj as QuestContainerRecord;
        if(record!=null){
            quest = Quest.GetByName(record.questName);
            completedTasks = record.completedObjectives;
        }
    }

    public Quest GetQuest(){
        return quest;
    }

    public int GetCompletedCount(){
        return completedTasks.Count;
    }

    public bool IsObjectiveComplete(string objective){
        return completedTasks.Contains(objective);
    }

    public bool IsComplete()
    {
        foreach(Quest.Objective task in quest.GetObjectives()){
            if(!completedTasks.Contains(task.reference)){
                return false;
            }
        }
        return true;
    }

    public bool HasTask(string task){
        return true;
    }

    public void CompleteTask(string task){
        if(!completedTasks.Contains(task))completedTasks.Add(task);
    }

    public object CaptureState()
    {
        QuestContainerRecord state = new QuestContainerRecord();
        state.questName = quest.name;
        state.completedObjectives = completedTasks;
        return state;
    }


}
