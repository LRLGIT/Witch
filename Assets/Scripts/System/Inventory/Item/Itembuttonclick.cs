using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itembuttonclick : MonoBehaviour
{
    public void ButtonPressed()
    {
        // ��ȡ InventoryButtonManager ��ʵ��
        InventoryButtonManager inventoryButtonManager = FindObjectOfType<InventoryButtonManager>();

        // ��� InventoryButtonManager �Ƿ����
        if (inventoryButtonManager != null)
        {
            // ��ȡ��ǰ��ť������
            ItemData_So itemData = inventoryButtonManager.currentItem != null ? inventoryButtonManager.currentItem: null;

            // �����Ʒ�����Ƿ���Ч
            if (itemData != null)
            {
                // ��ȡ���������е� ItemUse ���
                ItemUse[] allItemUses = FindObjectsOfType<ItemUse>();
                StaticData.onUseItem?.Invoke(itemData);
                // �������е� ItemUse ���
                foreach (ItemUse itemUse in allItemUses)
                {
                    // ��鵱ǰ ItemUse ����������Ƿ�����Ʒ����ƥ��
                    if (itemUse.gameObject.name == itemData.itemName)
                    {
                       // Debug.Log(itemUse.gameObject.name);
                        // ���ƥ�䣬����� ItemUse ����еķ�������������Ʒ����
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
