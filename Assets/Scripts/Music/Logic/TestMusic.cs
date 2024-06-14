using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TestMusic : MonoBehaviour
{
    public bool isright=true;            //判断是否弹对
    public bool roright = true;          //判断是否是弹奏密码的时候
    public KeyCode []spawnKeys;             // 触发生成的按键
    public GameObject []prefabToSpawn;     // 要生成的预制体
    public Transform[] spawnPoints;      // 生成位置数组
    public Transform parentObject;        // 指定的父物体(乐器音游界面)


    [Header("弹奏")]
    public float moveSpeed = 5f;         // 移动速度
    public float parabolaSpeed = 5f;     // 抛物线运动速度
    public float parabolaHeight = 5f;    // 抛物线高度

    [Header("删除位置")]
    public float destroyXPosition = -10f;

    void Update()
    {
        moveRO();
    }
    public void keymove()
    {
        for (int i = 0; i < spawnKeys.Length; i++)
        {
            // 弹对的情况
            // 检查是否按下生成预制体的按键
            if (Input.GetKeyDown(spawnKeys[i]))
            {
                // 启动协程来执行向左移动
                // 随机选择一个生成位置
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                // 生成预制体在指定父物体下
                GameObject spawnedObject = Instantiate(prefabToSpawn[i], randomSpawnPoint.position, randomSpawnPoint.rotation, parentObject);
                AudioSource audioSource = spawnedObject.GetComponent<AudioSource>();
                audioSource.Play();
                if (isright)
                {
                    //Debug.Log("right");
                    StartCoroutine(MoveObjectLeft(spawnedObject.transform));
                }
                else
                {
                    //Debug.Log("false");
                    //启动协程掉下去
                    StartCoroutine(MoveObjectParabola(spawnedObject.transform));
                }
            }
        }
    }

    public void moveRO()//用来防止输密码时候两边都运行keymove
    {
        if (GameObject.Find("RightPanel")!=null)
        {
            if (roright && GameObject.Find("RightPanel").activeSelf)
            {
                keymove();
            }
        }
    }

IEnumerator MoveObjectLeft(Transform objTransform)
    {
        // 循环移动直到到达目标位置
        while (objTransform.position.x > parentObject.position.x+ destroyXPosition)
        {
            // 向左移动
            objTransform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            yield return null;
        }

        // 移动完成后销毁物体
        Destroy(objTransform.gameObject);
    }

IEnumerator MoveObjectParabola(Transform objTransform)
    {
        float startTime = Time.time;
        Vector3 startPos = objTransform.position;

        // 循环执行抛物线运动
        while (Time.time - startTime <= 2f) // 控制运动的时间
        {
            float t = (Time.time - startTime) / 1f; // 归一化时间

            // 根据抛物线公式计算物体的位置
            float x = startPos.x - parabolaSpeed * t;
            float y = startPos.y - parabolaHeight * t * t;

            // 向左下方向移动
            objTransform.position = new Vector3(x, y, startPos.z - parabolaSpeed * t);

            yield return null;
        }

        // 运动完成后销毁物体
        Destroy(objTransform.gameObject);
    }

}


