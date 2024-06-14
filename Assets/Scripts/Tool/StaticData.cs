using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    
    public static bool fired = false;

    public static HashSet<string> stringInfo = new HashSet<string>();
    
    public static HashSet<AppearType> appeared = new HashSet<AppearType>();

    public static bool leftUnLocked=false, rightUnLocked=false;

    public static bool leftDoorOpened=false;

    public static Action<ItemData_So> onUseItem;
    
    public static Action onDialogEnd;
    
    public static Action onDialogStart;


    public static bool playerIsFree =>
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().canTalk;
    
    

    public static void ClearData()
    {
        fired = false;
        stringInfo.Clear();
        appeared.Clear();
        
        leftUnLocked = false;
        rightUnLocked = false;
        
        leftDoorOpened = false;
        
        onUseItem = null;
        
        onDialogEnd = null;
        
        onDialogStart = null;
        
        
        
    }
    
}
