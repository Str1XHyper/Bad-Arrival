using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Gun,
}

public enum Modifiers
{
    Damage,
    Recoil,
    Fire_Rate,
    Magazine_Capacity,
    Reload_Time
}

public enum Rarities
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

public class ItemObject : ScriptableObject
{
    public ItemType itemType;
    public string Name;
    [TextArea(15, 20)]
    public string Description;
    public Rarities Rarity;

    public Sprite uiDisplay;
    public bool stackable;
    public Item data = new Item();
    public GameObject groundItem;

    #region Gun Data

    [SerializeField] protected int BaseDamage;
    [SerializeField] [Range(0,100)] protected int BaseRecoilStrength;
    [SerializeField] protected int BaseRoundsPerMinute;
    [SerializeField] protected int BaseMagazineCapacity;
    [SerializeField] protected float BaseReloadTime;

    public Texture2D Crosshair;
    public int GetDamage(Item item)
    {
        return Mathf.RoundToInt(BaseDamage * ((float)item.buffs[0].value / 100f + 1));
    }

    public int GetRecoilStrength(Item item)
    {
        return Mathf.RoundToInt(BaseRecoilStrength * (1 - (float)item.buffs[1].value / 100f));
    }

    public int GetRPM(Item item)
        {
        return Mathf.RoundToInt(BaseRoundsPerMinute * ((float)item.buffs[2].value / 100f + 1));
    }

    public int GetMagCapacity(Item item)
    {
        return Mathf.RoundToInt(BaseMagazineCapacity * ((float)item.buffs[3].value / 100f + 1));
    }

    public float GetReloadTime(Item item)
    {
        return BaseReloadTime * (1 - item.buffs[4].value / 100f);
    }


    public FireTypes FireType;
    public GunTypes GunType;
    public GameObject Model;
    public float HeadshotMultiplier;

    public AudioClip gunshotSFX;
    public AudioClip rackingSFX;
    [Range(0,1f)]
    public float volumeScale = 1;
    public string fmodDirectory;
    public string fmodDirectoryRacking;

    #endregion

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }

    public GunStatsModel CreateStatsModel()
    {
        return new GunStatsModel()
        {
            BaseDamage = BaseDamage,
            BaseMagazineCapacity = BaseMagazineCapacity,
            BaseRecoilStrength = BaseRecoilStrength,
            BaseRoundsPerMinute = BaseRoundsPerMinute
        };
    }
}
    



[System.Serializable]
public class Item
{
    public string Name;
    public Rarities Rarity;
    public int Id = -1;
    public int ammoInMag = 0;

    [Tooltip("Gun buffs have to be in order: Damage, Recoil, Fire Rate, Magazine, Reload Time")]
    public ItemBuff[] buffs;
    public Item()
    {
        Name = "";
        Id = -1;
    }
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.data.Id;
        Rarity = item.Rarity;
        buffs = new ItemBuff[item.data.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max)
            {
                modifier = item.data.buffs[i].modifier
            };
        }
        if(item.itemType == ItemType.Gun)
        {
            ammoInMag = item.GetMagCapacity(this);
        }
    }
}


[System.Serializable]
public class ItemBuff : IModifier
{
    public Modifiers modifier;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }

    public void AddValue(ref int baseValue)
    {
        baseValue += value;
    }

    public void GenerateValue()
    {
        value = Random.Range(min, max);
    }
    public ItemBuff()
    {

    }
}

public class GunStatsModel
{
    public int BaseDamage;
    public int BaseRecoilStrength;
    public int BaseRoundsPerMinute;
    public int BaseMagazineCapacity;
}