using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGiver : MonoBehaviour
{
    bool isTrigger;
    DialogueController controller;
    public List<DialogueData_So> dP = new List<DialogueData_So>();
    public int i = 0;
    private void Start()
    {
        GameObject dialogueCanvas = GameObject.Find("Dialogue Canvas");
        if (dialogueCanvas != null)
        {

        }

        // 确保对话列表非空
        if (dP.Count == 0)
        {
            Debug.LogWarning("Dialogue list is empty in DialogueGiver on GameObject: " + gameObject.name);
        }
        else
        {
            controller.currentData = dP[0]; // 只有在dP列表不为空时才分配值
        }
    }

    private void Awake()
    {
        controller = GetComponent<DialogueController>();
    }



    private void Update()
    {
        if (controller.end && i < dP.Count - 1&&isTrigger)
        {
            i = controller.num;
            NextDialogue();
        }
    }

    private void NextDialogue()
    {
        // 更新对话，并在结束时将 dialogueCompleted 设置为 true
        controller.currentData = dP[i];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = false;
    }
}
