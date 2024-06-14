using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachAppear : MonoBehaviour
{
    public KeyCode key; // 指定的按键
    public GameObject Panel;
    public GameObject[] image;
    private int currentIndex = 0; // 当前显示的图像索引
    private bool hasCollided = false; // 是否发生碰撞
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
