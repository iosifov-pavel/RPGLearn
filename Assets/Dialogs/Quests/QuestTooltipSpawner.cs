using System.Collections;
using System.Collections.Generic;
using GameDevTV.Core.UI.Tooltips;
using UnityEngine;

public class QuestTooltipSpawner : TooltipSpawner
{
    Quest quest;
    QuestContainer container;
    public override bool CanCreateTooltip()
    {
        return true;
    }

    public override void UpdateTooltip(GameObject tooltip)
    {
        container = GetComponent<QuestItemUI>().GetQuestContainer();
        quest = GetComponent<QuestItemUI>().GetQuest();
        tooltip.GetComponent<QuestTooltipUI>().Setup(container);
    }
}
