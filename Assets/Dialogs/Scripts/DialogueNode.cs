using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogueNode : ScriptableObject
{
    [SerializeField] bool isPlayerSpeaking = false;
    [SerializeField] private string text;
    [SerializeField] private List<string> childrens = new List<string>();
    [SerializeField] private Rect rect = new Rect();

    public void SetGUID(){
        this.name = System.Guid.NewGuid().ToString();
    }

    public void SetRect(){
        rect.width = 150;
        rect.height = 150;
        rect.position = new Vector2(50,50);
    }

    public void ParentPosition(DialogueNode parent){
        Vector2 offset = new Vector2(parent.rect.width/2, parent.rect.height/2);
        rect.position = parent.rect.position + offset;
    }

    public Rect GetRect(){
        return rect;
    }
    public string GetText(){
        return text;
    }

    public bool IsPlayer(){
        return isPlayerSpeaking;
    }

    public void SetSpeakerAsPlayer(bool speaker){
        isPlayerSpeaking = speaker;
        EditorUtility.SetDirty(this);
    }

    public List<string> GetChildrens(){
        return childrens;
    }

    public void SetRectPosition(Vector2 position){
        Undo.RecordObject(this, "Update node position");
        rect.position = position;
        EditorUtility.SetDirty(this);
    }

    public void SetText(string newText){
        if(newText!=text){
            Undo.RecordObject(this, "Update dialog text");
            text = newText;
            EditorUtility.SetDirty(this);
        }
    }

    public void AddChild(string childID){
        Undo.RecordObject(this, "Linking");
        childrens.Add(childID);
        EditorUtility.SetDirty(this);
    }

    public void DeleteChild(string childID){
        Undo.RecordObject(this, "Linking");
        childrens.Remove(childID);
        EditorUtility.SetDirty(this);
    }

}
