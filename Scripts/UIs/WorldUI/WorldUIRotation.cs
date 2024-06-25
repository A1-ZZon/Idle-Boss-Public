using UnityEngine;

public class WorldUIRotation : MonoBehaviour
{
    private Camera MainCamera;

    void Start()
    {
        if (MainCamera == null)
        {
            // 만약 mainCamera가 설정되지 않았다면, 태그가 "MainCamera"인 카메라를 찾아 설정
            MainCamera = Camera.main;
        }
    }

    void Update()
    {
        // UI가 항상 카메라를 향하도록 설정
        transform.LookAt(transform.position + MainCamera.transform.rotation * Vector3.forward,
                         MainCamera.transform.rotation * Vector3.up);
    }
}