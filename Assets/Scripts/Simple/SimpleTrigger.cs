using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTrigger : MonoBehaviour
{
    public string context;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& StaticData.stringInfo.Contains(context) == false)
        {
            SimpleUIManager.Instance.Hint(context);
            StaticData.stringInfo.Add(context);
        }
    }
}
