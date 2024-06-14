using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transmit : MonoBehaviour
{
    public bool canTransmit = false;
    public KeyCode Ykey, Nkey;
    public GameObject Panel;
    [Header("���ͺ󳡾�����")]
    public string targetSceneName = "other";
    private bool IsTrigger = false;

    string pointName;
    private VideoPlayer videoPlayer;
    public GameObject video;

    public bool unUseVideo;

    public string FadeAnimName;
    public string FadeInAnimName;
    
    public Image fadeImage=>GameObject.FindGameObjectWithTag("Fade").GetComponent<Image>();
    private Animator fadeImageAnimator=>fadeImage.GetComponent<Animator>();

    private bool videoIsPlaying;
    
    
    public bool Auto;
    void Start()
    {
        
        
        if (Panel != null)
        {
            Panel.SetActive(false);
        }
        pointName = gameObject.name;

        // �����Ƶ����Ϊ�գ���ȡ��Ƶ���������
        if (video != null)
        {
            videoPlayer = video.GetComponent<VideoPlayer>();
            // ������Ƶ��������¼�
            //videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    void Update()
    {
        if(videoIsPlaying)return;
        if (!unUseVideo)
        {
            if (IsTrigger && Input.GetKeyDown(Ykey) && canTransmit)
            {
                QuestManager.Instance.SaveQuestManager();
                //FuWenManager.Instance.SaveFuWenData();
                // �����ƵΪ�ջ�����Ƶ�����ڼ���״̬��ֱ����ת����
                if (video == null)
                {
                    LoadScene();
                }
                // �����Ƶ��������Ҵ��ڼ���״̬��������Ƶ
                else if(video!=null)
                {
                    video.SetActive(true);
                    PlayVideo();
                }
            }
            if (Input.GetKeyDown(Nkey))
            {
                if (Panel != null)
                {
                    Panel.SetActive(false);
                }
            }
            if(Auto&&IsTrigger)//�Զ�������Ƶ+����
            {
                video.SetActive(true);
                PlayVideo();
            }
        }
        else
        {
            if (IsTrigger && Input.GetKeyDown(Ykey) && canTransmit)
            {
                fadeImage.enabled=true;
                PlayAnimationAndExecuteAction(fadeImageAnimator,FadeAnimName, () =>
                {
                    LoadSceneAsync(pointName,targetSceneName, () =>
                    {
                        Debug.Log(123);
                        PlayAnimationAndExecuteAction(fadeImageAnimator, FadeInAnimName, () =>
                        {
                            fadeImage.enabled=false;
                        });
                    });
                });
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsTrigger = true;
            if (canTransmit)
            {
                if (Panel != null)
                {
                    Panel.SetActive(true);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsTrigger = false;
            if (Panel != null)
            {
                Panel.SetActive(false);
            }
        }
    }

    void PlayVideo()
    {
        // ������Ƶ
        videoPlayer.Play();
        StartCoroutine(WaitForVideoToFinish());
    }

    void LoadScene()
    {
        SceneManager.LoadScene(targetSceneName);
        PlayerPrefs.SetString("StartPointNumber", pointName);
    }
    public void LoadSceneAsync(string pointName, string targetSceneName, Action onSceneLoaded)
    {
        StartCoroutine(LoadSceneCoroutine(pointName, targetSceneName, onSceneLoaded));
    }

    private IEnumerator LoadSceneCoroutine(string pointName, string targetSceneName, Action onSceneLoaded)
    {
        // ������ʼ����Ϣ
        PlayerPrefs.SetString("StartPointNumber", pointName);

        // ��ʼ�첽���س���
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetSceneName);
        
        // �����Զ��л�����
        asyncOperation.allowSceneActivation = false;

        // �ȴ������������
        while (!asyncOperation.isDone)
        {
            // �����ؽ��ȴﵽ0.9��ʵ�����Ѿ�������ɣ������Լ����
            if (asyncOperation.progress >= 0.9f)
            {
                // �����
                asyncOperation.allowSceneActivation = true;
                onSceneLoaded?.Invoke();
            }

            yield return null;
        }

        Debug.Log(123);
        // ����������ɺ�ִ�лص�
        //onSceneLoaded?.Invoke();
    }
    // ��Ƶ�������ʱ���õķ���
    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("��ִ��");
        //if (IsTrigger)
        if (true)
        {
            Debug.Log("��Trigger");
            // ��Ƶ������ɺ���ת����
            LoadScene();
        }
    }
    public void PlayAnimationAndExecuteAction(Animator animator, string animationName, Action action)
    {
        StartCoroutine(PlayAnimationCoroutine(animator, animationName, action));
    }

    private IEnumerator PlayAnimationCoroutine(Animator animator, string animationName, Action action)
    {
        videoIsPlaying = true;
        
        animator.Play(animationName);

        // �ȴ�������ʼ����
        yield return null;

        // ��ȡ��ǰ����״̬��Ϣ
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // �ȴ������������
        while (stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1.0f)
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        // ����������ɺ�ִ��Action
        action?.Invoke();

        videoIsPlaying = false;
    }

    IEnumerator WaitForVideoToFinish()
    {
        videoIsPlaying = true;
        
        if(videoPlayer!=null&&videoPlayer.clip!=null)yield return new WaitForSeconds((float)videoPlayer.clip.length);
        
        OnVideoFinished(videoPlayer);

        videoIsPlaying = false;
    }
    
    
}
