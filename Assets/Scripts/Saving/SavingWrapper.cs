using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Saving;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement{
    public class SavingWrapper : MonoBehaviour
    {
        // Start is called before the first frame update
        SavingSystem savingSystem;
        private const string currentSave = "currentSaveName";

        public void Continue() {
            StartCoroutine(LoadLastScene());
        }

        public void NewGame(string saveFile){
            SetCurrentSave(saveFile);
            StartCoroutine(LoasFirstScene(saveFile));
        }

        public void LoadGame(string saveFile){
            SetCurrentSave(saveFile);
            StartCoroutine(LoadLastScene());
        }

        private void SetCurrentSave(string saveFile)
        {
            PlayerPrefs.SetString(currentSave, saveFile);
            PlayerPrefs.Save();
        }

        string GetCurrentSave(){
            return PlayerPrefs.GetString(currentSave);
        }

        private IEnumerator LoasFirstScene(string saveFile)
        {
            savingSystem = GetComponent<SavingSystem>();
            yield return SceneManager.LoadSceneAsync(1);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return fader.FadeIn(1);
        }

        private IEnumerator LoasMenuScene()
        {
            savingSystem = GetComponent<SavingSystem>();
            yield return SceneManager.LoadSceneAsync(0);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return fader.FadeIn(1);
        }

        private void Start() {
            savingSystem = GetComponent<SavingSystem>();
            Fader fader = FindObjectOfType<Fader>();
        }

        IEnumerator LoadLastScene() {
            savingSystem = GetComponent<SavingSystem>();
            yield return savingSystem.LoadLastScene(GetCurrentSave());
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return fader.FadeIn(1);
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.L)){
                Load();
            }
            if(Input.GetKeyDown(KeyCode.S)){
                Save();
            }
            if(Input.GetKeyDown(KeyCode.D)){
                DeleteSave();
            }
        }

        public void Save()
        {
            savingSystem.Save(GetCurrentSave());
        }

        public void Load()
        {
            savingSystem.Load(GetCurrentSave());
        }

        public void LoadMenu(){
            StartCoroutine(LoasMenuScene());
        }

        public void DeleteSave(){
            GetComponent<SavingSystem>().Delete(GetCurrentSave());
        }

        public IEnumerable<string> ListSaves(){
            return savingSystem.ListOfSaves();
        }
    }
}
