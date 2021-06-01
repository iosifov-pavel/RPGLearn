using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement{
public class Fader : MonoBehaviour
{
    // Start is called before the first frame update
    CanvasGroup group;
    [SerializeField] float timeToFade = 2;
    private void Awake() {
    }
    void Start()
    {
        group = GetComponent<CanvasGroup>();
    }
    // Update is called once per frame
    void Update()
    {
    }


    public IEnumerator FadeOut(float time){
        while(group.alpha<1){
            group.alpha += Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator FadeIn(float time)
    {
        while (group.alpha > 0)
        {
            group.alpha -= Time.deltaTime / time;
            yield return null;
        }
    }

}
}

