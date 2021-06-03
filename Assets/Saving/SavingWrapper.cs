using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement{
    public class SavingWrapper : MonoBehaviour
    {
        // Start is called before the first frame update
        SavingSystem savingSystem;
        const string defaultSaveFile="save";
        IEnumerator Start() {
            savingSystem = GetComponent<SavingSystem>();
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            //yield return savingSystem.LoadLastScene(defaultSaveFile);
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
        }

        public void Save()
        {
            savingSystem.Save(defaultSaveFile);
        }

        public void Load()
        {
            savingSystem.Load(defaultSaveFile);
        }
    }
}
