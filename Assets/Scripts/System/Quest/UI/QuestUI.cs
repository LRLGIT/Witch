using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : Singleton<QuestUI>
{
    [Header("任务开关")]
    public KeyCode keyQuest;
    [Header("Elements")]
    public GameObject questPanel;
    public ItemTooltip tooltip;

    bool isOpen;
    [Header("Quest Name")]
    public RectTransform questListTransform;
    public QuestNameButton questNameButton;

    [Header("Text Content")]
    public Text questContentText;

    [Header("Requirement")]
    public RectTransform requireTransform;
    public QuestRequirement requirement;

    [Header("Reward")]
    public RectTransform rewardTransform;
    public ItemUI rewardUI;
    private void Update()
    {
        if(Input.GetKeyDown(keyQuest))
        {
            isOpen = !isOpen;
            questPanel.SetActive(isOpen);
            questContentText.text = string.Empty;
            //显示面板内容
            SetupQuestList();
            if (!isOpen)
                tooltip.gameObject.SetActive(false);
        }
    }

    public void SetupQuestList()
    {
        foreach(Transform item in questListTransform)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in requireTransform)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in rewardTransform)
        {
            Destroy(item.gameObject);
        }
        foreach(var task in QuestManager.Instance.tasks)
        {
            var newTask = Instantiate(questNameButton, questListTransform);
            newTask.SetupNameButton(task.questData);
            newTask.questContentText = questContentText;
        }
    }

    public void SetupRequireList(QuestData_So questData)
    {
        foreach(Transform item in requireTransform)
        {
            Destroy(item.gameObject);
        }
        foreach(var require in questData.questRequires)
        {
            var q = Instantiate(requirement, requireTransform);
            if (questData.isFinished)
                q.SetupRequirement(require.name, questData.isFinished);
            q.SetupRequirement(require.name, require.requireAmount, require.currentAmount);
        }
    }

    public void SetupRewardItem(ItemData_So itemData,int amount)
    {
        var item = Instantiate(rewardUI, rewardTransform);
        item.SetupItemUI(itemData, amount);
    }

}

