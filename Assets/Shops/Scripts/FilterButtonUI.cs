using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterButtonUI : MonoBehaviour
{   
    Button button;
    Shop currentShop=null;
    [SerializeField] ItemCategory filter;
    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(FilterItems);
    }

    public void FilterItems(){
        currentShop.SelectFilter(filter);
    }

    public void SetShop(Shop shop){
        currentShop = shop;
    }

    public ItemCategory GetFilter(){
        return filter;
    }

    public Button GetButton(){
        return button;
    }
}
