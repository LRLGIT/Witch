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
    [Header("传送后场景名称")]
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

        // 如果视频对象不为空，获取视频播放器组件
        if (video != null)
        {
            videoPlayer = video.GetComponent<VideoPlayer>();
            // 订阅视频播放完成事件
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
                // 如果视频为空或者视频不处于激活状态，直接跳转场景
                if (video == null)
                {
                    LoadScene();
                }
                // 如果视频对象存在且处于激活状态，播放视频
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
            if(Auto&&IsTrigger)//自动播放视频+传送
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
        // 播放视频
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
        // 保存起始点信息
        PlayerPrefs.SetString("StartPointNumber", pointName);

        // 开始异步加载场景
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetSceneName);
        
        // 禁用自动切换场景
        asyncOperation.allowSceneActivation = false;

        // 等待场景加载完成
        while (!asyncOperation.isDone)
        {
            // 当加载进度达到0.9（实际上已经加载完成），可以激活场景
            if (asyncOperation.progress >= 0.9f)
            {
                // 激活场景
                asyncOperation.allowSceneActivation = true;
                onSceneLoaded?.Invoke();
            }

            yield return null;
        }

        Debug.Log(123);
        // 场景加载完成后执行回调
        //onSceneLoaded?.Invoke();
    }
    // 视频播放完成时调用的方法
    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("已执行");
        //if (IsTrigger)
        if (true)
        {
            Debug.Log("是Trigger");
            // 视频播放完成后跳转场景
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

        // 等待动画开始播放
        yield return null;

        // 获取当前动画状态信息
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 等待动画播放完成
        while (stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1.0f)
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        // 动画播放完成后执行Action
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
