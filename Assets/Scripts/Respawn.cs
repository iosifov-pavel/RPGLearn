using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

public class Respawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform startLocation;
    [SerializeField] GameObject diePanel;
    Health player;
    
    private void Awake() {
        player = GetComponent<Health>();
        player.onDie.AddListener(RespawnPlayer);
        diePanel.SetActive(false);
    }

    private void Start() {
        if(player){
            if(player.IsDead()){
                RespawnPlayer();
            }
        }
    }
    
    public void RespawnPlayer(){
        Time.timeScale = 0;
        diePanel.SetActive(true);
    }

    public void MakeAlive(){
        player.Heal(gameObject, player.GetMAXHp()/2);
        Time.timeScale = 1;
        GetComponent<NavMeshAgent>().Warp(startLocation.position);
        diePanel.SetActive(false);
    }
}
