using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RowUI : MonoBehaviour
{
    Shop currentShop=null;
    ShopItem currentItem=null;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI avalability;
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] TextMeshProUGUI quantity;

    public void Setup(ShopItem item, Shop currentShop, int newQuantity)
    {
        icon.sprite = item.GetIcon();
        title.text = item.GetName();
        avalability.text = item.GetAvalability().ToString();
        price.text = $"${item.GetPrice():N2}";
        this.currentShop = currentShop;
        currentItem = item;
        quantity.text = newQuantity.ToString();
    }

    public void Add(){
        currentShop.AddToTransaction(currentItem.GetItem(),1);
    }

    public void Remove(){
        currentShop.AddToTransaction(currentItem.GetItem(),-1);
    }
}
