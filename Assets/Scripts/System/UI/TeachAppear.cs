using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachAppear : MonoBehaviour
{
    public KeyCode key; // ָ���İ���
    public GameObject Panel;
    public GameObject[] image;
    private int currentIndex = 0; // ��ǰ��ʾ��ͼ������
    private bool hasCollided = false; // �Ƿ�����ײ
    public bool one=true;

    void Update()
    {

        if (Input.GetKeyDown(key) && hasCollided )
        {
            if (currentIndex < image.Length - 1)
            {
                image[currentIndex].SetActive(false);
                currentIndex++;
                image[currentIndex].SetActive(true);
            }
            else
            {
                image[currentIndex].SetActive(false);
                Panel.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasCollided = true;
        currentIndex = 0;
        if (one == true)
        {
            Panel.SetActive(true);
            image[currentIndex].SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hasCollided = false;
        Panel.SetActive(false);
        one = false;
    }
}
