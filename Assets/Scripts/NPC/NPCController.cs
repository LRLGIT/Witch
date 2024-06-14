using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform[] pathPoints; // 路径点数组


    private int currentPointIndex = 0;
    private bool stopMoving = false;

    void FixedUpdate()
    {
        if (!stopMoving && pathPoints.Length > 0)
        {
            // 移向当前路径点
            transform.position = Vector2.MoveTowards(transform.position, pathPoints[currentPointIndex].position, moveSpeed * Time.deltaTime);

            // 如果到达当前路径点，则选择下一个路径点
            if (Vector2.Distance(transform.position, pathPoints[currentPointIndex].position) < 0.1f)
            {
                currentPointIndex = (currentPointIndex + 1) % pathPoints.Length;
            }

            // 根据目标点的位置调整NPC朝向
            if (transform.position.x < pathPoints[currentPointIndex].position.x)
            {
                // NPC 向右移动，设置朝向为右
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                // NPC 向左移动，设置朝向为左
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    // 当NPC与其他物体发生碰撞时调用
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 如果碰撞到了主角，则停止移动
            stopMoving = true;
        }
    }

    // 当NPC离开其他物体后调用
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 如果离开了主角，则继续移动
            stopMoving = false;
        }
    }
}
