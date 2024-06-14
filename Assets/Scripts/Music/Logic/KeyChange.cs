using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class KeyChange : MonoBehaviour
{
    public bool lute = false;
    public bool flute = false;
    public float pressTime = 0;
    public bool canPlay = false;
    [Header("密码")]
    public string[] passwords;
    [Header("面板")]
    GameObject MusicCanvas;

    [Header("可弹奏交互按键")]
    public GameObject music;
    RandomObjectMover Ro;//获取MusicCanvas上面的isright用于判断是否正确
    DialogueController DC;

    private string keyIn;//输入的按钮
    Animator animator;

    [Header("功能选择")]
    public bool talk = false;
    public bool Animator = false;
    public string Animatorname = "Bool";
    public bool transmit = false;
    Transmit tsm;
    Gamepad gamepad;

    public bool take = false;
    ItemPickup itp;
    KeyInput keyInput;
    int num = 0; //统计按的数量
    int Rightnum = 0;//统计按对的数量
    bool isTrigger;
    public bool allRight = false;
    LevelScrollRect ls;
    bool isup;
    float PressTime;
    public string keyName;
    public UnityEvent onComplete;
    void Start()
    {
        gamepad = Gamepad.current;
        MusicCanvas = GameObject.Find("Music Canvas");
        Ro = MusicCanvas.GetComponent<RandomObjectMover>();
        DC = GetComponent<DialogueController>();
        animator = GetComponent<Animator>();
        itp = GetComponent<ItemPickup>();
        ls = Ro.LS.GetComponent<LevelScrollRect>();
        tsm = GetComponent<Transmit>();
        keyInput = GameObject.Find("InputManager").GetComponent<KeyInput>();
    }
    private void Update()
    {
        if (isTrigger && canPlay && IsRightPanelActive()) //碰撞且音乐面板开启后
        {
            Ro.roright = !canPlay;
            if (ls.label == 0 && lute)
            {
                Getpresskey();
            }
            else if (ls.label == 1 && flute)
            {
                Getpresskeylong();
            }
            if (DC != null)
            {
                DC.canTalk = false;
            }
        }
        else
        {
            Rightnum = 0;
            num = 0;
        }
        if (Rightnum == passwords.Length)//全部对弹对
        {
            canPlay = false;
            Talk();
            Play();
            Pickup();
            Transmit();
            Invoke("CloseUI", 2.0f);
            allRight = true;
            music.SetActive(false);
            onComplete?.Invoke();

            DOVirtual.DelayedCall(0.4f, () =>
            {

                FXManager.Instance.PlayFX(0);
            });

        }
        if (DC != null)
        {
            if (Input.GetKeyDown(DC.key)&&DC.playerIsFree)
            {
                CloseUI();
            }
        }
        if (music != null && !allRight)  //特殊交互按键
        {
            music.SetActive(IsRightPanelActive());
        }

    }

    public void Getpresskey()//获取普通输入的按键
    {
        if (num < passwords.Length&&KeyInput.Instance.iskeydown)
        {
            keyIn = keyInput.keyname;
            GetRightKey();
            Ro.keymove(keyIn);
        }
    }

    public void GetRightKey()
    {
        if (keyIn == passwords[Rightnum])
        {
            Rightnum++;
            Ro.isright = true;
        }
        else
        {
            Ro.isright = false;
        }
        num++;
    }
    public void Getpresskeylong()//获取输入的按键
    {
        if (num < passwords.Length&& KeyInput.Instance.iskeydown)
        {
            keyIn = keyInput.keyname;
            Ro.keymove(keyIn);
            GetlongKey();
        }
    }

    public void GetlongKey()
    {
        if (keyIn == passwords[Rightnum])
        {
            if (PressTime < pressTime)
            {
                Rightnum++;
            }

        }
        num++;
    }
    public bool IsRightPanelActive()  //重复功能
    {
        Transform rightPanel = MusicCanvas.transform.Find("RightPanel");
        return rightPanel != null && rightPanel.gameObject.activeSelf;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = true;
            if (canPlay)
            {
                Ro.isright = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Ro.isright = true;
            Ro.roright = true;
            isTrigger = false;
            num = 0;
            CloseUI();
            if (canPlay == true)
            {
                Rightnum = 0;
            }
        }
    }
    public void CloseUI()
    {
        MusicCanvas.transform.Find("RightPanel").gameObject.SetActive(false);
    }
    public void Talk()
    {
        if (talk)
        {
            DC.canTalk = true;
        }
    }

    public void Play()
    {
        if (Animator)
        {
            animator.Play(Animatorname, 0, 0);
        }
    }

    public void Pickup()
    {
        if (take)
        {
            itp.canTake = true;
        }
    }

    public void Transmit()
    {
        if (transmit)
        {
            tsm.canTransmit = true;
        }
    }
}


