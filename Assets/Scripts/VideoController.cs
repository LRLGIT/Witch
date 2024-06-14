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
        // ��ȡ��Ƶ���������
        videoPlayer = GetComponent<VideoPlayer>();

        // ������Ƶ������ϵ��¼�
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // ��Ƶ�������ʱ�Ļص�����
    void OnVideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        // �ر���Ƶ���������
        videoPlayer.Stop();
        Bgm.Play();
        // �ر���Ƶ����
        gameObject.SetActive(false); // ���� Destroy(gameObject);
    }

    bool FindItem(string itemName) //Ѱ����������Ƿ����
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
