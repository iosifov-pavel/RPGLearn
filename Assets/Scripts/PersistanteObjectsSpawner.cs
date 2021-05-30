using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    
public class PersistanteObjectsSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject presistentObjectPrefab;
    static bool hasSpawned = false;
    void Awake(){
        if(hasSpawned) return;
        else{
            hasSpawned = true;
            SpawnObjects();
        }
    }

        private void SpawnObjects()
        {
            GameObject persistentObject = Instantiate(presistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }

        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
}
