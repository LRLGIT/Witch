using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAudio : MonoBehaviour
{
    private AudioSource audioSource; // 将 AudioSource 声明为私有变量

    public float maxVolumeDistance = 10f; // 最大音量距离
    public float minVolumeDistance = 1f; // 最小音量距离

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>(); // 获取 AudioSource 组件
    }

    void Update()
    {
        // 计算玩家与物品之间的距离
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 将距离映射到音量范围内
        float volume = Mathf.Clamp01(1 - (distanceToPlayer - minVolumeDistance) / (maxVolumeDistance - minVolumeDistance));

        // 设置音量
        audioSource.volume = volume;
    }
}
