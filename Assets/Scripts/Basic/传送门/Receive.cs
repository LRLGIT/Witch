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

        // ����Ƿ���ڴ��͵����ƣ�����У�������ƶ�����Ӧ���͵�
        if (PlayerPrefs.HasKey("StartPointNumber"))
        {
            string startPointNumber = PlayerPrefs.GetString("StartPointNumber");
            GameObject g = GameObject.Find(startPointNumber);
            if (g != null&&player!=null)
            {
                player.transform.position = g.transform.position;
            }

            // ������͵����ƣ���������һ�δ���ʱ�ظ�ʹ��
            PlayerPrefs.DeleteKey("StartPointNumber");
        }
    }
}
