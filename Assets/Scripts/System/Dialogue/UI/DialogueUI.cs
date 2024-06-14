using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueUI : Singleton<DialogueUI>
{
    public bool end;
    public KeyCode Endkey;
    public KeyCode Nextkey;
    public int num;
    [Header("Basic Elements")]
    public Image iconL;
    public Image iconR;
    public Text mainname;
    public Text mainText;
    public Button nextButton;
    public GameObject LayoutControl;
    public Animator DialoguePanel,imageL,imageR;
    [Header("Option")]
    public RectTransform optionPanel;
    public OptionUI optionPrefab;
    [Header("Data")]
    public DialogueData_So currentData;
    int currentIndex = 0;

    [Header("字完整出来时间")]
    public float time = 1f;
    [Header("获得物品显示时间")]
    public float delayTime=4f;
    [Header("提示")]
    public GameObject Panel;
    public Image image; // 使用 Image 类型而不是 GameObject
    public Text text;

    private Tweener textTween;
      public void Update()  //有问题
     {
        if (end && Input.GetKeyDown(Endkey))
        {
            if (textTween != null && textTween.IsPlaying())
            {
                //mainText.text = " ";
            

                textTween.Complete();
                return;
            }
            ClossUI();
        }
        // 检测键盘按下事件，KeyCode.A 表示A键
        if (Input.GetKeyDown(Nextkey))
        {
            // 只有对话框是激活状态且 nextButton 是可交互的时候才触发对话
            if (LayoutControl.activeSelf && nextButton.interactable)
            {
                Debug.Log(123);
                ContinueDialogue();
            }
        }
    } 
    protected override void Awake()
    {
        base.Awake();
        nextButton.onClick.AddListener(ContinueDialogue);//改成手柄按下：按下A打开按下B关闭界面
    }
    private void Start()
    {
        num = 0;
        end = false;
    }

    void ContinueDialogue()
    {
        //Debug.Log(123);
        if (currentIndex < currentData.dialoguePieces.Count&&!end)
        {
            //Debug.Log(123);
            UpdateMainDialogue(currentData.dialoguePieces[currentIndex]);

        }
        else
        {
            //Debug.Log(123);
            LayoutControl.SetActive(false);
            StaticData.onDialogEnd?.Invoke();
        }
    }
    public void UpdateDialogueData(DialogueData_So data) 
    {
        currentData = data;
        currentIndex = 0;
    }
    public void UpdateMainDialogue(DialoguePiece piece)
    {
        if (textTween != null && textTween.IsPlaying())
        {
            //mainText.text = " ";
            

            textTween.Complete();
            return;
        }
        end = piece.END;
        num = piece.num;
        LayoutControl.SetActive(true);
        StaticData.onDialogStart?.Invoke();
        //DialoguePanel.Play("Play");
        imageL.Play("Play");
        imageR.Play("Play");
        currentIndex++;
        if (piece.imageL != null)
        {
            iconL.enabled = true;
            iconL.sprite = piece.imageL;
        }
        else iconL.enabled = false;
        if (piece.imageR != null)
        {
            iconR.enabled = true;
            iconR.sprite = piece.imageR;
        }
        else iconR.enabled = false;
        mainname.text = piece.Name;                         
        mainText.text = "";
        //mainText.text = piece.text;
        textTween=mainText.DOText(piece.text, time);
        //mainText.DOText(piece.text, piece.text.Length * time).SetEase(Ease.Linear);

        if (piece.options.Count == 0 && currentData.dialoguePieces.Count > 0&&!end)
        {
            nextButton.interactable = true;
            nextButton.gameObject.SetActive(true);
            nextButton.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            nextButton.interactable = false;
            nextButton.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (piece.item != null)
        {
            ObtainUI(piece.item);
            CloseObtainUI(); // 传递延迟时间参数
        }
        //创建options
        CreateOptions(piece);
    }
    public void ObtainUI(ItemData_So itemData)//拾取UI物品
    {
        //存储到背包
        if (itemData.itemType == ItemType.Useable||itemData.itemType==ItemType.UI)
        {
            Debug.Log(InventoryManager.Instance);
            Debug.Log(InventoryManager.Instance.inventoryData);
            InventoryManager.Instance.inventoryData.AddItem(itemData, itemData.itemAmount);//添加进实际背包
            InventoryManager.Instance.inventoryUI.RefreshUI();
            InventoryManager.Instance.buttonManager.GenerateButtons();
            Panel.SetActive(true);
            image.sprite = itemData.itemIcon;
            text.text = "获得物品 " + itemData.itemName;
        }
        else if(itemData.itemType==ItemType.Fuwen)
        {
            if(FuWenManager.Instance==null)return;
            FuWenManager.Instance.ReplaceSpriteByItemData(itemData);
            Panel.SetActive(true);
            image.sprite = itemData.itemIcon;
            text.text = "获得物品 " + itemData.itemName;
        }
        else if(itemData.itemType==ItemType.quest)
        {
            InventoryManager.Instance.virtuBagData.AddItem(itemData, itemData.itemAmount);//添加进虚拟背包
            QuestManager.Instance.UpdataQuestProgress(itemData.itemName, itemData.itemAmount);
        }
    }

    public void CloseObtainUI()
    {
        StartCoroutine(CloseUIAfterDelay(delayTime));
    }

    private IEnumerator CloseUIAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Panel.SetActive(false);
        image.sprite = null;
        text.text = null;
    }
    void CreateOptions(DialoguePiece piece)//创建options
    {
        if (optionPanel.childCount > 0)
        {
            for (int i = 0; i < optionPanel.childCount; i++)//销毁所有的Option
            {
                Destroy(optionPanel.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < piece.options.Count; i++)
        {
            var option = Instantiate(optionPrefab, optionPanel);//生成所有的Option
            option.UpdateOption(piece, piece.options[i]);//更新Option的Text
        }
    }
    public void ClossUI()
    {
        LayoutControl.SetActive(false);
        StaticData.onDialogEnd?.Invoke();
        num =0;
        end = false;
    }

}
