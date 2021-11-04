using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private InventorySlot heldSlot;
    private bool inventoryOpen = false;
    private InputManager inputManager;
    private GunManager gunMechanics;
    [SerializeField] private float interactRange;

    private void Awake()
    {
        gunMechanics = GetComponent<GunManager>();
    }

    private void Start()
    {
        UIManager.instance.CloseInventory();
        inputManager = InputManager.instance;
    }

    private void Update()
    {
        RaycastHit hit;
        if (!inventoryOpen)
        {
            if (Player.instance.GetHeldSlot().ItemObject != null)
            {
                heldSlot = Player.instance.GetHeldSlot();
                if (heldSlot.item.ammoInMag > 0)
                {
                    if (heldSlot.ItemObject.FireType == FireTypes.Full_Auto)
                    {
                        if (inputManager.PlayerIsFiring() && gunMechanics.cooldown <= 0)
                        {
                            gunMechanics.Shoot(heldSlot);
                        }
                    }
                    else
                    {
                        if (inputManager.PlayerFired() && gunMechanics.cooldown <= 0)
                        {
                            gunMechanics.Shoot(heldSlot);
                        }
                    }
                }
                else if (!gunMechanics.isReloading)
                {
                    StartCoroutine(gunMechanics.Reload(heldSlot));
                    Player.instance.equippedGunVisual.GetComponent<GunVisualEffect>().StartReloadAnimation(heldSlot);
                }

                if (inputManager.PlayerReloaded() && !gunMechanics.isReloading)
                {
                    StartCoroutine(gunMechanics.Reload(heldSlot));
                    Player.instance.equippedGunVisual.GetComponent<GunVisualEffect>().StartReloadAnimation(heldSlot);
                }
            }

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactRange))
            {
                if (hit.collider.CompareTag("GroundItem"))
                {
                    UIManager.instance.CanInteract();
                    if (inputManager.PickedUpGroundItem())
                    {
                        if (Player.instance.PickUpItem(hit.collider.GetComponent<GroundItem>()))
                        {
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
                else if (hit.collider.CompareTag("RandomLoot"))
                {
                    UIManager.instance.CanInteract();
                    if (inputManager.OpenedChest())
                    {
                        if (Player.instance.PickUpItem(hit.collider.GetComponent<LootableChest>().lootpool.GenerateItem()))
                        {
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
                else
                {
                    UIManager.instance.CanNotInteract();
                }
            }
            else
            {
                UIManager.instance.CanNotInteract();
            }
        }

        if (inputManager.OpenedInventory())
        {
            if (inventoryOpen)
            {
                UIManager.instance.CloseInventory();
                inventoryOpen = !inventoryOpen;
            }
            else
            {
                UIManager.instance.OpenInventory();
                inventoryOpen = !inventoryOpen;
            }
        }

        if (inputManager.EquippedSlot1())
        {
            Player.instance.SetActiveSlot(0);
        }
        else if (inputManager.EquippedSlot2())
        {
            Player.instance.SetActiveSlot(1);
        }
    }
}