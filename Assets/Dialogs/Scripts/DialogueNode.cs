using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    public string ID;
    public string text;
    public string[] childrens;
    public Rect position;
}
