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

        bool allRightRotation = false; // 将 allRightRotation 初始化为 false

        foreach (WatchRotation watchRotation in watch)
        {
            if (!watchRotation.IsRightRotation())
            {
                // 如果有一个表达式不满足条件，将 allRightRotation 设置为 false，并跳出循环
                allRightRotation = false;
                break;
            }
            else
            {
                // 如果所有元素都为 true，则将 allRightRotation 设置为 true
                allRightRotation = true;
            }
        }
        if (allRightRotation && playMusic && Door != null)
        {
            Destroy(Door);
            source.Play();
            playMusic = false;  // 禁用播放音乐的逻辑
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

