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

    [SerializeField] private InventoryObject inventory;
    [SerializeField] private InventoryObject equipment;

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
}
