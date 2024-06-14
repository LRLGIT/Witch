using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Musickey : MonoBehaviour
{
    public GameObject imagePrefab; // Ҫ���ɵ� Image Prefab
    public float stretchSpeed = 50f; // �쳤�ٶ�
    //public KeyCode key;

    public string keyName;
    private Image currentImage;
    private float pressTime;
    bool canPress = true; // ��־�Ƿ���԰���
    AudioSource source;
    Gamepad gamepad;
    private void Start()
    {
        source= gameObject.GetComponent<AudioSource>();
        gamepad = Gamepad.current;
    }

    void Update()
    {
        if (canPress)
        {
            foreach (KeyInput.KeyStatus currentkey in KeyInput.Instance.KeyData)
            {
                if (currentkey.keyname == keyName)
                {
                    if (!source.isPlaying)
                    {
                        source.Play();
                    }
                    if (currentkey.isPressed) pressTime = Time.deltaTime;
                }
            }
            UpdateImageSize();
        }
        switch (keyName)
        {
            case "A": { if (gamepad.aButton.wasReleasedThisFrame) { canPress = false; source.Stop(); } break; }
            case "B": { if (gamepad.bButton.wasReleasedThisFrame){ canPress = false; source.Stop(); }break; }
            case "X": { if (gamepad.xButton.wasReleasedThisFrame) { canPress = false; source.Stop(); } break; }
            case "Y": { if (gamepad.yButton.wasReleasedThisFrame) { canPress = false; source.Stop(); } break; }
            case "Up": { if (gamepad.dpad.up.wasReleasedThisFrame) { canPress = false; source.Stop(); } break; }
            case "Down": { if (gamepad.dpad.down.wasReleasedThisFrame) { canPress = false; source.Stop(); } break; }
            case "Left": { if (gamepad.dpad.left.wasReleasedThisFrame) { canPress = false; source.Stop(); } break; }
            case "Right": { if (gamepad.dpad.right.wasReleasedThisFrame) { canPress = false; source.Stop(); } break; }
        }
        //if(canPress)
        //{
        //    if(Input.GetKeyDown(key))
        //    {
        //        source.clip = Loop;
        //        source.Play();
        //        source.loop = true;
        //    }
        //    if (Input.GetKey(key))
        //    {
        //        pressTime = Time.deltaTime;
        //    }
        //    if (Input.GetKeyUp(key))
        //    {
        //        canPress = false;
        //        source.Stop();
        //        source.loop = false; // ����ѭ������
        //        source.clip = End;
        //        source.Play();
        //    }
        //    UpdateImageSize();
        //}
    }


    private void UpdateImageSize()
    {
        if (currentImage == null)
        {
            CreateImage();
        }

        float newWidth = pressTime * stretchSpeed;
        float currentWidth = currentImage.rectTransform.sizeDelta.x;
        // �������
        float scaleFactor = currentWidth != 0 ? (currentWidth + newWidth) / currentWidth : 1f;
        currentImage.rectTransform.localScale = new Vector3(scaleFactor, 0.5f, 0.5f);

        // ���� Image ��С��λ��
        currentImage.rectTransform.sizeDelta = new Vector2(currentWidth + newWidth, currentImage.rectTransform.sizeDelta.y);
        currentImage.rectTransform.anchoredPosition = new Vector2(currentWidth / 2f, 0f); // �Ը���������Ϊ��׼�����ƶ�
    }

    private void CreateImage()
    {
        GameObject newImage = Instantiate(imagePrefab, transform);

        // ���� Image ��ê���λ�ã�ʹ���ڸ��������������ƶ�
        newImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        newImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        newImage.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

        currentImage = newImage.GetComponent<Image>();
        newImage.transform.SetAsFirstSibling();
    }
}
