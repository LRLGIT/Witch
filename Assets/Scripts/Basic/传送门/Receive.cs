// Receive.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receive : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // 检查是否存在传送点名称，如果有，则将玩家移动到对应传送点
        if (PlayerPrefs.HasKey("StartPointNumber"))
        {
            string startPointNumber = PlayerPrefs.GetString("StartPointNumber");
            GameObject g = GameObject.Find(startPointNumber);
            if (g != null&&player!=null)
            {
                player.transform.position = g.transform.position;
            }

            // 清除传送点名称，以免在下一次传送时重复使用
            PlayerPrefs.DeleteKey("StartPointNumber");
        }
    }
}
