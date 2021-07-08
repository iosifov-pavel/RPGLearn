using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

public class AISpeaker : MonoBehaviour, IRaycastable
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] string speakerName;
    public Cursors GetCursorType()
    {
        return Cursors.Dialogue;
    }

    public bool HandleRaycast(PlayerController controler)
    {
        if(!enabled) return false;
        if(dialogue==null) return false;
        if(Input.GetMouseButtonDown(0)){
            PlayerSpeaker player = controler.GetComponent<PlayerSpeaker>();
            if(player){
                player.StartDialog(dialogue, this);
                return true;
            }
        }
        return true;
    }

    public string GetSpeakerName(){
        return speakerName;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
