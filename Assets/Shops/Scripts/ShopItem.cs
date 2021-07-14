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

    internal Sprite GetIcon()
    {
        return item.GetIcon();
    }

    internal float GetPrice()
    {
        return price;
    }

    internal int GetAvalability()
    {
        return avalability;
    }

    internal string GetName()
    {
        return item.GetDisplayName();
    }
}
