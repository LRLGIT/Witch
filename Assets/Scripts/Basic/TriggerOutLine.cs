using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOutLine : MonoBehaviour
{
    public Material replacementMaterial; // �滻�Ĳ���
    private Material originalMaterial; // ԭʼ�Ĳ���
    private SpriteRenderer spriteRenderer; // SpriteRenderer���

    void Start()
    {
        // ��ȡSpriteRenderer���
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ȷ���滻���ʱ���ȷ��ֵ
        if (replacementMaterial == null)
        {
            Debug.LogError("Replacement Material is not assigned!");
            return;
        }

        // ����ԭʼ�Ĳ���
        originalMaterial = spriteRenderer.material;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ���滻�Ĳ���Ӧ�õ�SpriteRenderer��
        spriteRenderer.material = replacementMaterial;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // ��ԭʼ�Ĳ�������Ӧ�õ�SpriteRenderer��
        spriteRenderer.material = originalMaterial;
    }
}