using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController charController;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3;

    private Vector3 velocity;

    [SerializeField] private Transform groundCheck;
    private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xMovement + transform.forward * zMovement;
        
        charController.Move(move * movementSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        charController.Move(velocity * Time.deltaTime);
    }
}
