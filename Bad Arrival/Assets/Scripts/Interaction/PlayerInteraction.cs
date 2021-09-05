using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] private GameObject physicalBulletImpact;
    private InventorySlot heldSlot;
    [SerializeField] private float interactRange;

    public int cooldown;
    private bool inventoryOpen = false;
    private void Start()
    {
        UIManager.instance.CloseInventory();
    }

    private void Update()
    {
        RaycastHit hit;
        if (!inventoryOpen)
        {
            if(Player.instance.GetHeldSlot().ItemObject != null)
            {
                heldSlot = Player.instance.GetHeldSlot();
                if (heldSlot.ItemObject.FireType == FireTypes.Full_Auto)
                {
                    if (Input.GetKey(KeyCode.Mouse0) && cooldown <= 0)
                    {
                        Shoot(heldSlot);
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0) && cooldown <= 0)
                    {
                        Shoot(heldSlot);
                    }
                }
            }

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactRange) && hit.collider.tag == "Interactable")
            {
                UIManager.instance.CanInteract();
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (Player.instance.PickUpItem(hit.collider.GetComponent<GroundItem>()))
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

        if (Input.GetKeyDown(KeyCode.Tab))
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
        Debug.Log(heldGun.ItemObject.GetRPM(heldGun.item));

        Transform transform = Camera.main.transform;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            Instantiate(physicalBulletImpact, hit.point, transform.rotation);
            Debug.Log(transform.forward);
        }
        cooldown = Mathf.RoundToInt(60f / (heldGun.ItemObject.GetRPM(heldGun.item)) / Time.fixedDeltaTime);
    }
}