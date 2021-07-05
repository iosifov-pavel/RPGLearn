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
    Vector2 scrollposition;
    [NonSerialized]DialogueNode newNode = null;
    [NonSerialized]DialogueNode deleteNode = null;
    [NonSerialized]DialogueNode linkingNode = null;

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
        else
        {
            ProcessEvents();
            scrollposition = EditorGUILayout.BeginScrollView(scrollposition);
            GUILayoutUtility.GetRect(4000,4000);
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                ConnectionsDraw(node);
            }
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                NodeDraw(node);
            }
            EditorGUILayout.EndScrollView();
            ButtonChecks();
        }

    }

    private void ButtonChecks()
    {
        if (newNode != null)
        {
            Undo.RecordObject(selectedDialogue, "Added new Dialog Node");
            selectedDialogue.CreateChildNode(newNode);
            newNode = null;
        }
        if (deleteNode != null)
        {
            Undo.RecordObject(selectedDialogue, "Deleting Dialog Node");
            selectedDialogue.DeleteNode(deleteNode);
            deleteNode = null;
        }
    }

    private void ConnectionsDraw(DialogueNode node)
    {
        if(node.childrens.Count==0) return;
        Vector2 start = node.rect.center + new Vector2(node.rect.width/2,0);
        foreach(DialogueNode children in selectedDialogue.GetAllChildren(node))
        {
            Vector2 end = children.rect.center - new Vector2(children.rect.width/2,0);
            Vector2 offset = end-start;
            offset.y=0;
            offset.x *= 0.6f;
            Vector2 startTo = start + offset;
            Vector2 endTo = end - offset;
            Handles.DrawBezier(start,end,startTo,endTo,Color.blue, null , 3f);
        }
    }

    private void ProcessEvents()
    {
        if(Event.current.type == EventType.MouseDown && draggingNode == null){
            draggingNode = GetNodeAtPoint(Event.current.mousePosition+scrollposition);
            if(draggingNode!=null){
                draggingOffset = draggingNode.rect.position - Event.current.mousePosition;
                Undo.RecordObject(selectedDialogue, "Update node position");
            }
        }
        else if(Event.current.type == EventType.MouseDrag && draggingNode != null){
            draggingNode.rect.position = Event.current.mousePosition + draggingOffset;
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
            if(node.rect.Contains(point)){
                temporaryNode = node;
            }
        }
        return temporaryNode;
    }

    private void NodeDraw(DialogueNode node)
    {
        GUILayout.BeginArea(node.rect, nodeStyle);
        EditorGUI.BeginChangeCheck();
        string newText = EditorGUILayout.TextField(node.text);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(selectedDialogue, "Update dialog text");
            node.text = newText;
        }
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("+"))
        {
            newNode = node;
        }
        DrawLinkButtons(node);
        if (GUILayout.Button("x"))
        {
            deleteNode = node;
        }
        GUILayout.EndHorizontal();
        //foreach(DialogueNode childNode in selectedDialogue.GetAllChildren(node)){
        //    EditorGUILayout.LabelField(childNode.text);
        //}
        GUILayout.EndArea();
    }

    private void DrawLinkButtons(DialogueNode node)
    {
        if (linkingNode == null)
        {
            if (GUILayout.Button("Link"))
            {
                linkingNode = node;
            }
        }
        else
        {
            if (node.ID == linkingNode.ID)
            {
                if (GUILayout.Button("cancel"))
                {
                    linkingNode = null;
                }
            }
            else
            {
                if (linkingNode.childrens.Contains(node.ID))
                {
                    if (GUILayout.Button("unlink"))
                    {
                        Undo.RecordObject(selectedDialogue, "Linking");
                        linkingNode.childrens.Remove(node.ID);
                        linkingNode = null;
                    }
                }
                else
                {
                    if (GUILayout.Button("child"))
                    {
                        Undo.RecordObject(selectedDialogue, "Linking");
                        linkingNode.childrens.Add(node.ID);
                        linkingNode = null;
                    }
                }
            }

        }
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
