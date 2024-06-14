using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SavaGamenManager : Singleton<SavaGamenManager>
{
    // 存储当前场景名称的字段
    private string currentSceneName;

    // 保存当前场景的名称
    public void SaveCurrentSceneName()
    {
        // 获取当前场景的名称
        currentSceneName = SceneManager.GetActiveScene().name;

        // 这里可以将场景名称保存到你的存档数据中，比如PlayerPrefs或者自定义的存档文件中
        // 以PlayerPrefs为例
        PlayerPrefs.SetString("SavedSceneName", currentSceneName);
        PlayerPrefs.Save(); // 保存PlayerPrefs数据
    }

    // 加载保存的场景
    public void LoadSavedScene()
    {
        // 获取保存的场景名称
        string savedSceneName = PlayerPrefs.GetString("SavedSceneName", "");
        // 检查是否有保存的场景名称
        if (!String.IsNullOrEmpty(savedSceneName))
        {
            // 加载保存的场景
            SceneManager.LoadScene(savedSceneName);
        }
        else
        {
            Debug.LogWarning("No saved scene found!");
        }
    }
}
