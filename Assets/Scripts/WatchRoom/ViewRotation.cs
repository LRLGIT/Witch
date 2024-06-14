using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRotation : MonoBehaviour
{

   public Transform game;
    [Header("按键")]
    public string Left;
    public string Right;
    [Header("每次旋转角度/速度")]
    public float Degrees;    // 分针每次旋转的角度
    public float rotationSpeed = 30.0f;   // 分针旋转的速度

    private float currentAngle = 0.0f;   // 当前分针角度

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

    }//缓慢转动角度
}
