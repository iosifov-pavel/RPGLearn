using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TraitsUI : MonoBehaviour
{
    [SerializeField] Button confirm;
    [SerializeField] TextMeshProUGUI points;
    TraitsStore player=null;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<TraitsStore>();
        confirm.onClick.AddListener(player.Confirm);
    }

    // Update is called once per frame
    void Update()
    {
        if(player){
            points.text = player.GetUnPoints().ToString();
        }
    }
}
