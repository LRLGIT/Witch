using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
     void Awake()
     {

         var fades=GameObject.FindGameObjectsWithTag("Fade");
        
        if(fades.Length<2)DontDestroyOnLoad(this);
    }
    void Update()
    {
        // ��鵱ǰ����������
        string currentSceneName = SceneManager.GetActiveScene().name;

        // �����ǰ�������ض�����������"YourSceneName"��������øö���
        if (currentSceneName == "YourSceneName")
        {
            gameObject.SetActive(false);
        }
        else
        {
            // ��������ض�������ȷ�����������õ�
            gameObject.SetActive(true);
        }
    }
}
