using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WatchManager : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Thing;
    public GameObject Door;
    public WatchRotation[] watch;
    public ViewRotation view;
    AudioSource source;
    // Update is called once per frame
    private bool playMusic = true;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        Thing.SetActive(false);
        
        if(StaticData.appeared.Contains(AppearType.Watch))Destroy(Door);
    }
    void Update()
    {

        bool allRightRotation = false; // �� allRightRotation ��ʼ��Ϊ false

        foreach (WatchRotation watchRotation in watch)
        {
            if (!watchRotation.IsRightRotation())
            {
                // �����һ�����ʽ�������������� allRightRotation ����Ϊ false��������ѭ��
                allRightRotation = false;
                break;
            }
            else
            {
                // �������Ԫ�ض�Ϊ true���� allRightRotation ����Ϊ true
                allRightRotation = true;
            }
        }
        if (allRightRotation && playMusic && Door != null)
        {
            Destroy(Door);
            source.Play();
            playMusic = false;  // ���ò������ֵ��߼�
            Thing.SetActive(true);
            StaticData.appeared.Add(AppearType.Watch);
        }
        if (Panel.activeSelf == false)
        {
            foreach (WatchRotation watchRotation in watch)
            {
                watchRotation.SetInitialRotation();
            }
        }
    }
}

