using System;
using System.Collections.Generic;
using RPG.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] string id = "";
        static Dictionary<string, SaveableEntity> gloabalDic = new Dictionary<string, SaveableEntity>();
        public string GetUniqueIdentifier(){
            return id;
        }

        public object CaptureState(){
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach(ISaveable saveable in GetComponents<ISaveable>()){
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }

        public void RestoreState(object stateD){
            Dictionary<string, object> state = (Dictionary<string, object>) stateD;
            foreach(ISaveable saveable in GetComponents<ISaveable>()){
                string typeString = saveable.GetType().ToString();
                if(state.ContainsKey(typeString)){
                    saveable.RestoreState(state[typeString]);
                }
            }
        }
#if UNITY_EDITOR
        private void Update() {
            if(Application.IsPlaying(gameObject)) return;
            if(string.IsNullOrEmpty(gameObject.scene.path)) return;
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("id");
             

            print("Editing");
            if(string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue)){
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
            gloabalDic[property.stringValue] = this;
        }

        private bool IsUnique(string needToCheck)
        {
            if(!gloabalDic.ContainsKey(needToCheck)) return true;
            if(gloabalDic[needToCheck] == this) return true;
            if(gloabalDic[needToCheck]==null){
                gloabalDic.Remove(needToCheck);
                return true;
            }
            if(gloabalDic[needToCheck].GetUniqueIdentifier() != needToCheck){
                gloabalDic.Remove(needToCheck);
                return true;
            }
            return false;
        }
#endif
    }
}