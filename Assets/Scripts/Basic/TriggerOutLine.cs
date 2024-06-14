using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOutLine : MonoBehaviour
{
    public Material replacementMaterial; // 替换的材质
    private Material originalMaterial; // 原始的材质
    private SpriteRenderer spriteRenderer; // SpriteRenderer组件

    void Start()
    {
        // 获取SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 确保替换材质被正确赋值
        if (replacementMaterial == null)
        {
            Debug.LogError("Replacement Material is not assigned!");
            return;
        }

        // 保存原始的材质
        originalMaterial = spriteRenderer.material;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 将替换的材质应用到SpriteRenderer上
        spriteRenderer.material = replacementMaterial;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 将原始的材质重新应用到SpriteRenderer上
        spriteRenderer.material = originalMaterial;
    }
}