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
    public KeyCode triggerKey; // ָ��������Ƶ���������İ���

    void Start()
    {
        if (video != null)
        {
            video.SetActive(false);
            // ��ȡ��Ƶ���������������
            videoPlayer = video.GetComponent<VideoPlayer>();
            // �����Ƶ�������ʱ���¼�����
            videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    void Update()
    {
        // ���ָ�������Ƿ񱻰��£�������Ƶ���ڲ���״̬
        if ((Input.GetKeyDown(triggerKey)||Input.GetKeyDown(KeyCode.Space)) && videoPlayer != null && videoPlayer.isPlaying)
        {
            TriggerVideoEndAnimation();
        }
    }

    // ��Ƶ�������ʱ�Ļص�����
    private void OnVideoFinished(VideoPlayer vp)
    {
        // ֹͣ��Ƶ����
        videoPlayer.Stop();
        // ��ת��ָ������
        SceneManager.LoadScene(sceneName);

        SavaGamenManager.Instance.SaveCurrentSceneName();

    }

    // �����¼�ʱ��ʼ������Ƶ
    public void PlayVideoAndSwitchScene()
    {
        if (start)
        {
            PlayerPrefs.DeleteAll();
            Bag.items = new List<InventoryItem>(12); // ����һ�����г�ʼ����Ϊ12�����б�
            VirtualBag.items= new List<InventoryItem>(40);
            // ���12������Ʒ���б���
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
        // �����Ƶ�Ƿ����
        if (videoPlayer != null)
        {
            video.SetActive(true);
            // ������Ƶ
            videoPlayer.Play();
            Bgm.Pause();
            //StartCoroutine(WaitForVideoToFinish());
        }
        else
        {
            // �����Ƶ�����ڣ���ֱ����ת��ָ������
            SceneManager.LoadScene(sceneName);
            SavaGamenManager.Instance.SaveCurrentSceneName();
        }
    }

    // ������Ƶ���������ķ���
    public void TriggerVideoEndAnimation()
    {
        // ������Ƶ��������
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
