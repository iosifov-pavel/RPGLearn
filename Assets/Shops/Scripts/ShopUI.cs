using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI shopName=null;
    [SerializeField] GameObject rowsContainer=null;
    [SerializeField] RowUI rowPrefab=null;
    [SerializeField] TextMeshProUGUI total=null;
    [SerializeField] Button transactionButton = null;
    
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
        if(currentShop){
            currentShop.onChange-=RefreshUI;
        }
        currentShop = player.GetActiveShop();
        gameObject.SetActive(currentShop!=null);
        if(currentShop){
            shopName.text = currentShop.GetName();
            currentShop.onChange+=RefreshUI;
        }
        RefreshUI();
    }

    private void RefreshUI()
    {
        if(currentShop==null) return;
        ChildsHelper.DeleteChilds(rowsContainer);
        foreach(ShopItem item in currentShop.GetFilteredItems()){
            RowUI newRow = Instantiate<RowUI>(rowPrefab,rowsContainer.transform);
            newRow.Setup(item,currentShop,item.GetQuantity());
        }
        transactionButton.interactable = currentShop.CanTransact();
        if(currentShop.CanTransact() || Mathf.Approximately(currentShop.TransactionTotal(),0f)){
            total.color = Color.white;
        }
        else{
            total.color = Color.red;
        }
        total.text = $"Total: ${currentShop.TransactionTotal():N2}";

    }

    public void CloseWindow(){
        player.SetActiveShop(null);
    }

    public void ConfirmButton(){
        currentShop.ConfirmTransaction();
    }
}
