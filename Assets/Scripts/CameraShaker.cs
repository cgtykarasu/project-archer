using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Vector3 positionStrength;
    [SerializeField] Vector3 rotationStrength;
    
    static event Action Shake;

    public static void Invoke()
    {
        Shake?.Invoke();
    }

    void OnEnable() => Shake += CameraShake;
    void OnDisable() => Shake -= CameraShake;
    

    void CameraShake()
    {
        // cameraTransform.localPosition = new Vector3(Random.Range(-positionStrength.x, positionStrength.x),
        //     Random.Range(-positionStrength.y, positionStrength.y),
        //     Random.Range(-positionStrength.z, positionStrength.z));
        //
        // cameraTransform.localRotation = Quaternion.Euler(new Vector3(Random.Range(-rotationStrength.x, rotationStrength.x),
        //     Random.Range(-rotationStrength.y, rotationStrength.y),
        //     Random.Range(-rotationStrength.z, rotationStrength.z)));7

        cameraTransform.DOComplete();
        cameraTransform.DOShakePosition(0.25f, positionStrength);
        cameraTransform.DOShakeRotation(0.25f, rotationStrength);
    }
}
