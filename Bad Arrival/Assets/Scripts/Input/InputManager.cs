using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private PlayerControls playerControls;

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }

        playerControls = new PlayerControls();
    }


    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumped()
    {
        return playerControls.Player.Jump.triggered;
    }

    public void DisableControls()
    {
        playerControls.Disable();
    }
    public void EnableControls()
    {
        playerControls.Enable();
    }
}
