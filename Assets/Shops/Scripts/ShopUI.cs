using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShopUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI shopName=null;
    [SerializeField] GameObject rowsContainer=null;
    [SerializeField] RowUI rowPrefab=null;
    Shopper player = null;
    Shop currentShop = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Shopper>();
        if(player==null){
            return;
        }
        player.activeShopChanged += ShopChanged;
        ShopChanged();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShopChanged(){
        currentShop = player.GetActiveShop();
        gameObject.SetActive(currentShop!=null);
        if(currentShop)shopName.text = currentShop.GetName();
        RefreshUI();
    }

    private void RefreshUI()
    {
        if(currentShop==null) return;
        ChildsHelper.DeleteChilds(rowsContainer);
        foreach(ShopItem item in currentShop.GetFilteredItems()){
            RowUI newRow = Instantiate<RowUI>(rowPrefab,rowsContainer.transform);
            newRow.Setup(item);
        }
    }

    public void CloseWindow(){
        player.SetActiveShop(null);
    }
}
