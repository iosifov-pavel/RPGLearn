using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem{
    //Image icon;
    InventoryItem item;
    int avalability;
    float price;
    int quantityInTransaction;

    public ShopItem(InventoryItem item, int avalability, float price, int quantityInTransaction){
        this.item=item;
        this.avalability=avalability;
        this.price=price;
        this.quantityInTransaction = quantityInTransaction;
    }
    public InventoryItem GetItem(){
        return item;
    }

    public Sprite GetIcon()
    {
        return item.GetIcon();
    }

    public float GetPrice()
    {
        return price;
    }

    public int GetAvalability()
    {
        return avalability;
    }

    public string GetName()
    {
        return item.GetDisplayName();
    }

    public int GetQuantity(){
        return quantityInTransaction;
    }
}
