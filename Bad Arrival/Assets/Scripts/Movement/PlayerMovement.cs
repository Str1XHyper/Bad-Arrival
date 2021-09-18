using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController charController;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3;

    private Vector3 velocity;

    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;

    private Transform cameraTransform;

    private InputManager inputManager;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        inputManager = InputManager.instance;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        isGrounded = charController.isGrounded;

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        //Vector3 move = transform.right * xMovement + transform.forward * zMovement;
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;

        charController.Move(move * movementSpeed * Time.deltaTime);

        if (inputManager.PlayerJumped() && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        charController.Move(velocity * Time.deltaTime);
    }
}
