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
            // �����ѡ���Զ�ʰȡ�����谴�°�����ֱ��ʰȡ
            if (canTake)
            {
                PickupItem();
            }
        }
        else
        {
            // ���û�й�ѡ�Զ�ʰȡ������Ҫ����ָ��������ʰȡ
            if (canTake && Input.GetKeyDown(key) && isTrigger)
            {
                PickupItem();
            }
        }
    }

    // ʰȡ��Ʒ�ķ���
    public void PickupItem()
    {
        dialogueUI.ObtainUI(itemData);
        StaticData.stringInfo.Add(itemData.itemName);
        Invoke("CloseObtainUI", 0.1f);
        canTake = false;
    }

    // �ر�ʰȡ��ʾUI�ķ���
    void CloseObtainUI()
    {
        dialogueUI.CloseObtainUI();
        Destroy(gameObject);
    }
}
