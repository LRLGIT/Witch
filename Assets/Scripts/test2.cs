using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    public InventoryData_So Bag;
    private void Update()
    {
        Bag.items.Clear();  //³õÊ¼»¯±³°ü
    }
}
