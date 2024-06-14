using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RandomObjectMover : Singleton<RandomObjectMover>
{
    public bool isright = true;            //判断是否弹对
    public bool roright = true;            //判断是否是弹奏密码的时候
    public GameObject RightPanel;
    public string[] passwords;
    string password;

    //public KeyCode[] spawnKeys;            // 触发生成的按键

    public GameObject[] PrefabToSpawn;    // 要生成的预制体
    private GameObject[] prefabToSpawn;
    public Transform[] SpawnPoints;       // 生成位置数组
    private Transform[] spawnPoints;      //临时变量
    public Transform[] PraentObject;      //父物体
    Transform parentObject;        // 父物体临时变量(乐器音游界面)
    public GameObject[] Score;     //乐谱
    GameObject score;
    public GameObject LS;        //乐器切换
    LevelScrollRect LC;

    public GameObject spawnedObject;
    [Header("弹奏")]
    public float moveSpeed = 5f;         // 移动速度
    public float parabolaSpeed = 5f;     // 抛物线运动速度
    public float parabolaHeight = 5f;    // 抛物线高度

    [Header("删除位置")]
    public float destroyXPosition = -10f;

    private GameObject musicalInstrument;

    private GameObject fuwenIcon=>GameObject.FindGameObjectWithTag("Fuwen");

    private void Start()
    {
        LC = LS.GetComponent<LevelScrollRect>();
        spawnPoints = new Transform[4];
        prefabToSpawn = new GameObject[8];
        musicalInstrument = transform.Find("Musical instrument").gameObject;
        gamepad = Gamepad.current;
        
        
    }
    void Update()
    {
        password = KeyInput.Instance.keyname;
        switch (LC.label)
        {
            case 0:
                parentObject = PraentObject[0];
                for (int i = 0; i < 4 && i < SpawnPoints.Length; i++) { spawnPoints[i] = SpawnPoints[i]; };
                for (int i = 0; i < 8 && i < PrefabToSpawn.Length; i++) { prefabToSpawn[i] = PrefabToSpawn[i]; };
                score = Score[0];
                Score[1].SetActive(false);
                if(fuwenIcon!=null)fuwenIcon.SetActive(true);
                break;
            case 1:
                parentObject = PraentObject[1];
                for (int i = 0; i < 4 && i + 4 < SpawnPoints.Length; i++) { spawnPoints[i] = SpawnPoints[i + 4]; };
                for (int i = 0; i < 8 && i + 8 < PrefabToSpawn.Length; i++) { prefabToSpawn[i] = PrefabToSpawn[i + 8]; };
                score = Score[1]; 
                Score[0].SetActive(false);
                if(fuwenIcon!=null)fuwenIcon.SetActive(true);
                break;
        }
        if (musicalInstrument.activeSelf)
        {
            Open();
        }
        moveRO();
        if (parentObject.childCount == 0)
        {
            playing = false;
        }
    }
    public void keymove(string password)
    {
        for (int i = 0; i < passwords.Length; i++)
        {
            // 弹对的情况
            // 检查是否按下生成预制体的按键
            if (password == passwords[i])
            {
                // 启动协程来执行向左移动
                // 随机选择一个生成位置
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                // 生成预制体在指定父物体下
                spawnedObject = Instantiate(prefabToSpawn[i], randomSpawnPoint.position, randomSpawnPoint.rotation, parentObject);
                if(isright)
                {
                    spawnedObject.GetComponent<Animator>().Play("Play");
                }
                if (LC.label == 0)
                {
                    if (isright)
                    {
                        //Debug.Log("right");
                        StartCoroutine(MoveObjectLeft(spawnedObject.transform));
                    }
                    else
                    {
                        //Debug.Log("false");
                        StartCoroutine(MoveObjectParabola(spawnedObject.transform));
                    }
                }
                else if (LC.label == 1)
                {
                    StartCoroutine(MoveObjectLeft(spawnedObject.transform));
                }
                playing = true;
            }
        }
    }
    public void moveRO()//用来防止输密码时候两边都运行keymove
    {
        if (GameObject.Find("RightPanel") != null)
        {
            if (roright && GameObject.Find("RightPanel").activeSelf)
            {

                keymove(password);
            }
        }
    }

    IEnumerator MoveObjectLeft(Transform objTransform)
    {
        // 循环移动直到到达目标位置
        while (objTransform!=null&&objTransform.position.x > parentObject.position.x + destroyXPosition)
        {
            //if(objTransform==null)yield break;
            
            // 向左移动
            objTransform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            
            
            yield return null;
        }
        // 移动完成后销毁物体
        if(objTransform!=null)Destroy(objTransform.gameObject);
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
            if(objTransform!=null)objTransform.position = new Vector3(x, y, startPos.z - parabolaSpeed * t);

            yield return null;
        }

        // 运动完成后销毁物体
        if(objTransform!=null)Destroy(objTransform.gameObject);
    }

    [Header("乐谱开关")]
    public KeyCode key;
    public bool open;
    public bool playing;

    public Gamepad gamepad;
    void Open()
    {
        if (Input.GetKeyDown(key) || gamepad.leftTrigger.wasPressedThisFrame)
        {
            score.SetActive(!score.activeSelf);
            if (score.activeSelf)
            {
                if(fuwenIcon!=null)fuwenIcon.SetActive(false);
            }
            else
            {
                if(fuwenIcon!=null)fuwenIcon.SetActive(true);
            }
            
            gameObject.transform.Find("RightPanel").gameObject.SetActive(score.activeSelf);
            open = score.activeSelf;
            if (score.activeSelf)
            {
                ClearParentObjects();
            }
            //LC.open = open;
        }
    }
    void ClearParentObjects()
    {
        // 如果没有指定父物体，则不执行清空操作
        if (parentObject == null)
        {
            Debug.LogError("Please specify the parent object in the inspector.");
            return;
        }

        // 销毁指定父物体下的所有子物体
        foreach (Transform child in parentObject)
        {
            Destroy(child.gameObject);
        }
    }
}


