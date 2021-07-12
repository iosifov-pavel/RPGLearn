using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "RPGLearn/Quest", order = 0)]
public class Quest : ScriptableObject {
    [SerializeField] List<Objective> tasks = new List<Objective>();
    [SerializeField] List<Reward> rewards = new List<Reward>();

    [System.Serializable]
    public class Reward{
        public int number;
        public InventoryItem item;
    }

    [System.Serializable]
    public class Objective{
        public string reference;
        public string description;
    }

    public string GetTitle(){
        return name;
    }

    public int GetObjectiviesCount(){
        return tasks.Count;
    }

    public IEnumerable GetObjectives(){
        return tasks;
    }

    public List<Reward> GetRewards(){
        return rewards;
    }

    public bool IsTaskExist(string obj){
        foreach(Objective task in tasks){
            if(task.reference==obj){
                return true;
            }
        }
        return false;
    }

    public static Quest GetByName(string qName){
        foreach( Quest quest in Resources.LoadAll<Quest>("")){
            if(quest.name == qName){
                return quest;
            }
        }
        return null;
    }
}
