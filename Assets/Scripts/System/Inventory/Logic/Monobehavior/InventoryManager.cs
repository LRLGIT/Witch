using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [Header("����������")]
    public GameObject thing;
    public string []AnimatorName;
    public GameObject Panel;
    private CanvasGroup panelGroup;
    [Header("��������")]
    public KeyCode key;
    private bool isOpen;
    private bool isRightTriggerPressed;
    
    
    void Update()
    {
        // ��鰴�����Ұ�����Ƿ���
        if (!isRightTriggerPressed && (Input.GetKeyDown(key) || Input.GetAxis("LRT") > 0.5f))
        {
            isOpen = !isOpen;
            if (isOpen)
            {
                Panel.SetActive(isOpen);
                if (panelGroup != null) panelGroup.DOFade(1f,0.5f).OnComplete(() =>
                {
                    
                });
            }
            else
            {
                if (panelGroup != null) panelGroup.DOFade(0,0.5f).OnComplete(() =>
                {
                    Panel.SetActive(isOpen);
                });
            }
            
           
            isRightTriggerPressed = true;
        }
        // �����Ұ��������״̬
        if (Input.GetAxis("LRT") <= 0.5f)
        {
            isRightTriggerPressed = false;
        }
        if (thing != null && thingAnimator != null )
        {
            if (isOpen)
            {
                if(thingAnimator!=null)thingAnimator.Play(AnimatorName[0]);
                //if (panelGroup != null) panelGroup.DOFade(0,0.5f);
            }
            else if (!isOpen)
            {

                if(thingAnimator!=null)thingAnimator.Play(AnimatorName[1]);
                //if (panelGroup != null) panelGroup.DOFade(1f,0.5f);
            }
        }
    }
    public class DragData
    {
        public SlotHolder originalHolder;
        public RectTransform originalParent;
    }
    //TODO:������ģ�����ڱ�������
    [Header("Inventory Data")]
    public InventoryData_So inventoryData;  //��ʾ�ı���
    public InventoryData_So virtuBagData;



    [Header("Container")]
    public ContainerUI inventoryUI;

    public ContainerUI equipmentUI;

    [Header("Drag Canvas")]
    public Canvas dragCanvas;
    public DragData currentDrag;

    public InventoryButtonManager buttonManager;
    private Animator panelAnimator;
    private Animator thingAnimator;

    private void Start()
    {
        thingAnimator = thing.GetComponent<Animator>();
        panelAnimator = Panel.GetComponent<Animator>();
        
        panelGroup=Panel.GetComponent<CanvasGroup>();
        inventoryUI.RefreshUI();
        // ��ʼ��Animator�ֶ�
        if (thing != null)
        {
            thingAnimator = thing.GetComponent<Animator>();
    }

        if (Panel != null)
        {
            panelAnimator = Panel.GetComponent<Animator>();
        }
    }
    #region �����Ʒ�Ƿ���slot��Χ��
    public bool CheckInInventoryUI(Vector3 position)
    {
        for(int i =0;i<inventoryUI.slotHolders.Length;i++)
        {
            RectTransform t = inventoryUI.slotHolders[i].transform as RectTransform;
            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
            {
                return true;
            }
        }
        return false;
    }
    #endregion
    public void CheckItemInBag()
    {
        foreach (var item in inventoryData.items)
        {
            if(item.amount==0)
            {
                item.itemData = null;
            }
        }
    }
    public void CheckQuestItemInBag(string questItemName)
    {
        foreach(var item in virtuBagData.items)
        {
            if(item.itemData!=null)
            {
                if (item.itemData.itemName == questItemName)
                    QuestManager.Instance.UpdataQuestProgress(item.itemData.itemName, item.amount);
            }
        }
    }
    public InventoryItem QuestItemInBag(ItemData_So questItem)
    {
        return virtuBagData.items.Find(i => i.itemData == questItem);
    }
}
