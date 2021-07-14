using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RowUI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI avalability;
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] TextMeshProUGUI quantity;

    public void Setup(ShopItem item){
        icon.sprite = item.GetIcon();
        title.text = item.GetName();
        avalability.text = item.GetAvalability().ToString();
        price.text = item.GetPrice().ToString();
    }
}
