using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject UI;
    public KeyCode menuToggleKey = KeyCode.Escape; // ���ô�/�رղ˵��İ���
    
    private bool isMenuOpen = false;

    void Update()
    {
        // ����Ƿ����˲˵����ذ���
        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            // ����˵��Ѿ��򿪣��͹رղ˵�������˵�δ�򿪣��ʹ򿪲˵�
            if (isMenuOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
        if (Input.GetKeyDown(menuToggleKey))
        {
            if (isMenuOpen)
            {
                CloseMenu();
            }
        }
        if(menuUI.activeSelf==false)
        {
            UI.SetActive(false);
            Time.timeScale = 1f; // �ָ���Ϸ
        }
        if(menuUI.activeSelf)
        {
            Time.timeScale = 0f; // �ָ���Ϸ
        }
    }

    // �򿪲˵�
    void OpenMenu()
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f; // ��ͣ��Ϸ
        isMenuOpen = true;
    }

    // �رղ˵�
    void CloseMenu()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f; // �ָ���Ϸ
        isMenuOpen = false;
    }
}
