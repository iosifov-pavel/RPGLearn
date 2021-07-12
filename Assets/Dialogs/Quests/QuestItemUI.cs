using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestItemUI : MonoBehaviour
{
    Quest quest;
    QuestContainer container;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI progress;
    // Start is called before the first frame update
    public void Setup(QuestContainer questContainer){
        container = questContainer;
        quest = questContainer.GetQuest();
        title.text = quest.GetTitle();
        int count = quest.GetObjectiviesCount();
        progress.text = container.GetCompletedCount().ToString()+"/"+count.ToString();
    }

    public Quest GetQuest(){
        return quest;
    }

    public QuestContainer GetQuestContainer(){
        return container;
    }
}
