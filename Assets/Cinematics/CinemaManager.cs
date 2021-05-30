using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics{
public class CinemaManager : MonoBehaviour
{
    // Start is called before the first frame update
    bool alreadyPlayed = false;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Player" && !alreadyPlayed){
                GetComponent<PlayableDirector>().Play();
                alreadyPlayed = true;
        }
        
    }
}    
}

