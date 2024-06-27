using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private Vector2 moveInput;
    private Vector3 moveDirection;
    private Vector3 slopeMoveDirection;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float movementMultiplier = 10f;
    [SerializeField] private float airMultiplier = 0.4f;
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 0f;
    private float baseSpeed;

    private RaycastHit slopeHit;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 5f;
    private float playerHeight = 2f;
    [Header("Ground Detection")]
    private bool isGrounded;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundLayerMask;

    [Header("Components")]
    [SerializeField] private Rigidbody rig;
    [SerializeField] private Transform orientation;

    private void Awake()
    {
        rig = this.SafeGetComponent(rig);
        rig.freezeRotation = true;
        baseSpeed = moveSpeed;
    }

    private void Update()
    {
        isGrounded = IsGrounded();
        MoveInput();
        ControlDrag();

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position - new Vector3(0, playerHeight / 2, 0), groundDistance, groundLayerMask);
    }
    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position,Vector3.down, out slopeHit, playerHeight/ 2 + 0.5f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    private void Jump()
    {
        print("Jump");
        rig.AddForce(transform.up * (jumpForce * rig.mass), ForceMode.Impulse);
    }

    private void ControlDrag()
    {
        if (isGrounded)
        {
            rig.drag = groundDrag;
        }
        else
        {
            if(rig.velocity.y > 0)
            {
                rig.drag = airDrag;
            }
            else
            {
                rig.drag = airDrag/2;
            }
        }
    }

    private void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rig.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }else if (isGrounded && OnSlope())
        {
            rig.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if(!isGrounded)
        {
            rig.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    private void MoveInput()
    {
        moveDirection = orientation.forward * moveInput.normalized.y + orientation.right * moveInput.normalized.x;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnSprintInput(InputAction.CallbackContext context)
    {
        if (!isGrounded)
        {
            moveSpeed = baseSpeed;
            return;
        }
        if (context.performed)
        {
            moveSpeed = baseSpeed * 1.5f;
        }
        if (context.canceled)
        {
            moveSpeed = baseSpeed;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (isGrounded) {
            Jump();
        }
    }
}
