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
    public int Damage;
    public int RecoilStrength;
    public int RoundsPerMinute;
    public float HeadshotMultiplier;
    public int MagazineCapacity;
    public FireTypes FireType;
    public GunTypes GunType;
    public GameObject GunModel;

    private void Awake()
    {
        itemType = ItemType.Gun;
    }
}
