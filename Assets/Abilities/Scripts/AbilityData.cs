using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AbilityData 
{
    GameObject user;
    IEnumerable<GameObject> targets;
    Vector3 point;

    public AbilityData(GameObject user)
    {
        this.user = user;
    }

    public void SetTargets(IEnumerable<GameObject> targets){
        this.targets = targets;
    }

    public IEnumerable<GameObject> GetTargets(){
        return targets;
    }

    public GameObject GetUser(){
        return user;
    }

    public void SetPoint(Vector3 point){
        this.point = point;
    }

    public Vector3 GetPoint(){
        return point;
    }

    public void StartCoroutine(IEnumerator coroutine){
        user.GetComponent<MonoBehaviour>().StartCoroutine(coroutine);
    }
}
