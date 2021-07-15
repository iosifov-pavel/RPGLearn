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
    [SerializeField] float sellingMultiplier = 0.75f;
    
    [System.Serializable] class StockItemConfig{
        public InventoryItem item;
        public int initilaStock;
        [Range(0,100)]public float discountPercentage;
    }
    Shopper player = null;
    Inventory playerInventory=null;
    Money playerMoney=null;
    ItemCategory currentFilter = ItemCategory.None;
    Dictionary<InventoryItem, int> transaction = new Dictionary<InventoryItem, int>();
    Dictionary<InventoryItem, int> stock = new Dictionary<InventoryItem, int>();
    public event Action onChange;
    bool isBuyingMode=true;

    private void Awake() {
        foreach(StockItemConfig config in stockConfig){
            stock[config.item] = config.initilaStock;
        }
    }
    public IEnumerable<ShopItem> GetFilteredItems(){
        foreach(ShopItem item in GetAllItems()){
            if(item.GetItem().GetCategory()==GetFiler() || GetFiler()==ItemCategory.None){
                yield return item;
            }
        }
    }

    public IEnumerable<ShopItem> GetAllItems(){
        foreach(StockItemConfig config in stockConfig)
        {
            float price = config.item.GetPrice();
            if (IsByuingMode()) price *= (100 - config.discountPercentage) / 100;
            else price *= sellingMultiplier;
            int quantityInTransaction = 0;
            int avalability = GetAvalability(config.item);
            transaction.TryGetValue(config.item, out quantityInTransaction);
            yield return new ShopItem(config.item, avalability, price, quantityInTransaction);
        }
    }

    private int GetAvalability(InventoryItem item)
    {
        if(IsByuingMode())return stock[item];
        else{
            return CountItemsInInventory(item);
        }
    }

    private int CountItemsInInventory(InventoryItem item)
    {
        int count = 0;
        for(int i=0;i<playerInventory.GetSize();i++){
            if(playerInventory.GetItemInSlot(i)==item){
                count+=playerInventory.GetNumberInSlot(i);
            }
        }
        return count;
    }

    public void ConfirmTransaction(){
        if(playerInventory==null) return;
        foreach(ShopItem shopItem in GetAllItems()){
            InventoryItem item = shopItem.GetItem();
            int quantity = shopItem.GetQuantity();
            float price = shopItem.GetPrice();
            for (int i = 0; i < quantity; i++){
                if(isBuyingMode)
                {
                    BuyItem(item, price);
                }
                else
                {
                    SellItem(item,price);
                }
            }
        }
        onChange();
    }

    private void SellItem(InventoryItem item, float price)
    {
        int slot = FindFirstItemSlot(playerInventory,item);
        if(slot==-1) return;
        playerInventory.RemoveFromSlot(slot,1);
        AddToTransaction(item,-1);
        stock[item]++;
        playerMoney.UpdateBalance(price);
    }

    private int FindFirstItemSlot(Inventory playerInventory, InventoryItem item)
    {
        for(int i=0; i<playerInventory.GetSize();i++){
            if(playerInventory.GetItemInSlot(i)==item){
                return i;
            }
        }
        return -1;
    }

    private void BuyItem(InventoryItem item, float price)
    {
        if (playerMoney.GetBalance() < price) return;
        bool sucsess = playerInventory.AddToFirstEmptySlot(item, 1);
        if (sucsess)
        {
            AddToTransaction(item, -1);
            stock[item]--;
            playerMoney.UpdateBalance(-price);
        }
    }

    public void SwitchMode(bool isBuying){
        isBuyingMode = isBuying;
        onChange();
    }

    public bool IsByuingMode(){
        return isBuyingMode;
    }

    public bool CanTransact(){
        if(transaction.Count==0) return false;
        if(isBuyingMode){
            if(playerMoney.GetBalance()<TransactionTotal()) return false;
            if(!HasInventorySpase()) return false;
        }
        return true;
    }

    private bool HasInventorySpase()
    {
        if(playerInventory==null) return false;
        List<InventoryItem> items = new List<InventoryItem>();
        foreach(ShopItem item in GetAllItems()){
            InventoryItem inventoryItem = item.GetItem();
            int count = item.GetQuantity();
            for (int i = 0; i < count; i++)
            {
                items.Add(inventoryItem);
            }
        }
        return playerInventory.HasSpaceFor(items);
    }

    public void SelectFilter(ItemCategory category){
        currentFilter = category;
        onChange();
    }

    public ItemCategory GetFiler(){
        return currentFilter;
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
        int avalability = GetAvalability(item);
        if(transaction[item]+quantitiy<=avalability){
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
            playerInventory = controler.GetComponent<Inventory>();
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
