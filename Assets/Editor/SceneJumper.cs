using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJumper : EditorWindow  
{  
    [MenuItem("Tools/Scene Loader")] // 在Unity编辑器中添加一个菜单项来打开窗口  
    public static void ShowWindow()  
    {  
        // 显示已存在的窗口实例。如果没有，则创建一个新的。  
        EditorWindow.GetWindow<SceneJumper>("Scene Loader");  
    }  
  
    private string[] sceneNames; // 存储场景名称的数组  
  
    private int selectedSceneIndex = -1; // 当前选中的场景索引  


    private InventoryData_So bag;
    private InventoryData_So virtualBag;

    private string[] guids;
    //private List<ItemData_So> itemDatas;
    //private List<string> itemNames; 
    private ItemType selectedType = ItemType.Useable;
    private int selectedItemIndex = -1;

    private Dictionary<ItemType, List<ItemData_So>> itemDic;
    private void OnEnable()  
    {  
        // 当窗口启用时，获取所有场景的名称  
        sceneNames = EditorBuildSettings.scenes.Select(scene => Path.GetFileNameWithoutExtension(scene.path)).ToArray();  
        
        
        guids = AssetDatabase.FindAssets("t:ItemData_So");
        itemDic = new Dictionary<ItemType, List<ItemData_So>>();
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ItemData_So item = AssetDatabase.LoadAssetAtPath<ItemData_So>(path);
            if (item != null)
            {
                if (itemDic.ContainsKey(item.itemType))
                {
                    itemDic[item.itemType].Add(item);
                }
                else
                {
                    itemDic.Add(item.itemType, new List<ItemData_So>() { item });
                }
            }
        }
    }  
  
    private void OnGUI()  
    {  
        
        // 使用GUILayout来布局窗口内容  
        GUILayout.Label("Select a scene to load:", EditorStyles.boldLabel);  
  
        // 创建一个下拉菜单来显示场景名称  
        selectedSceneIndex = EditorGUILayout.Popup(selectedSceneIndex, sceneNames);  
  
        // 创建一个按钮来加载选中的场景  
        if (GUILayout.Button("Load Scene"))  
        {  
            if (selectedSceneIndex >= 0 && selectedSceneIndex < sceneNames.Length)  
            {  
                // 根据选中的索引加载场景  
                string scenePath = EditorBuildSettings.scenes[selectedSceneIndex].path;  
                EditorApplication.LoadLevelInPlayMode(scenePath);
            }  
        }  
        
        
        GUILayout.Space(10f);

        EditorGUILayout.ObjectField("Bag",bag, typeof(InventoryData_So), false);
        EditorGUILayout.ObjectField("VirtualBag",virtualBag, typeof(InventoryData_So), false);
        
        GUILayout.Label("Item to gain:", EditorStyles.boldLabel); 
        
        selectedType = (ItemType)EditorGUILayout.EnumPopup("Type:",selectedType);  
        selectedItemIndex = EditorGUILayout.Popup(selectedItemIndex, itemDic[selectedType].Select(item => item.itemName).ToArray());  
        
        if (GUILayout.Button("Add Item"))  
        {  
            if (itemDic.ContainsKey(selectedType)&&selectedItemIndex >= 0 && selectedItemIndex < itemDic[selectedType].Count)  
            {
                try
                {
                    GameObject.FindWithTag("Dialog").GetComponent<DialogueUI>().ObtainUI(itemDic[selectedType][selectedItemIndex]);
                }
                catch (NullReferenceException e)
                {
                    Debug.LogError("错误：场景里没有Dialogue和Inventory Canvas或Dialogue Canvas的Tag不是Dialog");
                    Console.WriteLine(e);
                    throw;
                }

                //EditorApplication.LoadLevelInPlayMode(SceneManager.GetActiveScene().path);
            }  
        } 
        
    }  
}