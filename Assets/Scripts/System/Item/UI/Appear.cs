using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour
{
    public KeyCode key; // 指定的按键
    public GameObject panel;
    [Header("获取的游戏物体")]
    public GameObject[] gameObjects;
    private bool playerInRange = false; // 是否玩家在范围内

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
        if (collision.CompareTag("Player")) // 碰撞对象为玩家
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 碰撞对象为玩家
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