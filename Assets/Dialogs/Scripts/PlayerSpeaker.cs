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
            int maxR = currentNode.GetChildrens().Count;
            int index = Random.Range(0,maxR);
            DialogueNode[] answerNodes = currentDialogue.GetAllChildren(currentNode).ToArray();
            hasPlayerAnswers = false;
            foreach(DialogueNode node in answerNodes){
                if(node.IsPlayer()){
                    hasPlayerAnswers = true;
                }
            }
            if(!hasPlayerAnswers){
                currentNode = answerNodes[index];
            }
            else{
                SetAnswers(answerNodes);
            }
        }
    }

    public void NextForCurrentNode(DialogueNode node){
        currentNode = currentDialogue.GetAllChildren(node).ToArray()[0];
        if(currentNode.GetChildrens().Count==0) return;
        int maxR = currentNode.GetChildrens().Count;
        int index = Random.Range(0,maxR);
        DialogueNode[] answerNodes = currentDialogue.GetAllChildren(currentNode).ToArray();
        hasPlayerAnswers = false;
        foreach(DialogueNode nodes in answerNodes){
            if(node.IsPlayer()){
                hasPlayerAnswers = true;
            }
        }
        if(!hasPlayerAnswers){
            currentNode = answerNodes[index];
        }
        else{
            SetAnswers(answerNodes);
        }
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

    private void SetAnswers(DialogueNode[] nodes){
        answers.Clear();
        foreach(DialogueNode node in nodes){
            answers.Add(node);
        }
    }

    public bool IsPlayerSpeak(){
        return hasPlayerAnswers;
    }

}
