using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchRotation : MonoBehaviour
{
    public Transform Minute, Hour;
    [Header("按键")]
    public string Left;
    public string Right;
    [Header("每次旋转角度/速度")]

    public float clockwiseRotationDegrees;    // 顺时针旋转角度
    public float anticlockwiseRotationDegrees;    // 逆时针旋转角度
    public float rotationSpeed = 30.0f;   // 旋转速度

    [Header("初始角度分针/时针")]
    public float MinuteAngle = 0.0f;   // 当前分针角度
    public float HourAngle = 0.0f;     // 当前时针角度

    private float currentMinuteAngle = 0.0f;   // 当前分针角度
    private float currentHourAngle = 0.0f;     // 当前时针角度

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
            AddRotation(); // 改为顺时针旋转
        }
        else if (KeyInput.Instance.keyname == Right)
        {
            MinusRotation(); // 改为逆时针旋转
        }
        AnimateRotation();
    }

    public void SetInitialRotation()//初始化
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

    private void AnimateRotation()//缓慢转动角度
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

        // 定义一个误差范围，比如0.01度
        float errorMargin = 0.01f;

        // 如果分针和时针的角度都在误差范围内，返回true，否则返回false
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
