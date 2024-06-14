using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraditionalKey : MonoBehaviour
{
    // 指定的按键
    public string Name;
    public GameObject Icon;
    // 指定的销毁位置的 X 轴坐标
    public float destroyPositionX = 0f;
    [Header("动画名称")]
    public string AnimationName;
    // 记录是否发生了碰撞
    private bool collided = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Line"))
        {
            collided = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Line"))
        {
            collided = false;
        }
    }
    void Update()
    {
        // 检查是否发生了碰撞且按下了指定按键
        if (collided && KeyInput.Instance.keyname==Name)
        {
            gameObject.GetComponent<Animator>().Play(AnimationName);
            // 碰撞且按下了指定按键，销毁碰撞的物体
            //gameObject.SetActive(false);
            Icon.SetActive(false);
        }
        // 检查是否未发生碰撞且按下了指定按键
        else if (transform.position.x <= destroyPositionX)
        {
            // 销毁物体
            gameObject.SetActive(false);
        }

    }
}
