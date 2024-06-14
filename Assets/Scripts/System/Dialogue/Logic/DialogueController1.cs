using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController1 : MonoBehaviour
{
    public bool one = false;
    public DialogueData_So currentData;
    public int num;
    public bool end;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")&&currentData!=null)
        {
            OpenDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        ClossDialogue();
    }
    private void Update()
    {
        if (DialogueUI.Instance.end == true&&DialogueUI.Instance.currentData==currentData)
        { end = DialogueUI.Instance.end; }    
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClossDialogue();
        }
    }
    void OpenDialogue()
    {
        //打开UI面版
        //传输对话内容信息
        DialogueUI.Instance.UpdateDialogueData(currentData);
        DialogueUI.Instance.UpdateMainDialogue(currentData.dialoguePieces[0]);
    }

    void ClossDialogue()
    {
        DialogueUI.Instance.ClossUI();
    }

    private void OnDestroy()
    {
        Debug.Log(666);
    }
}
