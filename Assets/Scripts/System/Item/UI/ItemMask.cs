using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMask : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private Color originalColor;

    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = mySpriteRenderer.color;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // 设置透明度为0.5
            if (mySpriteRenderer != null)
            {
                Color newColor = mySpriteRenderer.color;
                newColor.a = 0.5f;
                mySpriteRenderer.color = newColor;
            }

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        // 检查是否离开碰撞
        if (collision.gameObject.tag == "Player")
        {
            // 恢复原始透明度
            if(mySpriteRenderer!=null)mySpriteRenderer.color = originalColor;
        }
    }
}
