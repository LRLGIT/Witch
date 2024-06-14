using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemUI : MonoBehaviour
{
    public Image icon = null;
    public Text amount = null;
    public ItemData_So currentItemData;

    public InventoryData_So Bag { get; set; }
    public int Index { get; set; } = -1;
    
    public void SetupItemUI(ItemData_So item,int itemAmount)
    {
        if(itemAmount==0)
        {
            Bag.items[Index].itemData = null;
            currentItemData = null;
            icon.gameObject.SetActive(false);
            return;
        }
        if(itemAmount<0)
        {
            item = null;
        }
        if (item == null)
        {
            itemAmount = 0;
            currentItemData = null; 
        }
        if (item != null)
        {
            currentItemData = item;
            icon.sprite = item.itemIcon;
            amount.text = itemAmount.ToString();
            icon.gameObject.SetActive(true);
        }
        else
            icon.gameObject.SetActive(false);
    }

    public ItemData_So GetItem()
    {
        return Bag.items[Index].itemData;
    }
}
