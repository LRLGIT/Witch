using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchRotation : MonoBehaviour
{
    public Transform Minute, Hour;
    [Header("����")]
    public string Left;
    public string Right;
    [Header("ÿ����ת�Ƕ�/�ٶ�")]

    public float clockwiseRotationDegrees;    // ˳ʱ����ת�Ƕ�
    public float anticlockwiseRotationDegrees;    // ��ʱ����ת�Ƕ�
    public float rotationSpeed = 30.0f;   // ��ת�ٶ�

    [Header("��ʼ�Ƕȷ���/ʱ��")]
    public float MinuteAngle = 0.0f;   // ��ǰ����Ƕ�
    public float HourAngle = 0.0f;     // ��ǰʱ��Ƕ�

    private float currentMinuteAngle = 0.0f;   // ��ǰ����Ƕ�
    private float currentHourAngle = 0.0f;     // ��ǰʱ��Ƕ�

    private void Start()
    {
        currentMinuteAngle = MinuteAngle;
        currentHourAngle = HourAngle;
        SetInitialRotation();
    }

    private void Update()
    {
        if (KeyInput.Instance.keyname == Left)
        {
            AddRotation(); // ��Ϊ˳ʱ����ת
        }
        else if (KeyInput.Instance.keyname == Right)
        {
            MinusRotation(); // ��Ϊ��ʱ����ת
        }
        AnimateRotation();
    }

    public void SetInitialRotation()//��ʼ��
    {
        Minute.rotation = Quaternion.Euler(0f, 0f, MinuteAngle);
        Hour.rotation = Quaternion.Euler(0f, 0f, HourAngle);
        currentMinuteAngle = MinuteAngle;
        currentHourAngle = HourAngle;
    }

    private void AddRotation()
    {
        currentMinuteAngle += clockwiseRotationDegrees;
        currentHourAngle += clockwiseRotationDegrees / 12.0f;
    }

    private void MinusRotation()
    {
        currentMinuteAngle -= anticlockwiseRotationDegrees;
        currentHourAngle -= anticlockwiseRotationDegrees / 12.0f;
    }

    private void AnimateRotation()//����ת���Ƕ�
    {
        Quaternion targetMinuteRotation = Quaternion.Euler(0f, 0f, currentMinuteAngle);
        Quaternion targetHourRotation = Quaternion.Euler(0f, 0f, currentHourAngle);

        float currentMinuteAngleDifference = Quaternion.Angle(Minute.rotation, targetMinuteRotation);
        float currentHourAngleDifference = Quaternion.Angle(Hour.rotation, targetHourRotation);

        if (currentMinuteAngleDifference > 0.01f)
        {
            Minute.rotation = Quaternion.RotateTowards(Minute.rotation, targetMinuteRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            Minute.rotation = targetMinuteRotation;
        }

        if (currentHourAngleDifference > 0.01f)
        {
            Hour.rotation = Quaternion.RotateTowards(Hour.rotation, targetHourRotation, Time.deltaTime * rotationSpeed / 12f);
        }
        else
        {
            Hour.rotation = targetHourRotation;
        }
    }

    public bool IsRightRotation()
    {
        float minuteAngle = Minute.rotation.eulerAngles.z;
        float hourAngle = Hour.rotation.eulerAngles.z;

        // ����һ����Χ������0.01��
        float errorMargin = 0.01f;

        // ��������ʱ��ĽǶȶ�����Χ�ڣ�����true�����򷵻�false
        if (Mathf.Abs(minuteAngle) < errorMargin && Mathf.Abs(hourAngle) < errorMargin)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
