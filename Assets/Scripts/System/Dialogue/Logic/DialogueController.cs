using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public KeyCode key;
    public DialogueData_So currentData;
    public bool canTalk =true;
    bool isTrigger = false;
    public int num;
    public bool end;
    KeyChange kc;

    public AppearType appearType;

    public bool isTalking=false;
    public bool playerIsFree=>GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().canTalk;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && currentData != null)
        {
            isTrigger = true;
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canTalk = false;
        isTrigger = false;
        ClossDialogue();
    }
    private void Start()
    {
        kc = gameObject.GetComponent<KeyChange>();
        canTalk = true;

        if (StaticData.appeared.Contains(appearType))
        {
            gameObject.SetActive(false);
        }
        
    }

    private void OnEnable()
    {
        StaticData.onDialogStart+=OnDialogStart;
        
        StaticData.onDialogEnd+=OnDialogEnd;
        
    }

    private void OnDialogEnd()
    {
        isTalking = false;
    }

    private void OnDialogStart()
    {
        isTalking = true;
    }
    

    private void OnDisable()
    {
        StaticData.onDialogStart-=OnDialogStart;
        
        StaticData.onDialogEnd-=OnDialogEnd;
    }

    private void Update()
    {
        if(isTrigger)
        {
            num = DialogueUI.Instance.num;
            end = DialogueUI.Instance.end;
        }
        if (kc != null)
        {
            if ((kc.canPlay == false) && isTrigger) // 检查kc是否为空或canPlay为false
            {
                canTalk = true;
            }
            else if (kc.canPlay == true)
            {
                canTalk = false;
            }
        }
        else if(isTrigger)
        {
            canTalk = true;
        }

        if (canTalk && Input.GetKeyDown(key)&& isTrigger&&!isTalking&& playerIsFree)  
        {
            OpenDialogue();
            if (kc != null&&kc.allRight==false) //对话完并且没有全部弹对，打开密码开关(用于设置对话后才能输密码)
            {
                kc.canPlay = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClossDialogue();
        }

        if (StaticData.appeared.Contains(appearType))
        {
            this.enabled = false;
        }
    }

    void OpenDialogue()
    {
        //打开UI面版
        //传输对话内容信息
        //DialogueUI.Instance.DialoguePanel.Play("Play");
        //DialogueUI.Instance.imageL.Play("Play");
        //DialogueUI.Instance.imageR.Play("Play");
        
        DialogueUI.Instance.UpdateDialogueData(currentData);
        DialogueUI.Instance.UpdateMainDialogue(currentData.dialoguePieces[0]);

        var giver = GetComponent<QuestGiver>();

        if (giver != null && giver.IsComplete)
        {
            SimpleUIManager.Instance.Hint(giver.currentQuest.questName+" 已完成");
        }
        //StartCoroutine(DelayedUpdate());
    }

    void ClossDialogue()
    {
        DialogueUI.Instance.ClossUI();
    }
    //IEnumerator DelayedUpdate()
    //{
    //    // 等待两秒
    //    yield return new WaitForSeconds(0.5f);

    //    // 执行后续操作
    //    DialogueUI.Instance.UpdateDialogueData(currentData);
    //    DialogueUI.Instance.UpdateMainDialogue(currentData.dialoguePieces[0]);
    //}
}
