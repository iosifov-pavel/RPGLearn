using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

[CreateAssetMenu(fileName = "DelayedClickTargeting",
menuName = "RPGLearn/Abilities/Targeting/DelayedClick", order = 0)]
public class DelayedClickTargeting : TargetingStrategy
{
    PlayerController controller=null;
    [SerializeField] Texture2D texture;
    [SerializeField] Vector2 hotspot;
    [SerializeField] LayerMask terrain;
    [SerializeField] float radius = 5;
    [SerializeField] GameObject effectPrefab=null;

    public override void StartTargeting(GameObject user, Targets finished)
    {
        controller = user.GetComponent<PlayerController>();
        controller.StartCoroutine(Targeting(user,controller,finished));
    }

    private IEnumerator Targeting(GameObject user, PlayerController controller, Targets finished){
        controller.enabled = false;
        RaycastHit hit;
        GameObject effect = Instantiate(effectPrefab);
        effect.transform.localScale = new Vector3(radius*2,1,radius*2);
        while(true){
            Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,100f,terrain)){
                effect.transform.position = hit.point;
                if(Input.GetMouseButtonDown(0)){
                    //ждём пока кнопка мыши не будет отпущена
                    // чтобы не запускать движение этим кликом
                    yield return new WaitWhile(()=>Input.GetMouseButton(0));
                    controller.enabled = true;
                    finished(LookForTargets(hit));
                    Destroy(effect);
                    yield break;
                }   
            }
            yield return null;
        }
    }

    private IEnumerable<GameObject> LookForTargets(RaycastHit point){
        RaycastHit[] hits = Physics.SphereCastAll(point.point,radius,Vector3.up,0);
        foreach(RaycastHit sHit in hits){
            yield return sHit.transform.gameObject;
        }
    }
}
