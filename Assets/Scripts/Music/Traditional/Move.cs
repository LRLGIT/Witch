using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public float speed = 5.0f;      // ͼƬ�ƶ��ٶ�
    public float destroyPositionX = 10.0f;  // ͼƬ��ʧ��λ��

    private RectTransform imageTransform;

    void Start()
    {
        // ��ȡͼƬ��RectTransform���
        imageTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // �����ƶ�ͼƬ
        MoveImage();
    }

    void MoveImage()
    {
        // ����ͼƬ�µ�λ��
        Vector3 newPosition = imageTransform.position + Vector3.left * speed * Time.deltaTime;

        // ��ͼƬ�ƶ�����λ��
        imageTransform.position = newPosition;

        // ���ͼƬ������ָ����X��λ�ã�����ͼƬ����
        if (imageTransform.position.x >= destroyPositionX)
        {
            Destroy(gameObject);
        }
    }
}
