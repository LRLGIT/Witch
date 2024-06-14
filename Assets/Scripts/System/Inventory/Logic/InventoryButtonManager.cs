using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryButtonManager :Singleton<InventoryButtonManager>
{
    [System.Serializable]
    public struct ButtonData
    {
        public Button button;
        public Sprite normalSprite;
        public Sprite selectedSprite;
        public ItemData_So itemData;
        public UnityEvent onClickEvent;
        public SlotHolder slotHolder; // ��� SlotHolder ����
    }

    public Sprite nothing;
    public ItemData_So currentItem;
    public Sprite Bagimage;
    public GameObject Panel;
    public ButtonData[] buttonDataArray;
    public Image icon;
    public Text itemText;
    public Text itemName;
    private int currentIndex = 0;
    private Gamepad gamepad;
    private bool complete = false;
    void Start()
    {
        // ��ȡ��һ�����ӵ��ֱ�
        if (Gamepad.all.Count > 0)
        {
            gamepad = Gamepad.all[0];
        }
        // ��̬���ɰ�ť����ӵ� buttonDataArray
        GenerateButtons();
    }

    void Update()
    {
        if (gamepad != null)
        {
            if (gamepad.leftStick.y.ReadValue() > 0.5f)
            {
                if (!complete)
                {
                    SelectButton(currentIndex - 4);
                    complete = true;
                }
            }
            else if (gamepad.leftStick.y.ReadValue() < -0.5f)
            {
                if (!complete)
                {
                    SelectButton(currentIndex + 4);
                    complete = true;
                }
            }
            else if (gamepad.leftStick.x.ReadValue() >0.5f)
            {
                if (!complete)
                {
                    SelectButton(currentIndex + 1);
                    complete = true;
                }
            }
            else if (gamepad.leftStick.x.ReadValue() < -0.5f)
            {
                if (!complete)
                {
                    SelectButton(currentIndex - 1);
                    complete = true;
                }
            }
            else
            {
                complete = false;
            }

            if (gamepad.aButton.wasPressedThisFrame)
            {
                ExecuteSelectedButtonEvent();

            }
        }
        UpdateSelectedItemInfo();
    }
    private void UpdateSelectedItemInfo()
    {
        if (currentIndex < 0 || currentIndex >= buttonDataArray.Length)
            return;
        currentItem = buttonDataArray[currentIndex].itemData;
        icon.sprite = (currentItem != null) ? currentItem.itemSprite : nothing;
        itemText.text = (currentItem != null) ? currentItem.description : null;
        itemName.text = (currentItem != null) ? currentItem.itemName : null;
    }

    public void GenerateButtons()
    {
        // ��ʼ�� buttonDataArra
        buttonDataArray = new ButtonData[Panel.transform.childCount];

        for (int i = 0; i < Panel.transform.childCount; i++)
        {
            Transform buttonTransform = Panel.transform.GetChild(i);
            Button button = buttonTransform.GetComponent<Button>();
            if (button != null)
            {
                ButtonData buttonData = new ButtonData();
                buttonData.button = button;
                buttonData.normalSprite = Bagimage;
                buttonData.selectedSprite = button.spriteState.highlightedSprite;
                buttonData.itemData = buttonTransform.GetComponent<SlotHolder>().itemUI.currentItemData;
                buttonData.onClickEvent = button.onClick;
                buttonData.slotHolder = buttonTransform.GetComponent<SlotHolder>(); // ��ȡ SlotHolder ����
                buttonDataArray[i] = buttonData;
            }
        }
    }

    void SelectButton(int newIndex)
    {
        if (buttonDataArray.Length == 0)
            return;

        if (newIndex < 0)
        {
            newIndex = buttonDataArray.Length - 1;
        }
        else if (newIndex >= buttonDataArray.Length)
        {
            newIndex = 0;
        }

        if (buttonDataArray[currentIndex].button != null && buttonDataArray[currentIndex].button.image != null)
        {
            buttonDataArray[currentIndex].button.image.sprite = buttonDataArray[currentIndex].normalSprite;
        }

        currentIndex = newIndex;
        //���������ť��Я��������
        if (buttonDataArray[currentIndex].button != null && buttonDataArray[currentIndex].button.image != null)
        {
            buttonDataArray[currentIndex].button.image.sprite = buttonDataArray[currentIndex].selectedSprite;
        }
    }

    void ExecuteSelectedButtonEvent()
    {
        if (currentIndex < 0 || currentIndex >= buttonDataArray.Length)
            return;

        // ���ð�ť�� onClick �¼�
        if(buttonDataArray!=null)buttonDataArray[currentIndex].onClickEvent?.Invoke();
    }
}
