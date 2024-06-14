using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject UI;
    public KeyCode menuToggleKey = KeyCode.Escape; // 设置打开/关闭菜单的按键
    
    private bool isMenuOpen = false;

    void Update()
    {
        // 检测是否按下了菜单开关按键
        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            // 如果菜单已经打开，就关闭菜单；如果菜单未打开，就打开菜单
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
            Time.timeScale = 1f; // 恢复游戏
        }
        if(menuUI.activeSelf)
        {
            Time.timeScale = 0f; // 恢复游戏
        }
    }

    // 打开菜单
    void OpenMenu()
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f; // 暂停游戏
        isMenuOpen = true;
    }

    // 关闭菜单
    void CloseMenu()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f; // 恢复游戏
        isMenuOpen = false;
    }
}
