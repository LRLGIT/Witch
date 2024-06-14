using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SavaGamenManager : Singleton<SavaGamenManager>
{
    // �洢��ǰ�������Ƶ��ֶ�
    private string currentSceneName;

    // ���浱ǰ����������
    public void SaveCurrentSceneName()
    {
        // ��ȡ��ǰ����������
        currentSceneName = SceneManager.GetActiveScene().name;

        // ������Խ��������Ʊ��浽��Ĵ浵�����У�����PlayerPrefs�����Զ���Ĵ浵�ļ���
        // ��PlayerPrefsΪ��
        PlayerPrefs.SetString("SavedSceneName", currentSceneName);
        PlayerPrefs.Save(); // ����PlayerPrefs����
    }

    // ���ر���ĳ���
    public void LoadSavedScene()
    {
        // ��ȡ����ĳ�������
        string savedSceneName = PlayerPrefs.GetString("SavedSceneName", "");
        // ����Ƿ��б���ĳ�������
        if (!String.IsNullOrEmpty(savedSceneName))
        {
            // ���ر���ĳ���
            SceneManager.LoadScene(savedSceneName);
        }
        else
        {
            Debug.LogWarning("No saved scene found!");
        }
    }
}
