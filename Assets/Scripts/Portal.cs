using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement{
public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    enum DestinationID{
        A, B, C, D, E
    }
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] DestinationID id;
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Player"){
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition(){
        print("Player IN");
        //SceneManager.LoadScene(sceneToLoad);
        DontDestroyOnLoad(this.gameObject);
        //AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        //yield return operation.isDone;
        Fader fader = FindObjectOfType<Fader>();
        yield return fader.FadeOut(1);
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        print("Scene Loaded");
        Portal otherPortal = GetOtherPortal();
        Updateplayer(otherPortal);
        yield return fader.FadeIn(1);  
        Destroy(this.gameObject);
    }

        private void Updateplayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = otherPortal.spawnPoint.position;
        }

        private Portal GetOtherPortal()
        {
            Portal[] portals = FindObjectsOfType<Portal>();
            foreach(Portal portal in portals){
                if(portal==this) continue;
                if(this.PortalID() == portal.PortalID()) return portal;
            }
            return null;
        }

        DestinationID PortalID(){
            return id;
        }
    }
}


