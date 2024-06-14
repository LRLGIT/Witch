using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurchUI : MonoBehaviour
{
    public GameObject Panel;
    bool isTrigger;
    public KeyCode key;
    //public bool Lock=true;

    public GameObject error;
    
    public bool isLeft;
    public bool @lock
    {
        get => !(isLeft?StaticData.leftUnLocked:StaticData.rightUnLocked);
        set
        {
            if (isLeft)
            {
                StaticData.leftUnLocked = !value;
            }
            else
            {
                StaticData.rightUnLocked = !value;
            }
        }
    }

    private void OnEnable()
    {
        StaticData.onUseItem+=OnUseItem;
    }

    private void OnDisable()
    {
        StaticData.onUseItem-=OnUseItem;
    }

    private void OnUseItem(ItemData_So obj)
    {
        if(!@lock)return;
        Debug.Log("useItem");
        if (obj.itemName != "·红钥匙E·"&& isLeft)
        {
            if(obj.itemName!="·把手·")error.SetActive(true);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(key)&&isTrigger)
        {
            Panel.SetActive(!Panel.activeSelf);
        }
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
