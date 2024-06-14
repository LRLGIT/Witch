using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {Useable,quest,Fuwen,UI }
[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/Item Data")]
public class ItemData_So : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public Sprite itemSprite;
    public int itemAmount;
    [TextArea]
    public string description = "";
    public bool stackable;

    [Header("prefab")]
    public GameObject Prefab;
}
