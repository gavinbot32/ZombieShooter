using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [Header("Leave blank for main")]
    public Camera camera;

    private void Start()
    {
        if(camera == null)
        {
            camera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        transform.rotation = camera.transform.rotation;
    }

}
