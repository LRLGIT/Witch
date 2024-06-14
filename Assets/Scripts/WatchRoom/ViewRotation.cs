using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRotation : MonoBehaviour
{

   public Transform game;
    [Header("����")]
    public string Left;
    public string Right;
    [Header("ÿ����ת�Ƕ�/�ٶ�")]
    public float Degrees;    // ����ÿ����ת�ĽǶ�
    public float rotationSpeed = 30.0f;   // ������ת���ٶ�

    private float currentAngle = 0.0f;   // ��ǰ����Ƕ�

    public void Start()
    {
        currentAngle = 0.0f;
    }

    private void Update()
    {
        if (KeyInput.Instance.keyname == Left)
        {
            AddRotation();
        }
        else if (KeyInput.Instance.keyname == Right)
        {
            MinusRotation();
        }

        AnimateRotation();
    }

    private void AddRotation()
    {
        currentAngle += Degrees;
    }

    private void MinusRotation()
    {
        currentAngle -= Degrees;
    }

    private void AnimateRotation()
    {
        Quaternion targetMinuteRotation = Quaternion.Euler(0f, 0f, currentAngle);

        float currentMinuteAngleDifference = Quaternion.Angle(game.rotation, targetMinuteRotation);


        if (currentMinuteAngleDifference > 0.01f)
        {
            game.rotation = Quaternion.RotateTowards(game.rotation, targetMinuteRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            game.rotation = targetMinuteRotation;
        }

    }//����ת���Ƕ�
}
