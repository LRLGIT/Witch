using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScene : MonoBehaviour
{
    [Header("虚拟物品名")]
    public string ItemName1, ItemName2;
    [Header("场景")]
    public string ScenceName1,ScenceName2;
    bool isTrigger;
    private void Update()
    {
        if (isTrigger)
        {
            if (FindItem(ItemName1))
            {
                gameObject.GetComponent<Transmit>().targetSceneName = ScenceName1;
            }
            else //if (FindItem(ItemName2))
            {
                gameObject.GetComponent<Transmit>().targetSceneName = ScenceName2;
            }
        }
    }

    bool FindItem(string itemName) //寻找虚拟道具是否存在
    {
        foreach (var item in InventoryManager.Instance.virtuBagData.items)
        {
            if (item.itemData != null && item.itemData.itemName == itemName)
            {
                return true;
            }
            InventoryManager.Instance.virtuBagData.AddItem(item.itemData, -item.itemData.itemAmount);//删除虚拟物品
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = false;
    }
}
