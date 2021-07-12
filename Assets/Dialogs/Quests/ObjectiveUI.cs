using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectiveUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] Image check;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetObjectiveComplete(){
        check.gameObject.SetActive(true);
    }

    public void SetText(string text){
        title.text = text;
    }
}
