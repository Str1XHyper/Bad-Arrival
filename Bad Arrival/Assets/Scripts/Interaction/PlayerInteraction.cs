using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] private GameObject physicalBulletImpact;
    private InventorySlot heldSlot;
    [SerializeField] private float interactRange;
    public LayerMask IgnoredLayer;

    public int cooldown;
    private bool inventoryOpen = false;
    private InputManager inputManager;
    private bool isReloading = false;

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
            if(Player.instance.GetHeldSlot().ItemObject != null)
            {
                heldSlot = Player.instance.GetHeldSlot();
                if(heldSlot.item.ammoInMag > 0)
                {
                    if (heldSlot.ItemObject.FireType == FireTypes.Full_Auto)
                    {
                        if (inputManager.PlayerIsFiring() && cooldown <= 0)
                        {
                            Shoot(heldSlot);
                        }
                    }
                    else
                    {
                        if (inputManager.PlayerIsFiring() && cooldown <= 0)
                        {
                            Shoot(heldSlot);
                        }
                    }
                }
                else if(!isReloading)
                {
                    StartCoroutine(Reload(heldSlot));
                }

                if (inputManager.PlayerReloaded() && !isReloading)
                {
                    StartCoroutine(Reload(heldSlot));
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
                } else if (hit.collider.CompareTag("RandomLoot"))
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
    }

    private void FixedUpdate()
    {
        cooldown--;
    }

    private void Shoot(InventorySlot heldGun)
    {
        Transform transform = Camera.main.transform;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ~IgnoredLayer, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().ApplyDamage(heldGun.ItemObject.GetDamage(heldGun.item));
                UIManager.instance.ShowHitmarker();
            }

            Instantiate(physicalBulletImpact, hit.point, transform.rotation);
        }
        heldGun.item.ammoInMag--;
        cooldown = Mathf.RoundToInt(60f / (heldGun.ItemObject.GetRPM(heldGun.item)) / Time.fixedDeltaTime);
        
    }

    private IEnumerator Reload(InventorySlot heldGun)
    {
        isReloading = true;
        yield return new WaitForSeconds(heldGun.ItemObject.GetReloadTime(heldGun.item));
        heldGun.item.ammoInMag = heldGun.ItemObject.GetMagCapacity(heldGun.item);
        isReloading = false;
    }
}