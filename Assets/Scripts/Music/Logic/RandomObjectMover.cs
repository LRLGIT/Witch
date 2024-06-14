using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RandomObjectMover : Singleton<RandomObjectMover>
{
    public bool isright = true;            //�ж��Ƿ񵯶�
    public bool roright = true;            //�ж��Ƿ��ǵ��������ʱ��
    public GameObject RightPanel;
    public string[] passwords;
    string password;

    //public KeyCode[] spawnKeys;            // �������ɵİ���

    public GameObject[] PrefabToSpawn;    // Ҫ���ɵ�Ԥ����
    private GameObject[] prefabToSpawn;
    public Transform[] SpawnPoints;       // ����λ������
    private Transform[] spawnPoints;      //��ʱ����
    public Transform[] PraentObject;      //������
    Transform parentObject;        // ��������ʱ����(�������ν���)
    public GameObject[] Score;     //����
    GameObject score;
    public GameObject LS;        //�����л�
    LevelScrollRect LC;

    public GameObject spawnedObject;
    [Header("����")]
    public float moveSpeed = 5f;         // �ƶ��ٶ�
    public float parabolaSpeed = 5f;     // �������˶��ٶ�
    public float parabolaHeight = 5f;    // �����߸߶�

    [Header("ɾ��λ��")]
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
            // ���Ե����
            // ����Ƿ�������Ԥ����İ���
            if (password == passwords[i])
            {
                // ����Э����ִ�������ƶ�
                // ���ѡ��һ������λ��
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                // ����Ԥ������ָ����������
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
    public void moveRO()//������ֹ������ʱ�����߶�����keymove
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
        // ѭ���ƶ�ֱ������Ŀ��λ��
        while (objTransform!=null&&objTransform.position.x > parentObject.position.x + destroyXPosition)
        {
            //if(objTransform==null)yield break;
            
            // �����ƶ�
            objTransform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            
            
            yield return null;
        }
        // �ƶ���ɺ���������
        if(objTransform!=null)Destroy(objTransform.gameObject);
    }

    IEnumerator MoveObjectParabola(Transform objTransform)
    {
        float startTime = Time.time;
        Vector3 startPos = objTransform.position;

        // ѭ��ִ���������˶�
        while (Time.time - startTime <= 2f) // �����˶���ʱ��
        {
            float t = (Time.time - startTime) / 1f; // ��һ��ʱ��

            // ���������߹�ʽ���������λ��
            float x = startPos.x - parabolaSpeed * t;
            float y = startPos.y - parabolaHeight * t * t;

            // �����·����ƶ�
            if(objTransform!=null)objTransform.position = new Vector3(x, y, startPos.z - parabolaSpeed * t);

            yield return null;
        }

        // �˶���ɺ���������
        if(objTransform!=null)Destroy(objTransform.gameObject);
    }

    [Header("���׿���")]
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
        // ���û��ָ�������壬��ִ����ղ���
        if (parentObject == null)
        {
            Debug.LogError("Please specify the parent object in the inspector.");
            return;
        }

        // ����ָ���������µ�����������
        foreach (Transform child in parentObject)
        {
            Destroy(child.gameObject);
        }
    }
}


