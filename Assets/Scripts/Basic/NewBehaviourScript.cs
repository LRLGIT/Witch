using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        // 使用 FindObjectsOfType 方法查找场景中所有具有 MusicStart 组件的游戏对象
        MusicChange[] musicStartComponents = FindObjectsOfType<MusicChange>();

        if (musicStartComponents.Length > 0)
        {
            // 找到了至少一个 MusicStart 组件
            // 迭代数组并输出每个组件所在的游戏对象名称
            foreach (MusicChange musicStartComponent in musicStartComponents)
            {
                // 输出游戏对象的名称
                Debug.Log("Found MusicStart component on GameObject: " + musicStartComponent.gameObject.name);
            }
        }
        else
        {
            // 如果没有找到任何具有 MusicStart 组件的游戏对象，输出错误日志
            Debug.LogError("No MusicStart components found in the scene.");
        }
    }
}
