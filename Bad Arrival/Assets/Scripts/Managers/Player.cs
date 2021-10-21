using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Singleton
    public static Player instance;

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
    }
    #endregion

    public int MaxHP = 150;
    private int currentHP = 150;


    public int equippedSlot { get; private set; } = 0;

    [SerializeField] private InventoryObject inventory;
    [SerializeField] private InventoryObject equipment;

    private void Start()
    {
        UIManager.instance.UpdateHealth(currentHP);
        if(equipment.GetSlots[equippedSlot].ItemObject)
            CrosshairManager.instance.SetCrosshair(equipment.GetSlots[equippedSlot].ItemObject.Crosshair);
        else
            CrosshairManager.instance.SetCrosshair(null);
    }

    public bool PickUpItem(GroundItem groundItem)
    {
        Item _item = new Item(groundItem.item);
        if (inventory.AddItem(_item, 1))
        {
            return true;
        } else
        {
            return false;
        }
    }

    public InventorySlot[] GetEquippedGuns()
    {
        return equipment.GetSlots;
    }

    public InventorySlot GetHeldSlot()
    {
        return equipment.GetSlots[equippedSlot];
    }

    public void ApplyDamage(int damage)
    {
        currentHP -= damage;
        UIManager.instance.UpdateHealth(currentHP);
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.LogWarning("Player DIED!");
        //Play death animation
    }

    public int GetHealth()
    {
        return currentHP;
    }

    public void ResetHealth()
    {
        currentHP = MaxHP;
    }

    public bool PickUpItem(ItemObject itemObject)
    {
        Item _item = new Item(itemObject);
        if (inventory.AddItem(_item, 1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetActiveSlot(int slot)
    {
        equippedSlot = slot;
        UIManager.instance.SetActiveWeapon(slot);
        if (equipment.GetSlots[equippedSlot].ItemObject)
        {
            CrosshairManager.instance.SetCrosshair(equipment.GetSlots[equippedSlot].ItemObject.Crosshair);
        }
        else
        {
            CrosshairManager.instance.SetCrosshair(null);
        }
    }
}
