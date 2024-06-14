using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraditionalKey : MonoBehaviour
{
    // ָ���İ���
    public string Name;
    public GameObject Icon;
    // ָ��������λ�õ� X ������
    public float destroyPositionX = 0f;
    [Header("��������")]
    public string AnimationName;
    // ��¼�Ƿ�������ײ
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
        // ����Ƿ�������ײ�Ұ�����ָ������
        if (collided && KeyInput.Instance.keyname==Name)
        {
            gameObject.GetComponent<Animator>().Play(AnimationName);
            // ��ײ�Ұ�����ָ��������������ײ������
            //gameObject.SetActive(false);
            Icon.SetActive(false);
        }
        // ����Ƿ�δ������ײ�Ұ�����ָ������
        else if (transform.position.x <= destroyPositionX)
        {
            // ��������
            gameObject.SetActive(false);
        }

    }
}
