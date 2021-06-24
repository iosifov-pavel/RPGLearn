using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement{
public class Fader : MonoBehaviour
{
    // Start is called before the first frame update
    CanvasGroup group;
    [SerializeField] float timeToFade = 2;
    Coroutine activeCoroutine = null;
    bool outF=false, inF=false;
    private void Awake() {
        group = GetComponent<CanvasGroup>();
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void FadeOutImmediate(){
        group.alpha = 1;
    }
    public IEnumerator FadeOut(float time){
        StopAllCoroutines();
        while(group.alpha<1){
            group.alpha+= Time.deltaTime / time;
            yield return null;
        }
    }



    public IEnumerator FadeIn(float time)
    {
        StopAllCoroutines();
        while(group.alpha>0){
            group.alpha-= Time.deltaTime / time;
            yield return null;
        }
    }
}
}

