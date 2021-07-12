using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestTooltipUI : MonoBehaviour
{
    Quest quest;
    QuestContainer container;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI reward;
    //[SerializeField] TextMeshProUGUI progress;
    [SerializeField] GameObject objectivePrefab;
    [SerializeField] Transform objectiveContainer;
    // Start is called before the first frame update
    public void Setup(QuestContainer questContainer){
        ChildsHelper.DeleteChilds(objectiveContainer.gameObject);
        container = questContainer;
        quest = questContainer.GetQuest();
        title.text = quest.GetTitle();
        foreach(Quest.Objective task in quest.GetObjectives()){
            GameObject objective = Instantiate(objectivePrefab,objectiveContainer);
            ObjectiveUI objUI = objective.GetComponent<ObjectiveUI>();
            objUI.SetText(task.description);
            if(container.IsObjectiveComplete(task.reference)){
                objUI.SetObjectiveComplete();
            }
        }
        if(quest.GetRewards().Count==0){
            reward.text="No reward";
            return;
        }
        reward.text="Get: ";
        foreach(Quest.Reward rwrd in quest.GetRewards()){
            string rewardText="";
            if(rwrd.number>1){
                rewardText+=rwrd.number.ToString();
            }
            rewardText+=" "+rwrd.item.name+",";
            reward.text+=rewardText;
        }
    }
}
