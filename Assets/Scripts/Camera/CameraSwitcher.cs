using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCameraBase[] cameras;
    private CinemachineVirtualCameraBase currentCamera;

    private void Start()
    {
        // 获取初始激活的相机
        currentCamera = FindActiveVirtualCamera();
    }

    // 调用此方法来切换相机
    public void SwitchCamera(int cameraIndex)
    {
        // 确保索引在有效范围内
        if (cameraIndex >= 0 && cameraIndex < cameras.Length)
        {
            // 禁用当前相机
            currentCamera.Priority = 0;

            // 启用新的相机
            cameras[cameraIndex].Priority = 10;

            // 更新当前相机
            currentCamera = cameras[cameraIndex];
        }
        else
        {
            Debug.LogError("Invalid camera index");
        }
    }

    private CinemachineVirtualCameraBase FindActiveVirtualCamera()
    {
        CinemachineVirtualCameraBase activeCamera = null;

        // 获取所有的虚拟相机
        CinemachineVirtualCameraBase[] allCameras = GameObject.FindObjectsOfType<CinemachineVirtualCameraBase>();

        // 遍历所有相机，找到当前激活的相机
        foreach (var camera in allCameras)
        {
            if (camera.isActiveAndEnabled && camera.Follow != null && camera.Follow == Camera.main.transform)
            {
                activeCamera = camera;
                break;
            }
        }

        return activeCamera;
    }
}
