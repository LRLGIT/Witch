using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public ItemData_So itemData;

    public bool automatic;
    public KeyCode key;
    public bool canTake;
    DialogueUI dialogueUI;
    bool isTrigger = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }

    private void Start()
    {
        dialogueUI = GameObject.Find("Dialogue Canvas").GetComponent<DialogueUI>();

        if (StaticData.stringInfo.Contains(itemData.itemName))
        {
            CloseObtainUI();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = false;
    }

    private void Update()
    {
        if (automatic)
        {
            // 如果勾选了自动拾取，无需按下按键，直接拾取
            if (canTake)
            {
                PickupItem();
            }
        }
        else
        {
            // 如果没有勾选自动拾取，则需要按下指定按键来拾取
            if (canTake && Input.GetKeyDown(key) && isTrigger)
            {
                PickupItem();
            }
        }
    }

    // 拾取物品的方法
    public void PickupItem()
    {
        dialogueUI.ObtainUI(itemData);
        StaticData.stringInfo.Add(itemData.itemName);
        Invoke("CloseObtainUI", 0.1f);
        canTake = false;
    }

    // 关闭拾取提示UI的方法
    void CloseObtainUI()
    {
        dialogueUI.CloseObtainUI();
        Destroy(gameObject);
    }
}
