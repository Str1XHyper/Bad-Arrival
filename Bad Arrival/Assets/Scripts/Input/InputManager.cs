using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Singleton
    public static InputManager instance;

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }

        playerControls = new PlayerControls();
    }
    #endregion

    private PlayerControls playerControls;

    public bool MovementEnabled = true;
    public bool InteractionEnabled = true;
    public bool CombatEnabled = true;

    private void Start()
    {
        if(MovementEnabled)
        {
            playerControls.Movement.Enable();
        }
        else
        {
            playerControls.Movement.Disable();
        }
        if (InteractionEnabled)
        {
            playerControls.Interaction.Enable();
        }
        else
        {
            playerControls.Interaction.Disable();
        }
        if (CombatEnabled)
        {
            playerControls.Combat.Enable();
        }
        else
        {
            playerControls.Combat.Disable();
        }
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
        return playerControls.Movement.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.Movement.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumped()
    {
        return playerControls.Movement.Jump.triggered;
    }

    public bool OpenedInventory()
    {
        return playerControls.Interaction.OpenInventory.triggered;
    }

    public bool PickedUpGroundItem()
    {
        return playerControls.Interaction.PickUpGroundItem.triggered;
    }

    public bool OpenedChest()
    {
        return playerControls.Interaction.OpenChest.triggered;
    }
    
    public bool PlayerIsFiring()
    {
        return playerControls.Combat.Shoot.ReadValue<float>() > 0;
    }

    public bool PlayerReloaded()
    {
        return playerControls.Combat.Reload.triggered;
    }

    public void DisableControls()
    {
        playerControls.Movement.Disable();
        playerControls.Combat.Disable();
    }
    public void EnableControls()
    {
        playerControls.Movement.Enable();
        playerControls.Combat.Enable();
    }

    public bool EquippedSlot1()
    {
        return playerControls.Combat.Equipslot1.triggered;
    }

    public bool EquippedSlot2()
    {
        return playerControls.Combat.Equipslot2.triggered;
    }
}
