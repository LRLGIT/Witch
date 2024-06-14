using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCameraBase[] cameras;
    private CinemachineVirtualCameraBase currentCamera;

    private void Start()
    {
        // ��ȡ��ʼ��������
        currentCamera = FindActiveVirtualCamera();
    }

    // ���ô˷������л����
    public void SwitchCamera(int cameraIndex)
    {
        // ȷ����������Ч��Χ��
        if (cameraIndex >= 0 && cameraIndex < cameras.Length)
        {
            // ���õ�ǰ���
            currentCamera.Priority = 0;

            // �����µ����
            cameras[cameraIndex].Priority = 10;

            // ���µ�ǰ���
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

        // ��ȡ���е��������
        CinemachineVirtualCameraBase[] allCameras = GameObject.FindObjectsOfType<CinemachineVirtualCameraBase>();

        // ��������������ҵ���ǰ��������
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
