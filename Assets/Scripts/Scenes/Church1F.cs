using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Church1F : MonoBehaviour
{
    [Header("虚拟物品名称")]
    public string[] itemname;

    [Header("需要保存动画的场景中物品")]
    public GameObject[] thing;

    private bool leftCanOpen, rightCanOpen;

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.A))
    //     {
    //         Debug.Log(StaticData.leftUnLocked+""+StaticData.rightUnLocked);
    //     }
    // }
    private void Start()
    {
        if (StaticData.leftUnLocked)
        {
            leftCanOpen = true;
        }
        if(StaticData.rightUnLocked)
        {
            rightCanOpen = true;

        }
    }

    private void Update()
    {
        if (leftCanOpen)
        {
            thing[0].GetComponent<ChurchUI>().@lock = false;
            thing[1].GetComponent<Animator>().SetBool("Done", true);//开柜门
        }
        if(rightCanOpen)
        {
            thing[2].GetComponent<ChurchUI>().@lock = false;
            thing[3].GetComponent<Animator>().SetBool("Done", true);//开柜门

        }
          // if (FindItem(itemname[0]))
          //   {
          //       thing[0].GetComponent<ChurchUI>().Lock = false;
          //       thing[1].GetComponent<Animator>().SetBool("Door", true);//开柜门
          //   }
          // if(FindItem(itemname[1]))
          //   {
          //       thing[2].GetComponent<ChurchUI>().Lock = false;
          //       thing[3].GetComponent<Animator>().SetBool("Door", true);//开柜门
          //   }
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
