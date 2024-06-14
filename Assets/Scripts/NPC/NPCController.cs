using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform[] pathPoints; // ·��������


    private int currentPointIndex = 0;
    private bool stopMoving = false;

    void FixedUpdate()
    {
        if (!stopMoving && pathPoints.Length > 0)
        {
            // ����ǰ·����
            transform.position = Vector2.MoveTowards(transform.position, pathPoints[currentPointIndex].position, moveSpeed * Time.deltaTime);

            // ������ﵱǰ·���㣬��ѡ����һ��·����
            if (Vector2.Distance(transform.position, pathPoints[currentPointIndex].position) < 0.1f)
            {
                currentPointIndex = (currentPointIndex + 1) % pathPoints.Length;
            }

            // ����Ŀ����λ�õ���NPC����
            if (transform.position.x < pathPoints[currentPointIndex].position.x)
            {
                // NPC �����ƶ������ó���Ϊ��
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                // NPC �����ƶ������ó���Ϊ��
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    // ��NPC���������巢����ײʱ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �����ײ�������ǣ���ֹͣ�ƶ�
            stopMoving = true;
        }
    }

    // ��NPC�뿪������������
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ����뿪�����ǣ�������ƶ�
            stopMoving = false;
        }
    }
}
