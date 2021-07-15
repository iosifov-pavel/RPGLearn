using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI money=null;
    Money player = null; 
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Money>();
        if(player==null) {
            Debug.Log("Error: Player Money == null");
            return;
        }
        player.OnChange+=UpdateUI;
        UpdateUI();
    }

    // Update is called once per frame
    void UpdateUI()
    {
        money.text = $"${player.GetBalance():N2}";
    }
}
