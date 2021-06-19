using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics{
public class CinematicControlRemover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnEnable() {
        GetComponent<PlayableDirector>().played += DisableControl;
        GetComponent<PlayableDirector>().stopped += EnableControl;
    }
    private void OnDisable() {
        GetComponent<PlayableDirector>().played -= DisableControl;
        GetComponent<PlayableDirector>().stopped -= EnableControl;
    }
    // Update is called once per frame
    void Update()
    {
    }

    void DisableControl(PlayableDirector director){
        print("Disable Control");
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<Scheduler>().CancelCurrentAction();
        player.GetComponent<PlayerController>().enabled = false;
    }

    void EnableControl(PlayableDirector director){
        print("Enable Control");
        GameObject player = GameObject.FindWithTag("Player");
        if(player==null) return;
        player.GetComponent<PlayerController>().enabled = true;
    }
}
}

