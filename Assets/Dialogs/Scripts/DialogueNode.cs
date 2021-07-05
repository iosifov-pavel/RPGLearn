using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    public string ID;
    public string text;
    public List<string> childrens = new List<string>();
    public Rect rect;

    public DialogueNode(){
        ID = System.Guid.NewGuid().ToString();
        rect = new Rect();
        rect.width = 150;
        rect.height = 200;
        rect.position = new Vector2(50,50);
    }

    public DialogueNode(DialogueNode parent){
        ID = System.Guid.NewGuid().ToString();
        rect = new Rect();
        rect.width = 150;
        rect.height = 200;
        Vector2 offset = new Vector2(parent.rect.width/2, parent.rect.height/2);
        rect.position = parent.rect.position + offset;
    }
}
