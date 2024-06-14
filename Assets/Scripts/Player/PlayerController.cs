using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float runSpeed;
    private float Rspeed = 1;

    RandomObjectMover music;
    GameObject MusicPanel;
    public GameObject[] Panels;
    public List<GameObject> panelList=new List<GameObject>();
    GameObject MusicCanvas;
    LevelScrollRect Ls;

    public bool canTalk=>Panels.All(panel => !panel.activeSelf)&&panelList.All(panel => !panel.activeSelf);
 
    public KeyCode keyrun;

    Animator animator;
    Vector3 movement;

    private float noKeyPressTime = 0f;
    public float Time = 3f;
    private bool isAnimating = false;
    public bool canMove;
    void Start()
    {
        canMove = true;
        MusicCanvas = GameObject.Find("Music Canvas");
        animator = GetComponent<Animator>();
        music = MusicCanvas.GetComponent<RandomObjectMover>();
        MusicPanel = MusicCanvas.transform.Find("RightPanel").gameObject;
        Ls = MusicCanvas.transform.Find("Musical instrument").gameObject.GetComponent<LevelScrollRect>();
        animator.SetBool("R", true);
    }

    void FixedUpdate()
    {
        
        if (Panels != null)
        {

            bool allPanelsInactive = Panels.All(panel => !panel.activeSelf);
            //Debug.Log(allPanelsInactive);

            // foreach (var panel in Panels)
            // {
            //     if (panel.activeSelf)
            //     {
            //         Debug.Log(panel.transform.childCount);
            //     }
            // }
            //
            if (allPanelsInactive&&canMove)
            {
                //Debug.Log(123);
                if (panelList.Count>0)
                {
                    allPanelsInactive = panelList.All(panel => !panel.activeSelf);
                    if (allPanelsInactive&&canMove)
                    {
                        //Debug.Log(123);
                        Move();
                    }
                    else
                    {
                        //Debug.Log(123);
                        animator.SetBool("run", false);
                        animator.SetBool("walk", false);
                
                    }
                }
                else
                {
                    //Debug.Log(123);
                    Move();
                }
            }
            else
            {
                animator.SetBool("run", false);
                animator.SetBool("walk", false);
            }
        } 


    }

    private void Update()
    {
        if (MusicPanel.activeSelf == true)
        {
            switch (Ls.label)
            {
                case 0: animator.SetBool("play-lute", true); break;
                case 1: animator.SetBool("play-flute", true); break;
            }
            if (music.playing == true)
            {
                animator.SetBool("playing", true);
            }
            else
            {
                animator.SetBool("playing", false);
            }
        }
        else
        {
            animator.SetBool("playing", false);
            animator.SetBool("play-lute", false);
            animator.SetBool("play-flute", false);
        }
        if (!Input.anyKey && Mathf.Approximately(Input.GetAxisRaw("LRT"), 0f) && Mathf.Approximately(Input.GetAxisRaw("Horizontal"), 0f)&& Mathf.Approximately(Input.GetAxisRaw("Vertical"), 0f))
        {
            noKeyPressTime += UnityEngine.Time.deltaTime;

            // 如果未按键时间超过3秒并且动画没有在播放，开始播放动画
            if (noKeyPressTime >= Time && !isAnimating)
            {
                StartAnimation();
            }
        }
        else 
        {
            // 如果有按键按下或者轴的值发生变化，重置未按键时间并停止动画播放
            noKeyPressTime = 0f;
            StopAnimation();
        }

        void StartAnimation()
        {
            isAnimating = true;
            animator.SetBool("static", true);
           
        }

        // 停止动画
        void StopAnimation()
        {
            isAnimating = false;
            animator.SetBool("static", false);
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // 根据输入方向设置对象的位置
        movement = new Vector3(horizontalInput, verticalInput, 0f).normalized * (moveSpeed * Rspeed * UnityEngine.Time.deltaTime);
        transform.Translate(movement);

        if (horizontalInput != 0 || verticalInput != 0)
        {
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }

        // 根据水平输入调整角色的朝向
        if (horizontalInput > 0)
        {
            animator.SetBool("R", true); // 设置为朝右的动画状态
        }
        else if (horizontalInput < 0)
        {
            animator.SetBool("R", false); // 设置为非朝右的动画状态
        }
        if (Input.GetKey(keyrun))
        {
            Rspeed = runSpeed;
            animator.SetBool("run", true);
            animator.SetBool("walk", false);
        }
        else
        {
            animator.SetBool("run", false);
            Rspeed = 1;
        }
    }

    public void RegisterPanel(GameObject panel)
    {
        if(!panelList.Contains(panel))panelList.Add(panel);   
    }

}
