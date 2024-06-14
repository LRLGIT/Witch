using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TestMusic : MonoBehaviour
{
    public bool isright=true;            //�ж��Ƿ񵯶�
    public bool roright = true;          //�ж��Ƿ��ǵ��������ʱ��
    public KeyCode []spawnKeys;             // �������ɵİ���
    public GameObject []prefabToSpawn;     // Ҫ���ɵ�Ԥ����
    public Transform[] spawnPoints;      // ����λ������
    public Transform parentObject;        // ָ���ĸ�����(�������ν���)


    [Header("����")]
    public float moveSpeed = 5f;         // �ƶ��ٶ�
    public float parabolaSpeed = 5f;     // �������˶��ٶ�
    public float parabolaHeight = 5f;    // �����߸߶�

    [Header("ɾ��λ��")]
    public float destroyXPosition = -10f;

    void Update()
    {
        moveRO();
    }
    public void keymove()
    {
        for (int i = 0; i < spawnKeys.Length; i++)
        {
            // ���Ե����
            // ����Ƿ�������Ԥ����İ���
            if (Input.GetKeyDown(spawnKeys[i]))
            {
                // ����Э����ִ�������ƶ�
                // ���ѡ��һ������λ��
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                // ����Ԥ������ָ����������
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
                    //����Э�̵���ȥ
                    StartCoroutine(MoveObjectParabola(spawnedObject.transform));
                }
            }
        }
    }

    public void moveRO()//������ֹ������ʱ�����߶�����keymove
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
        // ѭ���ƶ�ֱ������Ŀ��λ��
        while (objTransform.position.x > parentObject.position.x+ destroyXPosition)
        {
            // �����ƶ�
            objTransform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            yield return null;
        }

        // �ƶ���ɺ���������
        Destroy(objTransform.gameObject);
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
            objTransform.position = new Vector3(x, y, startPos.z - parabolaSpeed * t);

            yield return null;
        }

        // �˶���ɺ���������
        Destroy(objTransform.gameObject);
    }

}


