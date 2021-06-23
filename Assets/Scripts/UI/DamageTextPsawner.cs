using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextPsawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject targetSpawn;

    public void Spawn(float value){
        GameObject damageText = Instantiate(targetSpawn,transform);
        Text text = damageText.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        text.text = value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
