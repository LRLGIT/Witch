using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itembuttonclick : MonoBehaviour
{
    public void ButtonPressed()
    {
        // 获取 InventoryButtonManager 的实例
        InventoryButtonManager inventoryButtonManager = FindObjectOfType<InventoryButtonManager>();

        // 检查 InventoryButtonManager 是否存在
        if (inventoryButtonManager != null)
        {
            // 获取当前按钮的数据
            ItemData_So itemData = inventoryButtonManager.currentItem != null ? inventoryButtonManager.currentItem: null;

            // 检查物品数据是否有效
            if (itemData != null)
            {
                // 获取场景中所有的 ItemUse 组件
                ItemUse[] allItemUses = FindObjectsOfType<ItemUse>();
                StaticData.onUseItem?.Invoke(itemData);
                // 遍历所有的 ItemUse 组件
                foreach (ItemUse itemUse in allItemUses)
                {
                    // 检查当前 ItemUse 组件的名称是否与物品名称匹配
                    if (itemUse.gameObject.name == itemData.itemName)
                    {
                       // Debug.Log(itemUse.gameObject.name);
                        // 如果匹配，则调用 ItemUse 组件中的方法，并传递物品数据
                        itemUse.UseItem(itemData);
                    }
                    else
                    {
                        Debug.Log("Can not find" + itemUse.gameObject.name);
                    }
                    
                }
            }
            else
            {
                Debug.LogError("Current item data is null in InventoryButtonManager!");
            }
        }
        else
        {
            Debug.LogError("InventoryButtonManager not found in the scene!");
        }
    }
}
