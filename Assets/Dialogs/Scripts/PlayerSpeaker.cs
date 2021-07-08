using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpeaker : MonoBehaviour
{
    Dialogue currentDialogue=null;
    DialogueNode currentNode = null;
    AISpeaker currentAISpeaker = null;
    public event Action OnDialogueUpdated;
    List<DialogueNode> answers = new List<DialogueNode>();
    // Start is called before the first frame update
    List<DialogActions> actions = new List<DialogActions>();
    bool actionsNeedToTrigger = false;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialog(Dialogue newDialogue, AISpeaker speaker){
        currentDialogue = newDialogue;
        currentNode = currentDialogue.GetRootNode();
        currentAISpeaker = speaker;
        actions.Clear();
        OnDialogueUpdated();
    }

    public bool IsActive(){
        return currentDialogue!=null;
    }

    public string GetText(){
        if(currentDialogue==null) return "";
        return currentNode.GetText();
    }

    public string GetAINAme(){
        return currentAISpeaker.GetSpeakerName();
    }

    public void Next(){
        if(HasNext()){
            if(currentNode.GetChildrens().Count==0) return;
            int maxR = currentNode.GetChildrens().Count;
            int index = UnityEngine.Random.Range(0,maxR);
            DialogueNode[] answerNodes = currentDialogue.GetAllChildren(currentNode).ToArray();
            currentNode = answerNodes[index];
            InteractWithNodeAction(currentNode);
        }
        OnDialogueUpdated();
    }

    public void NextForCurrentNode(DialogueNode node){
        InteractWithNodeAction(node);
        if(node.GetChildrens().Count==0){ 
            CloseDialogue();
            return;
        }
        currentNode = currentDialogue.GetAllChildren(node).ToArray()[0];
        InteractWithNodeAction(currentNode);
        OnDialogueUpdated();
    }


    public void InteractWithNodeAction(DialogueNode node){
        if(actionsNeedToTrigger){
            ExecuteActions();
        }
        if(node && node.GetActions()!=DialogActions.None){
            if(node.IsPlayer())Debug.Log("Add from player: "+node.GetActions());
            else Debug.Log("Add: "+currentNode.GetActions());
            actions.Add(node.GetActions());
            actionsNeedToTrigger = true;
        }
    }

    public void ExecuteActions(){
        actionsNeedToTrigger=false;
        currentAISpeaker.GetComponent<AIDialogTrigger>().TriggerActions(actions);
    }

    public bool HasNext(){
        if(currentNode.GetChildrens().Count==0){
            return false;
        }
        return true;
    }

    public IEnumerable<DialogueNode> GetChoices(){
        return answers;
    }

    public void SetAnswers()
    {
        answers.Clear();
        DialogueNode[] answerNodes = currentDialogue.GetAllChildren(currentNode).ToArray();
        foreach (DialogueNode node in answerNodes)
        {
            InteractWithNodeAction(node);
            answers.Add(node);
        }
    }

    public void CloseDialogue(){
        Debug.Log("End of a Dialog");
        if(actionsNeedToTrigger)ExecuteActions();
        answers.Clear();
        actions.Clear();
        currentDialogue = null;
        currentNode = null;
        currentAISpeaker=null;
        OnDialogueUpdated();
    }

    public bool HasAnswers(){
        int num = currentNode.GetChildrens().Count;
        if (num ==0) return false;
        if(!currentDialogue.GetAllChildren(currentNode).ToArray()[0].IsPlayer()) return false;
        return true;
    }
}
