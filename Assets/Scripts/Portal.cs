using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameDevTV.Saving;
using UnityEngine.AI;
using RPG.Control;

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
        DontDestroyOnLoad(this.gameObject);
        Fader fader = FindObjectOfType<Fader>();
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player.enabled = false;
        yield return fader.FadeOut(1);
        SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
        savingWrapper.Save();
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        PlayerController newPlayer = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        newPlayer.enabled = false;
        print("Scene Loaded");
        savingWrapper.Load();
        Portal otherPortal = GetOtherPortal();
        Updateplayer(otherPortal);
        savingWrapper.Save();
        yield return new WaitForSeconds(0.5f);
        yield return fader.FadeIn(1);  
        newPlayer.enabled = true;

        Destroy(this.gameObject);
    }

        private void Updateplayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.GetComponent<NavMeshAgent>().enabled = true;
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


