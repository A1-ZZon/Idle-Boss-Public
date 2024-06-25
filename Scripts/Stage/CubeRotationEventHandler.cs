using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum ERotateType{RotateX, RotateZ} // X가 상하, Z가 좌우회전
public class CubeRotationEventHandler : MonoBehaviour
{
    
    [Header("# Rotation Info")]
    [SerializeField] private float rotationDuration;
    [SerializeField] private float delayDuration;
        
    [Header("# Camera Event Info")]
    [SerializeField] private CameraController cam;

    public event Action OnRotatinEnd;
    private Coroutine rotateCubeCoroutine;
    private WaitForSeconds wait;
    private Quaternion targetRot;
    private Quaternion currentRot;
    private Dictionary<ERotateType, Vector3> rotateDic;
    
    private void Awake()
    {
        wait = new WaitForSeconds(delayDuration);
        rotateDic = new Dictionary<ERotateType, Vector3>
        {
            { ERotateType.RotateX, new Vector3(-90, 0, 0) },
            { ERotateType.RotateZ, new Vector3(0, 0, -90) }
        };
    }
    public void RotateCube(ERotateType rotateType) // 카메라, 큐브효과
    {
        if (rotateCubeCoroutine != null)
        {
            StopCoroutine(rotateCubeCoroutine);
        }
        
        rotateCubeCoroutine = StartCoroutine(RotateCubeCoroutine(rotateType));
    }

    private IEnumerator RotateCubeCoroutine(ERotateType rotateType)
    {
        cam.ToggleCameraTransition(true);
        
        yield return wait;
        
        cam.CameraShakeEffect(rotationDuration);
        
        float currentTime = 0f;
        Quaternion currentRot = transform.rotation;
        Quaternion targetRot = Quaternion.Euler(rotateDic[rotateType])*currentRot ;
        
        while (currentTime < rotationDuration)
        {
            currentTime += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(currentRot, targetRot, currentTime / rotationDuration);
            yield return null;
        }
        transform.rotation = targetRot;

        OnRotatinEnd?.Invoke();
        yield return wait;
        cam.ToggleCameraTransition(false);
    }
}
