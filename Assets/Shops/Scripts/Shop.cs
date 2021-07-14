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
    Shopper player;


    public event Action onChange;
    public IEnumerable<ShopItem> GetFilteredItems(){
        yield return new ShopItem(InventoryItem.GetFromID("a94749a2-247f-4199-b798-3fd4b0d1f718"),1,1f,1);
        yield return new ShopItem(InventoryItem.GetFromID("f4ec6479-7fdb-4504-bedf-3ed107b21a71"),2,2f,2);
        yield return new ShopItem(InventoryItem.GetFromID("fe8c0a2c-a696-4a77-88da-e29557581f14"),3,3f,3);
    }

    public void ConfirmTransaction(){

    }

    public void SwitchMode(bool isBuying){

    }

    public bool IsByuingMode(){
        return true;
    }

    public bool CanTransact(){
        return true;
    }

    public void SelectFilter(ItemCategory category){

    }

    public ItemCategory GetFiler(){
        return ItemCategory.None;
    }

    public float TransactionTotal(){
        return 0;
    }

    public void AddToTransaction(InventoryItem item, int quantitiy){

    }

    public bool HandleRaycast(PlayerController controler)
    {
        if(Input.GetMouseButtonDown(0)){
            player = controler.GetComponent<Shopper>();
            if(player==null) return false;
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
