using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Saving;
namespace RPG.SceneManagement{
    public class SavingWrapper : MonoBehaviour
    {
        // Start is called before the first frame update
        SavingSystem savingSystem;
        const string defaultSaveFile="save";

        private void Awake() {
            StartCoroutine(LoadLastScene());
        }
        IEnumerator LoadLastScene() {
            savingSystem = GetComponent<SavingSystem>();
            yield return savingSystem.LoadLastScene(defaultSaveFile);
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
            savingSystem.Save(defaultSaveFile);
        }

        public void Load()
        {
            //StartCoroutine(savingSystem.LoadLastScene(defaultSaveFile));
            savingSystem.Load(defaultSaveFile);
        }

        public void DeleteSave(){
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
    }
}
