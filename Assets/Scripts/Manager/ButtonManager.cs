using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [System.Serializable]
    public struct ButtonData
    {
        public Button button;
        public Sprite normalSprite;
        public Sprite selectedSprite;
        public UnityEvent onClickEvent; // 存储按钮下面的事件
    }

    public ButtonData[] buttonDataArray; // 存储每个按钮的数据

    private int currentIndex = 0; // 当前选中的按钮索引
    private Gamepad gamepad; // 手柄输入

    bool complete = false; // 判断是否完成选择

    void Start()
    {
        // 获取第一个连接的手柄
        if (Gamepad.all.Count > 0)
        {
            gamepad = Gamepad.all[0];
        }
    }

    void Update()
    {
        if (gamepad != null)
        {
            if (gamepad.leftStick.y.ReadValue() > 0.5f) // 上方向键
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

            if (gamepad.buttonSouth.wasPressedThisFrame) // 按下确定键
            {
                ExecuteSelectedButtonEvent(); // 执行选中按钮的事件
            }
        }
    }

    void SelectButton(int newIndex)
    {
        // 恢复上一个选中按钮的图片
        buttonDataArray[currentIndex].button.image.sprite = buttonDataArray[currentIndex].normalSprite;

        // 确保索引在有效范围内
        if (newIndex < 0)
        {
            newIndex = buttonDataArray.Length - 1;
        }
        else if (newIndex >= buttonDataArray.Length)
        {
            newIndex = 0;
        }

        // 更新当前选中按钮的索引
        currentIndex = newIndex;

        // 改变新选中按钮的图片
        buttonDataArray[currentIndex].button.image.sprite = buttonDataArray[currentIndex].selectedSprite;
    }

    void ExecuteSelectedButtonEvent()
    {
        // 执行当前选中按钮的事件
        buttonDataArray[currentIndex].onClickEvent.Invoke();
    }
}
