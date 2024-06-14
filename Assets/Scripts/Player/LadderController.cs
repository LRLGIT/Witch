using UnityEngine;
using System;
using System.Collections;

public class LadderController : MonoBehaviour
{
    private Animator animator;         //���������Ժ���move
    public float climbSpeed;
    public KeyCode Key;
   public bool up, down, platform;
    public GameObject Up,Down;
    private Collider2D Playcollider;
    void Start()
    {
        // ��Start�����л�ȡAnimator���������
        animator = GetComponent<Animator>();
        Playcollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // �������ⰴ�����룬���ݰ���ѡ��Ҫ���ŵĶ���
        if (Input.GetKeyDown(Key))
        {
            if (up)
            {
                gameObject.GetComponent<PlayerController>().canMove = false;
                transform.position = Up.transform.position;
                animator.Play("climbup", 0, 0);
                StartCoroutine(MoveToPosition(Down.transform.position));
            }
            else if (down)
            {
                gameObject.GetComponent<PlayerController>().canMove = false;
                transform.position = Down.transform.position;
                animator.Play("climbdown", 0, 0);
                StartCoroutine(MoveToPosition(Up.transform.position));
                animator.SetBool("Platform", false);
            }
        }
        if (platform && down)
        {
            animator.SetBool("Platform",true);
            platform = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LadderUP"))
        {
            up = true;
        }
        if (collision.gameObject.CompareTag("LadderDown"))
        {
            down = true;
        }
      
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LadderUP"))
        {
            up = false;
        }
        if (collision.gameObject.CompareTag("LadderDown"))
        {
            down = false;
        }
    }
    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        Playcollider.isTrigger = true;

        while (transform.position != targetPosition)
        {
            // ʹ��Vector3.MoveTowards����ʹ��ҽ�ɫƽ���ƶ���Ŀ��λ��
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, climbSpeed * Time.deltaTime);
            yield return null;
        }
        Playcollider.isTrigger = false;
        gameObject.GetComponent<PlayerController>().canMove = true;
    }
}
