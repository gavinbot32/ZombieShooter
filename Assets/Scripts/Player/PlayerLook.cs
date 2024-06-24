using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("Looking")]
    [SerializeField] private Vector2 sensitivity = new Vector2(3f, 3f);

    [Header("Components")]
    [SerializeField] Transform cam;
    [SerializeField] private Transform cameraPos;
    [SerializeField] private Transform orientation;
    private Vector2 mouseInput;
    

    float multiplier = 0.01f;

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        ToggleCursor(true);
        cam.SetParent(null);
    }
    private void Update()
    {
        MyInput();
        cam.position = cameraPos.position;
        cam.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0,yRotation, 0);
    }
    private void MyInput()
    {
        yRotation += mouseInput.x * sensitivity.x * multiplier;
        xRotation -= mouseInput.y * sensitivity.y * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }
    public void OnMouseInput(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }


    private void ToggleCursor(bool locked)
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
