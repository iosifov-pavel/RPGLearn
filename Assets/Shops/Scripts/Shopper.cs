using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopper : MonoBehaviour
{
    Shop activeShop = null;
    public event Action activeShopChanged;
    // Start is called before the first frame update
    public void SetActiveShop(Shop shop)
    {
        activeShop = shop;
        if(activeShopChanged!=null){
            activeShopChanged();
        }
    }

    public Shop GetActiveShop()
    {
        return activeShop;
    }
}
