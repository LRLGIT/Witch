using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        // ʹ�� FindObjectsOfType �������ҳ��������о��� MusicStart �������Ϸ����
        MusicChange[] musicStartComponents = FindObjectsOfType<MusicChange>();

        if (musicStartComponents.Length > 0)
        {
            // �ҵ�������һ�� MusicStart ���
            // �������鲢���ÿ��������ڵ���Ϸ��������
            foreach (MusicChange musicStartComponent in musicStartComponents)
            {
                // �����Ϸ���������
                Debug.Log("Found MusicStart component on GameObject: " + musicStartComponent.gameObject.name);
            }
        }
        else
        {
            // ���û���ҵ��κξ��� MusicStart �������Ϸ�������������־
            Debug.LogError("No MusicStart components found in the scene.");
        }
    }
}
