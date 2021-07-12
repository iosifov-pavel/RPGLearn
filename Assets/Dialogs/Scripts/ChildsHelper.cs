using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildsHelper : MonoBehaviour
{
    // Start is called before the first frame update
    public static void DeleteChilds(GameObject parent){
        foreach(Transform child in parent.transform){
            if(child==parent) continue;
            Destroy(child.gameObject);
        }
    }
}
