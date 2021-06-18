using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {

        [SerializeField] string safeFileName = "save";
        public void Save(string saveFile)
        {   
            Dictionary<string,object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        public IEnumerator LoadLastScene(string saveFile){
            Dictionary<string,object> state = LoadFile(saveFile);
            if(state.ContainsKey("LastScene")){
                int sceneID = (int)state["LastScene"];
                if(sceneID != SceneManager.GetActiveScene().buildIndex){
                    yield return SceneManager.LoadSceneAsync(sceneID);
                }
            }        
            RestoreState(state);
        }

        private void SaveFile(string saveFile, object state)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);
            using (FileStream stream = File.Open(path,FileMode.Create)){
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        internal void Delete(string defaultSaveFile)
        {
            string path = GetPathFromSaveFile(defaultSaveFile);
            File.Delete(path);
        }

        private  void CaptureState(Dictionary<string,object> state)
        {
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>()){
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
            state["LastScene"] = SceneManager.GetActiveScene().buildIndex;
        }


        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }

        private Dictionary<string,object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            if(!File.Exists(path)) return new Dictionary<string, object>();
            using(FileStream stream = File.Open(path,FileMode.Open)){
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string,object>)formatter.Deserialize(stream);
            }
        }

        private void RestoreState(Dictionary<string,object> state)
        {
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>()){
                string key = saveable.GetUniqueIdentifier();
                if(state.ContainsKey(key)){
                    saveable.RestoreState(state[key]);
                }
            }
        }

        //private byte[] SerializeVector(Vector3 vector){
        //    byte[] vectorBytes = new byte[3 * 4];
        //    BitConverter.GetBytes(vector.x).CopyTo(vectorBytes,0);
        //    BitConverter.GetBytes(vector.y).CopyTo(vectorBytes,4);
        //    BitConverter.GetBytes(vector.z).CopyTo(vectorBytes,8);
        //    return vectorBytes;
        //}
        //
        //private Vector3 DserializeVector(byte[] buffer){
        //    Vector3 result = new Vector3();
        //    result.x = BitConverter.ToSingle(buffer,0);
        //    result.y = BitConverter.ToSingle(buffer,4);
        //    result.z = BitConverter.ToSingle(buffer,8);
        //    return result;
        //}

        private string GetPathFromSaveFile(string saveFile){
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }


    }
}