using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRay : MonoBehaviour
{
    [SerializeField]private Transform mainCamera;

    void Update()
    {
        transform.rotation = mainCamera.rotation;
        Debug.Log(transform.rotation);
    }
}
