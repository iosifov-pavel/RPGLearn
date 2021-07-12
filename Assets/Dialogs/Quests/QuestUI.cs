using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject questPrefab;
    PlayerQuestList questList;

    private void Start() {
        questList = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerQuestList>();
        questList.OnChange+=FillList;
        FillList();
        
    }

    void FillList(){
        ChildsHelper.DeleteChilds(gameObject);
        foreach(QuestContainer quest in questList.GetQuests()){
            QuestItemUI newQuestUI = Instantiate(questPrefab, this.transform).GetComponent<QuestItemUI>();
            newQuestUI.Setup(quest);
        }
    }
}
