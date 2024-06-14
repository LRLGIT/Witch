using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChange : MonoBehaviour
{
    public GameObject notemap;
    public Vector3 initialPosition; // �����ʼλ��
    public KeyCode key;
    public GameObject panel;
    public GameObject musicright;
    public GameObject music2;
    MusicStart musicStartScript;
    bool isTrigger;

    private void Start()
    {
        musicStartScript = notemap.GetComponent<MusicStart>();
        // �����ʼλ��
        initialPosition = notemap.transform.position;
    }

    void Update()
    {
        // ���R���Ƿ���
        if (Input.GetKeyDown(key) && isTrigger&&!musicStartScript.hasStart)
        {
            // ����NoteMap�������������activeΪtrue
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
            // ����NoteMap�������������activeΪfalse
            SetChildrenActive(notemap.transform, false);
            // ��notemap�ص���ʼλ��
            notemap.transform.position = initialPosition;
        }
    }

    // ���ø������������������active״̬
    void SetChildrenActive(Transform parent, bool active)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(active);
        }
    }
}
