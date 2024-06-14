using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public bool one;
    public string itemname;
    public AudioSource Bgm;
    void Start()
    {
        // 获取视频播放器组件
        videoPlayer = GetComponent<VideoPlayer>();

        // 订阅视频播放完毕的事件
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // 视频播放完毕时的回调函数
    void OnVideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        // 关闭视频播放器组件
        videoPlayer.Stop();
        Bgm.Play();
        // 关闭视频对象
        gameObject.SetActive(false); // 或者 Destroy(gameObject);
    }

    bool FindItem(string itemName) //寻找虚拟道具是否存在
    {
        foreach (var item in InventoryManager.Instance.virtuBagData.items)
        {
            if (item.itemData != null && item.itemData.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }
}
