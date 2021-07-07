using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpeaker : MonoBehaviour
{
    [SerializeField] Dialogue currentDialogue;
    DialogueNode currentNode = null;
    bool hasPlayerAnswers = false;
    List<DialogueNode> answers = new List<DialogueNode>();
    // Start is called before the first frame update
    private void Awake() {
        currentNode = currentDialogue.GetRootNode();
    }
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetText(){
        if(currentDialogue==null) return "";
        return currentNode.GetText();
    }

    public void Next(){
        if(HasNext()){
            if(currentNode.GetChildrens().Count==0) return;
            int maxR = currentNode.GetChildrens().Count;
            int index = Random.Range(0,maxR);
            DialogueNode[] answerNodes = currentDialogue.GetAllChildren(currentNode).ToArray();
            currentNode = answerNodes[index];
        }
    }

    public void NextForCurrentNode(DialogueNode node){
        if(node.GetChildrens().Count==0){ 
            CloseDialogue();
            return;
        }
        currentNode = currentDialogue.GetAllChildren(node).ToArray()[0];
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
            answers.Add(node);
        }
    }

    public void CloseDialogue(){
        Debug.Log("End of a Dialog");
    }

    public bool HasAnswers(){
        int num = currentNode.GetChildrens().Count;
        if (num ==0) return false;
        if(!currentDialogue.GetAllChildren(currentNode).ToArray()[0].IsPlayer()) return false;
        SetAnswers();
        return true;
    }
}
