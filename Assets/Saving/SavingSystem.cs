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
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);
            using (FileStream stream = File.Open(path,FileMode.Create)){
                Transform player = GetPlayerTransform();
                BinaryFormatter formatter = new BinaryFormatter();
                SerializableVector3 graph = new SerializableVector3(player.position);
                formatter.Serialize(stream, graph);
            }
            
        }

        private Transform GetPlayerTransform()
        {
            return GameObject.FindGameObjectWithTag("Player").transform;
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Loading from "+GetPathFromSaveFile(saveFile));
            using(FileStream stream = File.Open(path,FileMode.Open)){
                Transform player = GetPlayerTransform();
                BinaryFormatter formatter = new BinaryFormatter();
                SerializableVector3 newPos = (SerializableVector3) formatter.Deserialize(stream);
                Vector3 pos = newPos.ToVector();
                print(pos);
                //player.position = DserializeVector(bytes);

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