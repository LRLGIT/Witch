using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SceneJump : MonoBehaviour
{
    public AudioSource Bgm;
    private VideoPlayer videoPlayer;
    public GameObject video;
    public string sceneName;
    public InventoryData_So Bag;
    public InventoryData_So VirtualBag;
    public bool start;
    public KeyCode triggerKey; // 指定触发视频结束动画的按键

    void Start()
    {
        if (video != null)
        {
            video.SetActive(false);
            // 获取视频播放器组件的引用
            videoPlayer = video.GetComponent<VideoPlayer>();
            // 添加视频播放完成时的事件监听
            videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    void Update()
    {
        // 检测指定按键是否被按下，并且视频处于播放状态
        if ((Input.GetKeyDown(triggerKey)||Input.GetKeyDown(KeyCode.Space)) && videoPlayer != null && videoPlayer.isPlaying)
        {
            TriggerVideoEndAnimation();
        }
    }

    // 视频播放完成时的回调函数
    private void OnVideoFinished(VideoPlayer vp)
    {
        // 停止视频播放
        videoPlayer.Stop();
        // 跳转到指定场景
        SceneManager.LoadScene(sceneName);

        SavaGamenManager.Instance.SaveCurrentSceneName();

    }

    // 触发事件时开始播放视频
    public void PlayVideoAndSwitchScene()
    {
        if (start)
        {
            PlayerPrefs.DeleteAll();
            Bag.items = new List<InventoryItem>(12); // 创建一个具有初始容量为12的新列表
            VirtualBag.items= new List<InventoryItem>(40);
            // 添加12个空物品到列表中
            for (int i = 0; i < 12; i++)
            {
                Bag.items.Add(new InventoryItem());
            }
            for (int i = 0; i < 40; i++)
            {
                VirtualBag.items.Add(new InventoryItem());
            }
            
            StaticData.ClearData();


        }
        // 检查视频是否存在
        if (videoPlayer != null)
        {
            video.SetActive(true);
            // 播放视频
            videoPlayer.Play();
            Bgm.Pause();
            //StartCoroutine(WaitForVideoToFinish());
        }
        else
        {
            // 如果视频不存在，则直接跳转到指定场景
            SceneManager.LoadScene(sceneName);
            SavaGamenManager.Instance.SaveCurrentSceneName();
        }
    }

    // 触发视频结束动画的方法
    public void TriggerVideoEndAnimation()
    {
        // 触发视频结束动画
        OnVideoFinished(videoPlayer);
    }
    
    IEnumerator WaitForVideoToFinish()
    {
        yield return new WaitForSeconds((float)videoPlayer.clip.length);
        
        OnVideoFinished(videoPlayer);
    }

    public void ClearFuwen()
    {
        Debug.Log("cleared");
        var obj=GameObject.FindGameObjectWithTag("Fuwen");

        
        if (obj != null)
        {
            obj.transform.parent.gameObject.SetActive(false);
                
            Destroy(obj.transform.parent.gameObject);
        }
    }
}
