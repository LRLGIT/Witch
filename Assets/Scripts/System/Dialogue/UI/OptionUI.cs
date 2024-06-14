using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public Text optionText;
    private Button thisButton;
    private DialoguePiece currentPiece;

    private bool takeQuest;

    private string nextPieceID;
    void Awake()
    {
        thisButton = GetComponent<Button>();
        // 移除现有的点击事件监听器
        thisButton.onClick.RemoveAllListeners();
        // 注册点击事件监听器
        //thisButton.onClick.AddListener(OnOptionClicked);
    }

    public void UpdateOption(DialoguePiece piece,DialogueOption option)
    {
        currentPiece = piece;
        optionText.text = option.text;
        nextPieceID = option.targetID;
        takeQuest = option.takeQuest;
    }

    public void OnOptionClicked()
    {
        if (currentPiece.quest != null)
        {
            var newTask = new QuestManager.QuestTask
            {
                questData = Instantiate(currentPiece.quest)
            };
            if(takeQuest)
            {
                //添加到任务列表
                //是否已经有任务
                if(QuestManager.Instance.HaveQuest(newTask.questData))
                {
                    //判断是否给予奖励
                    if(QuestManager.Instance.GetTask(newTask.questData).IsComplete)
                    {
                        newTask.questData.GiveRewards();
                        QuestManager.Instance.GetTask(newTask.questData).IsFinished = true;
                        
                    }
                }
                else
                {
                    //没有任务接受任务
                    QuestManager.Instance.tasks.Add(newTask);
                    QuestManager.Instance.GetTask(newTask.questData).IsStart = true;

                    foreach(var requireItem in newTask.questData.RequireTargetName())
                    {
                        InventoryManager.Instance.CheckQuestItemInBag(requireItem);
                    }
                    SimpleUIManager.Instance.Hint("任务 "+newTask.questData.questName+" 已接受");
                }
            }
        }
        if(nextPieceID=="")
        {
            DialogueUI.Instance.LayoutControl.SetActive(false);
            return;
        }
        else
        {
            DialogueUI.Instance.UpdateMainDialogue(DialogueUI.Instance.currentData.dialogueIndex[nextPieceID]);
        }
    }
}
