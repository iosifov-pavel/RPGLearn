using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using RPG.SceneManagement;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    PlayerController player;
    SavingWrapper wrapper;
    private void Awake() {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        wrapper = FindObjectOfType<SavingWrapper>();
    }
    private void OnEnable() {
        Time.timeScale = 0;
        player.enabled =false;
    }

    private void OnDisable() {
        Time.timeScale = 1;
        player.enabled = true;
    }

    public void Save(){
        wrapper = FindObjectOfType<SavingWrapper>();
        wrapper.Save();
    }

    public void SaveQuit(){
        Save();
        wrapper.LoadMenu();
    }
}
