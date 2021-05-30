using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement{
public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int sceneToLoad = -1;
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Player"){
            print("Player IN");
            SceneManager.LoadScene(sceneToLoad);
        }
        
    }
}
}


