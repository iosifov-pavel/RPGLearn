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

    public override void StartTargeting(AbilityData data, Targets finished)
    {
        controller = data.GetUser().GetComponent<PlayerController>();
        controller.StartCoroutine(Targeting(data,controller,finished));
    }

    private IEnumerator Targeting(AbilityData data, PlayerController controller, Targets finished){
        controller.enabled = false;
        RaycastHit hit;
        GameObject effect = Instantiate(effectPrefab);
        effect.transform.localScale = new Vector3(radius*2,1,radius*2);
        while(!data.IsCancelled()){
            Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,100f,terrain)){
                effect.transform.position = hit.point;
                if(Input.GetMouseButtonDown(0)){
                    //ждём пока кнопка мыши не будет отпущена
                    // чтобы не запускать движение этим кликом
                    yield return new WaitWhile(()=>Input.GetMouseButton(0));
                    data.SetPoint(hit.point);
                    data.SetTargets(LookForTargets(hit));
                    break;
                }   
            }
            yield return null;
        }
        controller.enabled = true;
        Destroy(effect);
        finished();
    }

    private IEnumerable<GameObject> LookForTargets(RaycastHit point){
        RaycastHit[] hits = Physics.SphereCastAll(point.point,radius,Vector3.up,0);
        foreach(RaycastHit sHit in hits){
            yield return sHit.transform.gameObject;
        }
    }
}
