using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "RPGLearn/Dialogue", order = 0)]
public class Dialogue : ScriptableObject, ISerializationCallbackReceiver {
    [SerializeField] List<DialogueNode> nodes = new List<DialogueNode>();
    Dictionary<string, DialogueNode> nodesDict = new Dictionary<string, DialogueNode>();

#if UNITY_EDITOR
    private void Awake() {
    }


    private void OnValidate() {
        nodesDict.Clear();
        foreach(DialogueNode node in nodes){
            nodesDict[node.name] = node;
        }
    }
#endif

    public DialogueNode GetRootNode(){
        return nodes[0];
    }
    public IEnumerable<DialogueNode> GetAllNodes(){
        return nodes;
    }

    public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
    {
        if(parentNode.GetChildrens().Count==0) return null;
        List<DialogueNode> childrens = new List<DialogueNode>();
        foreach(string nodeID in parentNode.GetChildrens()){
            if(!nodesDict.ContainsKey(nodeID)) continue;
            childrens.Add(nodesDict[nodeID]);
        }
        
        return childrens;
    }

    public void CreateChildNode(DialogueNode parent)
    {
        DialogueNode newNode = CreateInstance<DialogueNode>();
        newNode.SetRect();
        newNode.SetGUID();
        if(parent!=null){
            Undo.RegisterCreatedObjectUndo(newNode,"Created Dialogue Node");
            Undo.RecordObject(this, "Added node to dialog");
        } 
        nodes.Add(newNode);
        if(parent!=null){
            if(parent.IsPlayer()) newNode.SetSpeakerAsPlayer(false);
            else newNode.SetSpeakerAsPlayer(true);
            newNode.ParentPosition(parent);
            parent.AddChild(newNode.name);
        }
        OnValidate();
    }

    public void DeleteNode(DialogueNode node){
        Undo.RecordObject(this, "Deleting Dialog Node");
        nodes.Remove(node);
        OnValidate();
        foreach(DialogueNode nodeC in nodes){
            nodeC.DeleteChild(node.name);
        }
        Undo.DestroyObjectImmediate(node);
    }

    public void OnBeforeSerialize()
    {
        if(nodes.Count==0){
            CreateChildNode(null);
        }
        OnValidate();
        if(AssetDatabase.GetAssetPath(this) != "")
        {
            foreach (DialogueNode item in GetAllNodes())
            {
                if(AssetDatabase.GetAssetPath(item) == ""){
                    AssetDatabase.AddObjectToAsset(item, this);
                }
            }
        }
    }

    public void OnAfterDeserialize()
    {
        
    }
}
