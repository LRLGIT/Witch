using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Church1F : MonoBehaviour
{
    [Header("������Ʒ����")]
    public string[] itemname;

    [Header("��Ҫ���涯���ĳ�������Ʒ")]
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
            thing[1].GetComponent<Animator>().SetBool("Done", true);//������
        }
        if(rightCanOpen)
        {
            thing[2].GetComponent<ChurchUI>().@lock = false;
            thing[3].GetComponent<Animator>().SetBool("Done", true);//������

        }
          // if (FindItem(itemname[0]))
          //   {
          //       thing[0].GetComponent<ChurchUI>().Lock = false;
          //       thing[1].GetComponent<Animator>().SetBool("Door", true);//������
          //   }
          // if(FindItem(itemname[1]))
          //   {
          //       thing[2].GetComponent<ChurchUI>().Lock = false;
          //       thing[3].GetComponent<Animator>().SetBool("Door", true);//������
          //   }
    }


    bool FindItem(string itemName) //Ѱ����������Ƿ����
    {
        foreach (var item in InventoryManager.Instance.virtuBagData.items)
        {
            if (item.itemData != null && item.itemData.itemName == itemName)
            {
                return true;
            }
            InventoryManager.Instance.virtuBagData.AddItem(item.itemData, -item.itemData.itemAmount);//ɾ��������Ʒ
        }
        return false;
    }
}
