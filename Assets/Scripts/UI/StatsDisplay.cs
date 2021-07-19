using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Stats;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    Health health;
    Health target;
    Mana playerMana;
    [SerializeField] Text healthText;
    [SerializeField] Text enemyText;
    [SerializeField] Text expText;
    [SerializeField] Text level;
    [SerializeField] Text manaText;

    private void Awake() {
        health = GameObject.FindWithTag("Player").GetComponent<Health>();
        playerMana = GameObject.FindWithTag("Player").GetComponent<Mana>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        level.text = health.GetComponent<BaseStats>().GetLevel().ToString();
        expText.text = health.GetComponent<Expierence>().GetExpValue().ToString();
        healthText.text = string.Format("{0}/{1}",health.GetHP(),health.GetMAXHp());
        target = health.GetComponent<Fighter>().GetTarget();
        if(target==null){
            enemyText.text = string.Format("N/A");
        }
        else enemyText.text = string.Format("{0}/{1}",target.GetHP(),target.GetMAXHp());
        manaText.text = $"{playerMana.GetMana():N0}/{playerMana.GetMaxMana()}";
    }
}
