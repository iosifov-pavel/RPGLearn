using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "RPGLearn/Dialogue", order = 0)]
public class Dialogue : ScriptableObject {
    [SerializeField] List<DialogueNode> nodes = new List<DialogueNode>();
    Dictionary<string, DialogueNode> nodesDict = new Dictionary<string, DialogueNode>();

#if UNITY_EDITOR
    private void Awake() {
        if(nodes.Count==0){
            DialogueNode rootNode = new DialogueNode();
            nodes.Add(rootNode);
        }
        OnValidate();
    }

    public DialogueNode GetRootNode(){
        return nodes[0];
    }

    private void OnValidate() {
        nodesDict.Clear();
        foreach(DialogueNode node in nodes){
            nodesDict[node.ID] = node;
        }
    }
#endif
    public IEnumerable<DialogueNode> GetAllNodes(){
        return nodes;
    }

    public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
    {
        if(parentNode.childrens.Count==0) return null;
        List<DialogueNode> childrens = new List<DialogueNode>();
        foreach(string nodeID in parentNode.childrens){
            if(!nodesDict.ContainsKey(nodeID)) continue;
            childrens.Add(nodesDict[nodeID]);
        }
        
        return childrens;
    }

    public void CreateChildNode(DialogueNode parent)
    {
        DialogueNode newNode = new DialogueNode(parent);
        nodes.Add(newNode);
        parent.childrens.Add(newNode.ID);
        OnValidate();
    }

    public void DeleteNode(DialogueNode node){
        nodes.Remove(node);
        OnValidate();
        foreach(DialogueNode nodeC in nodes){
            nodeC.childrens.Remove(node.ID);
        }
    }

    public void LinkNodes(){

    }
}
