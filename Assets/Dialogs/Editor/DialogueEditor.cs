using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;

public class DialogueEditor : EditorWindow {
    Dialogue selectedDialogue = null;
    [NonSerialized]GUIStyle nodeStyle = null;
    [NonSerialized]GUIStyle playerNodeStyle = null;
    [NonSerialized]DialogueNode draggingNode = null;
    [NonSerialized]Vector2 draggingOffset;
    [NonSerialized]Vector2 canvasDraggingOffset;
    [NonSerialized]Vector2 scrollposition;
    [NonSerialized]bool canvasDraging = false;
    [NonSerialized]DialogueNode newNode = null;
    [NonSerialized]DialogueNode deleteNode = null;
    [NonSerialized]DialogueNode linkingNode = null;

    const float canvasSize = 4000f;
    const float backgroundSize = 50f;

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
            Rect canvas = GUILayoutUtility.GetRect(canvasSize,canvasSize);
            Texture2D background = Resources.Load("background") as Texture2D;
            Rect textCoords = new Rect(0,0,canvasSize/backgroundSize,canvasSize/backgroundSize);
            GUI.DrawTextureWithTexCoords(canvas, background, textCoords);
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
            selectedDialogue.CreateChildNode(newNode);
            newNode = null;
        }
        if (deleteNode != null)
        {
            selectedDialogue.DeleteNode(deleteNode);
            deleteNode = null;
        }
    }

    private void ConnectionsDraw(DialogueNode node)
    {
        if(node.GetChildrens().Count==0) return;
        Vector2 start = node.GetRect().center + new Vector2(node.GetRect().width/2,0);
        foreach(DialogueNode children in selectedDialogue.GetAllChildren(node))
        {
            Vector2 end = children.GetRect().center - new Vector2(children.GetRect().width/2,0);
            Vector2 offset = end-start;
            offset.y=0;
            offset.x *= 0.6f;
            Vector2 startTo = start + offset;
            Vector2 endTo = end - offset;
            Handles.DrawBezier(start,end,startTo,endTo,Color.blue, null , 3f);
        }
    }

    private void ProcessEvents(){
        if(Event.current.type == EventType.MouseDown && (draggingNode == null || !canvasDraging)){
            draggingNode = GetNodeAtPoint(Event.current.mousePosition+scrollposition);
            if(draggingNode!=null){
                draggingOffset = draggingNode.GetRect().position - Event.current.mousePosition;
                Selection.activeObject = draggingNode;
            }
            else if(!canvasDraging){
                canvasDraging = true;
                canvasDraggingOffset = scrollposition + Event.current.mousePosition;
                Selection.activeObject = selectedDialogue;
                Debug.Log(canvasDraggingOffset);
            }
        }
        else if(Event.current.type == EventType.MouseDrag && (draggingNode != null || canvasDraging)){
            if(canvasDraging){
                scrollposition = -Event.current.mousePosition + canvasDraggingOffset;
            }
            else{
                draggingNode.SetRectPosition(Event.current.mousePosition + draggingOffset);
            }
            GUI.changed = true;
        }
        else if(Event.current.type == EventType.MouseUp && (draggingNode!=null || canvasDraging)){
            draggingNode = null;
            if(canvasDraging) canvasDraging = false;
        }
    }

    private DialogueNode GetNodeAtPoint(Vector2 point)
    {
        DialogueNode temporaryNode = null;
        foreach(DialogueNode node in selectedDialogue.GetAllNodes()){
            if(node.GetRect().Contains(point)){
                temporaryNode = node;
            }
        }
        return temporaryNode;
    }

    private void NodeDraw(DialogueNode node)
    {
        GUIStyle style = nodeStyle;
        if(node.IsPlayer()) style = playerNodeStyle;
        GUILayout.BeginArea(node.GetRect(), style);
        node.SetText(EditorGUILayout.TextField(node.GetText()));
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
        GUILayout.EndArea();
    }

    private void DrawLinkButtons(DialogueNode node)
    {
        if (linkingNode == null){
            if (GUILayout.Button("Link")){
                linkingNode = node;
            }
        }
        else{
            if (node.name == linkingNode.name){
                if (GUILayout.Button("cancel")){
                    linkingNode = null;
                }
            }
            else{
                if (linkingNode.GetChildrens().Contains(node.name)){
                    if (GUILayout.Button("unlink")){
                        linkingNode.DeleteChild(node.name);
                        linkingNode = null;
                    }
                }
                else{
                    if (GUILayout.Button("child")){
                        linkingNode.AddChild(node.name);
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

        playerNodeStyle = new GUIStyle();
        playerNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
        playerNodeStyle.normal.textColor = Color.white;
        playerNodeStyle.padding = new RectOffset(15,15,15,15);
        playerNodeStyle.border = new RectOffset(8,8,8,8);
    }

    private void OnSelectionChanged(){
        Dialogue dialogue = Selection.activeObject as Dialogue;
        if(dialogue!=null){
            selectedDialogue = dialogue;
        }
        else{
            //selectedDialogue = null;
        }
        Repaint();
    }
}
