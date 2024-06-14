using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    
    [Header("鸟")]
    public GameObject thing;
    public bool bird;
    public GameObject interactive;
    public GameObject MusicPanel;
    
    [Header("酒馆")]
    public bool PubRoom;
    public GameObject NoteMap;
    public Vector3 offset;

    [Header("女巫")]
    public bool witch;
    public GameObject Player;
    public Transform targetPosition3;


    public GameObject NPC;
    public float moveSpeed = 2.0f;
    public Transform targetPosition1;
    public Transform targetPosition2;
    private Animator animator;
    [Header("第一段动画")]
    public string AnimationFirst;
    [Header("第二段动画")]
    public string AnimationSecond;
    [Header("第三段动画(角色)")]
    public string AnimationThird;
    bool isTrigger;
    bool isPlayer;
    public bool ones;
    DialogueUI DU;
    // 记录起始位置
    Vector3 startPosition;
    bool isMoving = false;
    // 记录目标位置
    Vector3 targetPosition;
    MusicStart musicStart;
    
    void Start()
    {
        DU = GameObject.Find("Dialogue Canvas").GetComponent<DialogueUI>();
        ones = true;
    }

    void Update()
    {
        if (bird)
        {
            if (MusicPanel.activeSelf)
            {
                if (KeyInput.Instance.iskeydown)
                {
                    gameObject.GetComponent<Animator>().SetBool("bird", true);
                    GetComponent<AudioSource>().Play();
                    if (thing != null)
                    {
                        interactive.SetActive(true);
                        thing.GetComponent<ItemPickup>().canTake = true;
                    }

                    StaticData.appeared.Add(AppearType.Bird);
                }
            }
        }
        else if (PubRoom)
        {
            animator = NPC.GetComponent<Animator>();
            Vector3 newPosition = NoteMap.transform.position + offset;
            gameObject.transform.position = newPosition;
            if (isTrigger)
            {
                musicStart = NoteMap.GetComponent<MusicStart>();
                musicStart.hasEnd = true;
                PlayAnimation(AnimationFirst);
                MoveNPC(targetPosition1.position,NPC);
            }
            else if (DU.end)
            {
                PlayAnimation(AnimationSecond);
                MoveNPC(targetPosition2.position,NPC);
            }
        }
        else if (witch)
        {
            if (ones)
            {
                animator = NPC.GetComponent<Animator>();
                if (isPlayer)
                {
                    NPC.SetActive(true);
                    Player.GetComponent<PlayerController>().canMove = false;
                    Player.GetComponent<Animator>().Play(AnimationThird);
                    MoveNPC(targetPosition3.position, Player);
                    NPC.GetComponent<Animator>().SetBool("Talk", true);
                    MoveNPC(targetPosition1.position, NPC);
                }
                if (NPC.GetComponent<DialogueController1>().end)
                {
                    Player.GetComponent<PlayerController>().canMove = true;
                    Player.GetComponent<Animator>().Play("standR");
                    Destroy(NPC.GetComponent<DialogueController1>());
                    ones = false;
                }
            }
        }
    }

    void PlayAnimation(string animationName)
    {
        // 如果Animator组件存在
        if (animator != null)
        {
            // 播放指定名称的动画
            animator.Play(animationName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Line"))
        {
            isTrigger = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Line"))
        {
            isTrigger = false;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
    void MoveNPC(Vector3 destination,GameObject NPC)
    {
        // 如果NPC不为空且当前未处于移动状态
        if (NPC != null && !isMoving)
        {
            // 设置起始位置和目标位置
            startPosition = NPC.transform.position;
            targetPosition = destination;

            // 开始移动
            StartCoroutine(MoveRoutine(NPC));
        }
    }

    // 移动协程
    IEnumerator MoveRoutine(GameObject NPC)
    {
        // 设置移动状态为真
        isMoving = true;

        // 计算移动距离
        float distance = Vector3.Distance(startPosition, targetPosition);

        // 当距离大于0时继续移动
        while (distance > 0.01f)
        {
            // 使用插值进行平滑移动
            NPC.transform.position = Vector3.MoveTowards(NPC.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 重新计算距离
            distance = Vector3.Distance(NPC.transform.position, targetPosition);

            // 等待下一帧
            yield return null;
        }

        // 移动结束，将移动状态设置为假
        isMoving = false;
    }
}
