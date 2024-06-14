using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChange : MonoBehaviour
{
    public GameObject notemap;
    public Vector3 initialPosition; // 保存初始位置
    public KeyCode key;
    public GameObject panel;
    public GameObject musicright;
    public GameObject music2;
    MusicStart musicStartScript;
    bool isTrigger;

    private void Start()
    {
        musicStartScript = notemap.GetComponent<MusicStart>();
        // 保存初始位置
        initialPosition = notemap.transform.position;
    }

    void Update()
    {
        // 检测R键是否按下
        if (Input.GetKeyDown(key) && isTrigger&&!musicStartScript.hasStart)
        {
            // 设置NoteMap下所有子物体的active为true
            SetChildrenActive(notemap.transform, true);
            musicStartScript.hasStart = true;
        }
        if(musicStartScript.hasEnd)
        {
            panel.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = true;
            panel.SetActive(true);
            musicright.SetActive(true);
            music2.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = false;
            panel.SetActive(false);
            music2.SetActive(true);
            musicright.SetActive(false);
            musicStartScript.hasStart = false;
            // 设置NoteMap下所有子物体的active为false
            SetChildrenActive(notemap.transform, false);
            // 将notemap回到初始位置
            notemap.transform.position = initialPosition;
        }
    }

    // 设置父物体下所有子物体的active状态
    void SetChildrenActive(Transform parent, bool active)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(active);
        }
    }
}
