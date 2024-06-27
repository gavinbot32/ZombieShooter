using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Billboard : MonoBehaviour
{
    [Header("Leave blank for main")]
    [FormerlySerializedAs("camera")] public Camera cam;

    private void Start()
    {
        if(cam == null)
        {
            cam = Camera.main;
        }
    }

    private void LateUpdate()
    {
        transform.rotation = cam.transform.rotation;
    }

}
