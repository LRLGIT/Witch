using UnityEngine;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public List<BoxCollider2D> mapBoundsList;

    void LateUpdate()
    {
        if (player != null && mapBoundsList.Count > 0)
        {
            // 获取主角的位置
            Vector3 playerPos = player.position;

            // 初始化一个包围盒，包含所有地图边界
            Bounds totalBounds = new Bounds(mapBoundsList[0].bounds.center, mapBoundsList[0].bounds.size);
            for (int i = 1; i < mapBoundsList.Count; i++)
            {
                totalBounds.Encapsulate(mapBoundsList[i].bounds);
            }

            // 限制摄像机的x和y坐标在所有地图边界的范围内
            float clampedX = Mathf.Clamp(playerPos.x, totalBounds.min.x, totalBounds.max.x);
            float clampedY = Mathf.Clamp(playerPos.y, totalBounds.min.y, totalBounds.max.y);

            // 设置摄像机的位置
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
