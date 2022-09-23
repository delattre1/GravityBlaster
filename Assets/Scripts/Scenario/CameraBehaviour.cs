using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBehaviour : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] Transform objectToFocus;

    void Start()
    {
        cinemachineVirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "CameraChange"){
            cinemachineVirtualCamera.Follow = objectToFocus;
        }
    }
}
