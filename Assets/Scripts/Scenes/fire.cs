using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    [Header("虚拟物品名称")]
    public string itemName;

    [Header("火播放动画名")]
    public string AnimatorName;
    private void Update()
    {
        if(FindItem(itemName))
        {
            gameObject.GetComponent<Animator>().Play(AnimatorName);
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
}
