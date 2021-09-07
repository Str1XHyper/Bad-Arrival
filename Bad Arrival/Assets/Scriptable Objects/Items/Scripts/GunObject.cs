using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireTypes
{
    Full_Auto,
    Semi_Auto,
    Bolt_Action,
    Single_Fire,
}

public enum GunTypes
{
    Energy,
    Physical
}

[CreateAssetMenu(fileName = "New Gun", menuName = "Inventory System/Weapon/Gun")]
public class GunObject : ItemObject
{
    private void Awake()
    {
        itemType = ItemType.Gun;
    }
}


