using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "RPGLearn/Quest", order = 0)]
public class Quest : ScriptableObject {
    [SerializeField] List<string> tasks = new List<string>();

    public void AddTask(string newTask){
        tasks.Add(newTask);
    }

    public IEnumerable<string> GetQuest(){
        foreach(string task in tasks){
            yield return task;
        }
    }
}
