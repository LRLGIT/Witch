using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChurchKeyManager : MonoBehaviour
{
    bool Lock;
    public ChurchUI churchUI;
    [System.Serializable]
    public struct ButtonData
    {
        public Button button;
        public Sprite normalSprite;
        public Sprite selectedSprite;
        public UnityEvent onClickEvent;
    }
    public ButtonData[] buttonDataArray;

    private int currentIndex = 0;
    private Gamepad gamepad;
    private bool complete = false;

    void Start()
    {
        // 获取第一个连接的手柄
        if (Gamepad.all.Count > 0)
        {
            gamepad = Gamepad.all[0];
        }
        // 动态生成按钮并添加到 buttonDataArray
        GenerateButtons();
    }

    void Update()
    {
        Lock = churchUI.@lock;
        //Debug.Log(Lock);
        if (gamepad != null&&!Lock)
        {
            if (gamepad.leftStick.y.ReadValue() > 0.5f)
            {
                if (!complete)
                {
                    SelectButton(currentIndex - 1);
                    complete = true;
                }
            }
            else if (gamepad.leftStick.y.ReadValue() < -0.5f)
            {
                if (!complete)
                {
                    SelectButton(currentIndex + 1);
                    complete = true;
                }
            }
            else
            {
                complete = false;
            }

            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                ExecuteSelectedButtonEvent();
                churchUI.Panel.SetActive(false);
            }
        }
    }
    void GenerateButtons()
    {
        // 初始化 buttonDataArray
        buttonDataArray = new ButtonData[gameObject.transform.childCount];

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform buttonTransform = gameObject.transform.GetChild(i);
            Button button = buttonTransform.GetComponent<Button>();

            if (button != null)
            {
                ButtonData buttonData = new ButtonData();
                buttonData.button = button;
                buttonData.normalSprite = button.image.sprite;
                buttonData.selectedSprite = button.spriteState.highlightedSprite;
                buttonData.onClickEvent = button.onClick;

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
        if (buttonDataArray[currentIndex].button != null && buttonDataArray[currentIndex].button.image != null)
        {
            buttonDataArray[currentIndex].button.image.sprite = buttonDataArray[currentIndex].selectedSprite;
        }
    }
    void ExecuteSelectedButtonEvent()
    {
        if (currentIndex < 0 || currentIndex >= buttonDataArray.Length)
            return;
        buttonDataArray[currentIndex].onClickEvent.Invoke();
    }
}
