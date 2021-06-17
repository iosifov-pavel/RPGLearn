using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    Health health;
    Health target;
    [SerializeField] Text healthText;
    [SerializeField] Text enemyText;
    [SerializeField] Text expText;

    private void Awake() {
        health = GameObject.FindWithTag("Player").GetComponent<Health>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        expText.text = health.GetComponent<Expierence>().GetExpValue().ToString();
        healthText.text = string.Format("{0:0.0}%",health.GetPercentage());
        target = health.GetComponent<Fighter>().GetTarget();
        if(target==null){
            enemyText.text = string.Format("N/A");
        }
        else enemyText.text = string.Format("{0:0.0}%",target.GetPercentage());
    }
}
