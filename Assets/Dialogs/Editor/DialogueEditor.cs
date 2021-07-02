using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;

public class DialogueEditor : EditorWindow {
    Dialogue selectedDialogue = null;
    GUIStyle nodeStyle = null;
    DialogueNode draggingNode = null;
    Vector2 draggingOffset = Vector2.zero;

    [MenuItem("RPGLearn/Dialogue Editor")]
    public static void ShowWindow() {
        var window = GetWindow<DialogueEditor>();
        window.titleContent = new GUIContent("Dialogue Editor");
        window.Show();
    }

    private void OnGUI() {
        //Repaint();
        if(selectedDialogue==null){
            EditorGUILayout.LabelField("No Dialogue Selected");
            return;
        }
        else{
            ProcessEvents();
            foreach(DialogueNode node in selectedDialogue.GetAllNodes())
            {
                NodeDraw(node);
                Debug.Log(1);
            }
        }

    }

    private void ProcessEvents()
    {
        if(Event.current.type == EventType.MouseDown && draggingNode == null){
            draggingNode = GetNodeAtPoint(Event.current.mousePosition);
            if(draggingNode!=null){
                draggingOffset = draggingNode.position.position - Event.current.mousePosition;
            }
        }
        else if(Event.current.type == EventType.MouseDrag && draggingNode != null){
            Undo.RecordObject(selectedDialogue, "Update node position");
            draggingNode.position.position = Event.current.mousePosition + draggingOffset;
            GUI.changed = true;
        }
        else if(Event.current.type == EventType.MouseUp && draggingNode!=null){
            draggingNode = null;
        }
    }

    private DialogueNode GetNodeAtPoint(Vector2 point)
    {
        DialogueNode temporaryNode = null;
        foreach(DialogueNode node in selectedDialogue.GetAllNodes()){
            if(node.position.Contains(point)){
                temporaryNode = node;
            }
        }
        return temporaryNode;
    }

    private void NodeDraw(DialogueNode node)
    {
        GUILayout.BeginArea(node.position, nodeStyle);
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.LabelField("Node:", EditorStyles.whiteLabel);
        string newText = EditorGUILayout.TextField(node.text);
        string newID = EditorGUILayout.TextField(node.ID);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(selectedDialogue, "Update dialog text");
            node.text = newText;
            node.ID = newID;
        }
        GUILayout.EndArea();
    }

    [OnOpenAsset(1)]
    public static bool ShowEditor(int instanceID, int line){
        Dialogue dial = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
        if(dial){
            ShowWindow();
            return true;
        }
        return false;
    }

    private void OnEnable(){
        Selection.selectionChanged += OnSelectionChanged;    
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
        nodeStyle.normal.textColor = Color.white;
        nodeStyle.padding = new RectOffset(15,15,15,15);
        nodeStyle.border = new RectOffset(8,8,8,8);
    }

    private void OnSelectionChanged(){
        Dialogue dialogue = Selection.activeObject as Dialogue;
        if(dialogue!=null){
            selectedDialogue = dialogue;
        }
        else{
            selectedDialogue = null;
        }
        Repaint();
    }


}
