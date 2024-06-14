using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyInput : Singleton<KeyInput>
{
    public bool iskeydown;
    public string keyname;

    private Gamepad gamepad;

    [System.Serializable]
    public struct KeyStatus
    {
        public string keyname;
        public bool isPressed; //��������
        public float pressTime;
    }
    public KeyStatus[] KeyData;

    private void Start()
    {
        gamepad = Gamepad.current;
    }

    private void Update()
    {
        keyname = "";
        iskeydown = false;
        // A ��ť�߼�
        if (gamepad.aButton.wasPressedThisFrame && !KeyData[0].isPressed)
        {
            KeyDown(0);
        }
        if (gamepad.aButton.wasReleasedThisFrame)
        {
            KeyUp(0);
        }
        // B ��ť�߼�
        if (gamepad.bButton.wasPressedThisFrame && !KeyData[1].isPressed)
        {
            KeyDown(1);
        }
        if (gamepad.bButton.wasReleasedThisFrame)
        {
            KeyUp(1);
        }
        // X ��ť�߼�
        if (gamepad.xButton.wasPressedThisFrame && !KeyData[2].isPressed)
        {

            KeyDown(2);
        }
        if (gamepad.xButton.wasReleasedThisFrame)
        {
            KeyUp(2);
        }
        // Y ��ť�߼�
        if (gamepad.yButton.wasPressedThisFrame && !KeyData[3].isPressed)
        {

            KeyDown(3);
        }
        if (gamepad.yButton.wasReleasedThisFrame)
        {
            KeyUp(3);
        }
        // �Ϸ�����߼�
        if (gamepad.dpad.up.wasPressedThisFrame && !KeyData[4].isPressed)
        {

            KeyDown(4);
        }
        if (gamepad.dpad.up.wasReleasedThisFrame)
        {
            KeyUp(4);
        }
        // �·�����߼�
        if (gamepad.dpad.down.wasPressedThisFrame && !KeyData[5].isPressed)
        {
            KeyDown(5);
        }
        if (gamepad.dpad.down.wasReleasedThisFrame)
        {
            KeyUp(5);
        }
        // ������߼�
        if (gamepad.dpad.left.wasPressedThisFrame && !KeyData[6].isPressed)
        {
            KeyDown(6);
        }
        if (gamepad.dpad.left.wasReleasedThisFrame)
        {
            KeyUp(6);
        }
        // �ҷ�����߼�
        if (gamepad.dpad.right.wasPressedThisFrame && !KeyData[7].isPressed)
        {
            KeyDown(7);
        }
        if (gamepad.dpad.right.wasReleasedThisFrame)
        {
            KeyUp(7);
        }
    }

    void KeyDown(int i)
    {
        KeyData[i].isPressed = true;
        KeyData[i].pressTime = 0f;
        keyname = KeyData[i].keyname;
        iskeydown = true;
        StartCoroutine(UpdateKeyPressTime(i));
    }
    void KeyUp(int i)
    {
        KeyData[i].isPressed = false;
        keyname = "";
        iskeydown = false;
    }

    IEnumerator UpdateKeyPressTime(int i)
    {
        while (KeyData[i].isPressed)
        {
            KeyData[i].pressTime += Time.deltaTime;
            yield return null;
        }
    }


}

