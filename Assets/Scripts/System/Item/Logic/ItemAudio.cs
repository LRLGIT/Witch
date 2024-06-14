using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAudio : MonoBehaviour
{
    private AudioSource audioSource; // �� AudioSource ����Ϊ˽�б���

    public float maxVolumeDistance = 10f; // �����������
    public float minVolumeDistance = 1f; // ��С��������

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>(); // ��ȡ AudioSource ���
    }

    void Update()
    {
        // �����������Ʒ֮��ľ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ������ӳ�䵽������Χ��
        float volume = Mathf.Clamp01(1 - (distanceToPlayer - minVolumeDistance) / (maxVolumeDistance - minVolumeDistance));

        // ��������
        audioSource.volume = volume;
    }
}
