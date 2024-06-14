using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour
{
    public KeyCode key; // ָ���İ���
    public GameObject panel;
    [Header("��ȡ����Ϸ����")]
    public GameObject[] gameObjects;
    private bool playerInRange = false; // �Ƿ�����ڷ�Χ��

    public AppearType type;

    private void Start()
    {
        if (StaticData.appeared.Contains(type))
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(key) && playerInRange)
        {
            panel.SetActive(!panel.activeSelf);

            // if (StaticData.playerIsFree)
            // {
                foreach (GameObject gameObject in gameObjects)
                {
                    if (gameObject != null)
                    {
                        gameObject.SetActive(true);
                    }
                }
            //}

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ��ײ����Ϊ���
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ��ײ����Ϊ���
        {
            playerInRange = false;
            panel.SetActive(false);
        }
    }
}

public enum AppearType
{
    None,
    Watch,
    Bird
}