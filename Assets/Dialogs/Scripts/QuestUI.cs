using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Quest quest;

    private void Start() {
        foreach (string task in quest.GetQuest())
        {
            Debug.Log(task);
        }
    }
}
