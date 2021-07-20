using System.Collections;
using System.Collections.Generic;
using RPG.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    SavingWrapper wrapper;
    [SerializeField] Button continueButton;
    [SerializeField] TMP_InputField field;
    private void Start() {
        wrapper = FindObjectOfType<SavingWrapper>();
        continueButton.onClick.AddListener(Continue);
    }

    public void Continue(){
        wrapper.Continue();
    }

    public void StartPlaying(){
        wrapper.NewGame(field.text);
    }

    public void Quit(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
