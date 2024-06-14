using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
     void Awake()
     {

         var fades=GameObject.FindGameObjectsWithTag("Fade");
        
        if(fades.Length<2)DontDestroyOnLoad(this);
    }
    void Update()
    {
        // 检查当前场景的名称
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 如果当前场景是特定场景（比如"YourSceneName"），则禁用该对象
        if (currentSceneName == "YourSceneName")
        {
            gameObject.SetActive(false);
        }
        else
        {
            // 如果不是特定场景，确保对象是启用的
            gameObject.SetActive(true);
        }
    }
}
