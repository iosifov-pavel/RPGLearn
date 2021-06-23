using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image image;
    [SerializeField] Canvas rootCanvas = null;

    // Update is called once per frame
    float percent = 1f;
    void Update()
    {
        if(percent<=0 || Mathf.Approximately(percent,1f)){
            rootCanvas.enabled = false;
            return;
        }
        rootCanvas.enabled = true;
        Vector3 newScale = image.rectTransform.localScale;
        newScale.x = percent;
        image.rectTransform.localScale = newScale;
    }

    public void SetView(float percent){
        this.percent = percent;
    }
}
