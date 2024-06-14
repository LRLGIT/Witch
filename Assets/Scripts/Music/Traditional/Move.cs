using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public float speed = 5.0f;      // 图片移动速度
    public float destroyPositionX = 10.0f;  // 图片消失的位置

    private RectTransform imageTransform;

    void Start()
    {
        // 获取图片的RectTransform组件
        imageTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // 向右移动图片
        MoveImage();
    }

    void MoveImage()
    {
        // 计算图片新的位置
        Vector3 newPosition = imageTransform.position + Vector3.left * speed * Time.deltaTime;

        // 将图片移动到新位置
        imageTransform.position = newPosition;

        // 如果图片到达了指定的X轴位置，销毁图片对象
        if (imageTransform.position.x >= destroyPositionX)
        {
            Destroy(gameObject);
        }
    }
}
