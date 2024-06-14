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
        public UnityEvent onClickEvent; // �洢��ť������¼�
    }

    public ButtonData[] buttonDataArray; // �洢ÿ����ť������

    private int currentIndex = 0; // ��ǰѡ�еİ�ť����
    private Gamepad gamepad; // �ֱ�����

    bool complete = false; // �ж��Ƿ����ѡ��

    void Start()
    {
        // ��ȡ��һ�����ӵ��ֱ�
        if (Gamepad.all.Count > 0)
        {
            gamepad = Gamepad.all[0];
        }
    }

    void Update()
    {
        if (gamepad != null)
        {
            if (gamepad.leftStick.y.ReadValue() > 0.5f) // �Ϸ����
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

            if (gamepad.buttonSouth.wasPressedThisFrame) // ����ȷ����
            {
                ExecuteSelectedButtonEvent(); // ִ��ѡ�а�ť���¼�
            }
        }
    }

    void SelectButton(int newIndex)
    {
        // �ָ���һ��ѡ�а�ť��ͼƬ
        buttonDataArray[currentIndex].button.image.sprite = buttonDataArray[currentIndex].normalSprite;

        // ȷ����������Ч��Χ��
        if (newIndex < 0)
        {
            newIndex = buttonDataArray.Length - 1;
        }
        else if (newIndex >= buttonDataArray.Length)
        {
            newIndex = 0;
        }

        // ���µ�ǰѡ�а�ť������
        currentIndex = newIndex;

        // �ı���ѡ�а�ť��ͼƬ
        buttonDataArray[currentIndex].button.image.sprite = buttonDataArray[currentIndex].selectedSprite;
    }

    void ExecuteSelectedButtonEvent()
    {
        // ִ�е�ǰѡ�а�ť���¼�
        buttonDataArray[currentIndex].onClickEvent.Invoke();
    }
}
