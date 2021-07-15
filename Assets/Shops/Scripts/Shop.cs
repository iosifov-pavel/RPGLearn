using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using RPG.Control;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour, IRaycastable
{
    [SerializeField] string shopName="";
    [SerializeField] StockItemConfig[] stockConfig;
    
    [System.Serializable] class StockItemConfig{
        public InventoryItem item;
        public int initilaStock;
        [Range(0,100)]public float discountPercentage;
    }
    Shopper player = null;
    Money playerMoney=null;
    Dictionary<InventoryItem, int> transaction = new Dictionary<InventoryItem, int>();
    Dictionary<InventoryItem, int> stock = new Dictionary<InventoryItem, int>();
    public event Action onChange;

    private void Awake() {
        foreach(StockItemConfig config in stockConfig){
            stock[config.item] = config.initilaStock;
        }
    }
    public IEnumerable<ShopItem> GetFilteredItems(){
        return GetAllItems();
    }

    public IEnumerable<ShopItem> GetAllItems(){
        foreach(StockItemConfig config in stockConfig){
            float price = config.item.GetPrice();
            price*=(100-config.discountPercentage)/100;
            int quantityInTransaction = 0;
            transaction.TryGetValue(config.item, out quantityInTransaction);
            yield return new ShopItem(config.item,stock[config.item], price, quantityInTransaction);
        }
    }

    public void ConfirmTransaction(){
        Inventory playerInventory = player.GetComponent<Inventory>();
        if(playerInventory==null) return;
        foreach(ShopItem shopItem in GetAllItems()){
            InventoryItem item = shopItem.GetItem();
            int quantity = shopItem.GetQuantity();
            float price = shopItem.GetPrice();
            for (int i = 0; i < quantity; i++){
                if(playerMoney.GetBalance()<price) break;
                bool sucsess = playerInventory.AddToFirstEmptySlot(item,1);
                if(sucsess){
                    AddToTransaction(item,-1);
                    stock[item]--;
                    playerMoney.UpdateBalance(-price);
                }
            }
        }
        onChange();
    }

    public void SwitchMode(bool isBuying){

    }

    public bool IsByuingMode(){
        return true;
    }

    public bool CanTransact(){
        if(transaction.Count==0) return false;
        if(playerMoney.GetBalance()<TransactionTotal()) return false;
        return true;
    }

    public void SelectFilter(ItemCategory category){

    }

    public ItemCategory GetFiler(){
        return ItemCategory.None;
    }

    public float TransactionTotal(){
        float total = 0;
        foreach(ShopItem item in GetAllItems()){
            if(transaction.ContainsKey(item.GetItem())){
                total+=item.GetQuantity()*item.GetPrice();
            }
        }
        return total;
    }

    public void AddToTransaction(InventoryItem item, int quantitiy){
        if(!transaction.ContainsKey(item)){
            transaction[item]=0;
        }
        if(transaction[item]+quantitiy<=stock[item]){
            transaction[item]+=quantitiy;
        }
        if(transaction[item]<=0){
            transaction.Remove(item);
        }
        Debug.Log(transaction);
        onChange();
    }

    public bool HandleRaycast(PlayerController controler)
    {
        if(Input.GetMouseButtonDown(0)){
            player = controler.GetComponent<Shopper>();
            playerMoney = controler.GetComponent<Money>();
            if(player==null || playerMoney==null) return false;
            player.SetActiveShop(this);
        }
        return true;
    }

    public Cursors GetCursorType()
    {
        return Cursors.Shop;
    }

    public string GetName(){
        return shopName;
    }
}
