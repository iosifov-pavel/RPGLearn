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
        public string GetUniqueIdentifier(){
            return id;
        }

        public object CaptureState(){
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach(ISaveable saveable in GetComponents<ISaveable>()){

            }
            return state;
            //return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state){
            SerializableVector3 sVector = (SerializableVector3) state;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Scheduler>().CancelCurrentAction();
            transform.position = sVector.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
#if UNITY_EDITOR
        private void Update() {
            if(Application.IsPlaying(gameObject)) return;
            if(string.IsNullOrEmpty(gameObject.scene.path)) return;
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("id");
             

            print("Editing");
            if(string.IsNullOrEmpty(property.stringValue)){
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}