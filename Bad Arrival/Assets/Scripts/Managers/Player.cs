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
    private int currentHP = 50;

    [SerializeField] private InventoryObject inventory;
    [SerializeField] private InventoryObject equipment;

    private void Start()
    {
        UIManager.instance.UpdateHealth(currentHP);
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

    public InventorySlot GetHeldSlot()
    {
        return  equipment.GetSlots[0];
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
}
