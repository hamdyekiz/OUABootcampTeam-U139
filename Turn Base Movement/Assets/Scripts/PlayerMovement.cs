using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float groundCheckDistance = 0.2f;
    public LayerMask ground;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    private Vector3 moveDirection;
    private Vector3 velocity;
    private bool isGrounded;
    private CharacterController controller;
    private Transform cameraTransform;
    private Animator anim;
    private bool canMove = true; 
    private bool isCursorLocked = true; 

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform; 

        Cursor.lockState = canMove ? CursorLockMode.Locked : CursorLockMode.None;
        isCursorLocked = canMove; 
        Cursor.visible = false;
    }

    private void Update()
    {
        if (canMove) 
            Move();
    }

    public void SetCanMove(bool value)
    {
        canMove = value;

       
        if (canMove)
        {
            Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = false; 
        }
        else
        {
            isCursorLocked = Cursor.lockState == CursorLockMode.Locked; 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, ground);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = transform.right * moveX + transform.forward * moveZ;
        moveDirection.Normalize();

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up, mouseX);

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && moveDirection != Vector3.zero;
        moveSpeed = isRunning ? runSpeed : walkSpeed;

        if (moveDirection == Vector3.zero)
        {
            Idle();
        }
        else if (isRunning)
        {
            Run();
        }
        else
        {
            Walk();
        }

        moveDirection *= moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime + velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
