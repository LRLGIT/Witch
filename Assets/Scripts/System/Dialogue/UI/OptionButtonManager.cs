using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionButtonManager : MonoBehaviour
{
    [System.Serializable]
    public struct ButtonData
    {
        public Button button;
        public Sprite normalSprite;
        public Sprite selectedSprite;
        public UnityEvent onClickEvent;
    }

    public Transform optionPanel;
    public ButtonData[] buttonDataArray;

    private int currentIndex = 0;
    private Gamepad gamepad;

    private int previousButtonCount = 0;
    private bool complete = false;

    void Start()
    {
        currentIndex = 0;
        // 获取第一个连接的手柄
        if (Gamepad.all.Count > 0)
        {
            gamepad = Gamepad.all[0];
        }

        // 如果 optionPanel 为null，尝试在场景中查找
        if (optionPanel == null)
        {
            optionPanel = GameObject.Find("OptionPanel").transform;
        }

        // 动态生成按钮并添加到 buttonDataArray
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

            if (gamepad.aButton.wasPressedThisFrame)
            {
                ExecuteSelectedButtonEvent();
            }
        }

        UpdateButtonData(); // 检查按钮数据
    }

    void GenerateButtons()   //动态更新
    {
        // 初始化 buttonDataArray
        buttonDataArray = new ButtonData[optionPanel.childCount];

        for (int i = 0; i < optionPanel.childCount; i++)
        {
            Transform buttonTransform = optionPanel.GetChild(i);
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
        // 更新上次的按钮数量
        previousButtonCount = optionPanel.childCount;
    }

    void UpdateButtonData()
    {
        //检查按钮数量是否发生变化
        if (optionPanel.childCount != previousButtonCount)
        {
            GenerateButtons(); // 重新生成按钮数据
            previousButtonCount = optionPanel.childCount; // 更新按钮数量
        }
        //GenerateButtons(); // 重新生成按钮数据
        //previousButtonCount = optionPanel.childCount; // 更新按钮数量
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
        currentIndex = 0;
    }
}
