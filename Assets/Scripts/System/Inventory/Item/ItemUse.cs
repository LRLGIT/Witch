using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ItemUse : MonoBehaviour
{
    bool isTrigger = false;
    [Header("场景中")]
    public GameObject thing;
    public GameObject thing2;
    [Header("虚拟任务道具")]
    public ItemData_So quesetItem;
    [Header("显示UI")]
    public GameObject UIPanel;
    [Header("声音")]
    public AudioSource sound;
    [Header("动画(过渡)名称")]
    public string AnimatorName;
    public ChurchUI churchUI;
    [Header("移动+传送")]
    public Transform initialpath;
    public Transform paths;

    public Sprite watch;
    public Sprite letter;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            InventoryManager.Instance.inventoryUI.RefreshUI();
        }
    }
    public float moveSpeed;
    public void UseItem(ItemData_So item)   //增加删除道具
    {

        if (isTrigger)
        {
            //Debug.Log(item.itemName);
            switch (item.itemName)
            {
                case ("·蓝钥匙·"):
                case ("·红钥匙E·"):
                    thing.GetComponent<Animator>().SetBool(AnimatorName, true);//开柜门
                    sound.Play();
                    churchUI.@lock = false;
                    ReduceItem(item);
                    //AddItem(quesetItem);
                    //if(!churchUI.Lock) thing.GetComponent<Animator>().SetBool(AnimatorName, true);//开柜门
                    break;
                case ("·齿轮·"):
                    thing.SetActive(true);
                    ReduceItem(item);
                    AddItem(quesetItem);
                    break;
                case ("·把手·"):
                    if (!churchUI.@lock)
                    {
                        thing.GetComponent<Animator>().SetBool(AnimatorName, true);//把手转动
                        if (FindItem("Door_key"))
                        {
                            AddItem(quesetItem);
                            sound.Play();
                            ReduceItem(item);
                        }//如果虚拟钥匙存在则可以用把手
                        else
                        {
                            UIPanel.SetActive(true);
                            //ReduceItem(item);
                        }
                    }
                    break;
                case ("·水铃兰·"):
                case ("·酒·"):
                    StartCoroutine(MoveAfterDelay(1.0f, initialpath));
                    thing.GetComponent<Animator>().Play(AnimatorName);//角色动画播放
                    thing2.SetActive(false);
                    AddItem(quesetItem);
                    StartCoroutine(MoveAfterDelay(2.0f, paths));
                    ReduceItem(item);
                    break;
                case ("·花籽·"):
                    AddItem(quesetItem);
                    sound.Play();
                    ReduceItem(item);
                    UIPanel.SetActive(true);
                    break;
            }
        }
        else if (item.itemName == "·怀表·")
        {
            UIPanel.GetComponent<Image>().sprite = watch;
            UIPanel.GetComponent<Image>().SetNativeSize();
            UIPanel.GetComponent<Image>().SetNativeSize();
            
            UIPanel.SetActive(true);
        }
        else if (item.itemName == "·一封信·")
        {
            UIPanel.GetComponent<Image>().sprite = letter;
            UIPanel.GetComponent<Image>().SetNativeSize();
            UIPanel.SetActive(true);
        }
    }
    bool FindItem(string itemName) //寻找虚拟道具是否存在
    {
        foreach (var item in InventoryManager.Instance.virtuBagData.items)
        {
            if (item.itemData != null && item.itemData.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }
    void ReduceItem(ItemData_So item)
    {
        InventoryManager.Instance.inventoryData.AddItem(item, -item.itemAmount);//从实际背包删除
        InventoryManager.Instance.CheckItemInBag();
        InventoryManager.Instance.buttonManager.GenerateButtons();//更新按钮
        InventoryManager.Instance.inventoryUI.RefreshUI();
    }
    void AddItem(ItemData_So item)
    {
        InventoryManager.Instance.virtuBagData.AddItem(item,item.itemAmount);
        InventoryManager.Instance.buttonManager.GenerateButtons();
        InventoryManager.Instance.inventoryUI.RefreshUI();
        QuestManager.Instance.UpdataQuestProgress(item.itemName, item.itemAmount);
    }

    IEnumerator MoveAfterDelay(float delay,Transform path)
    {
        yield return new WaitForSeconds(delay); // 等待指定的秒数

        // 开始移动物体
        StartCoroutine(MoveToPathCoroutine(path));
    }

    IEnumerator MoveToPathCoroutine(Transform paths)
    {
        // 关闭碰撞体
        thing.GetComponent<Collider2D>().isTrigger = true;

        // 计算移动方向
        Vector3 direction = (paths.position - thing.transform.position).normalized;

        // 循环移动物体直到到达目标位置
        while (Vector2.Distance(thing.transform.position, paths.position) > 0.1f)
        {
            // 移动物体
            thing.transform.position += direction * moveSpeed * Time.deltaTime;

            yield return null; // 等待一帧
        }

        // 到达目标位置后重新打开碰撞体
        thing.GetComponent<Collider2D>().isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag==("Player"))
        {
            isTrigger=true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == ("Player"))
        {
            isTrigger=false;
        }
    }
}
