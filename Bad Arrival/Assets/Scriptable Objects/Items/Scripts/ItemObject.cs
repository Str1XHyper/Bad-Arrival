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
}

public class ItemObject : ScriptableObject
{
    public ItemType itemType;
    public string Name;
    [TextArea(15, 20)]
    public string Description;

    public Sprite uiDisplay;
    public bool stackable;
    public Item data = new Item();
    public ItemBuff[] buffs;

    #region Gun Data

    [SerializeField] protected int BaseDamage;
    [SerializeField] protected int BaseRecoilStrength;
    [SerializeField] protected int BaseRoundsPerMinute;
    [SerializeField] protected int BaseMagazineCapacity;
    public int GetDamage(Item item)
    {
        return Mathf.RoundToInt(BaseDamage * ((float)item.buffs[0].value / 100f + 1));
    }

    public int GetRecoilStrength(Item item)
    {
        return Mathf.RoundToInt(BaseRecoilStrength * ((float)item.buffs[1].value / 100f + 1));
    }

    public int GetRPM(Item item)
    {
        return Mathf.RoundToInt(BaseRoundsPerMinute * ((float)item.buffs[2].value / 100f + 1));
    }

    public int GetMagCapacity(Item item)
    {
        return Mathf.RoundToInt(BaseMagazineCapacity * ((float)item.buffs[3].value / 100f + 1));
    }


    public FireTypes FireType;
    public GunTypes GunType;
    public GameObject Model;
    public float HeadshotMultiplier;

    #endregion

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }

}
    



[System.Serializable]
public class Item
{
    public string Name;
    public int Id = -1;
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
        buffs = new ItemBuff[item.data.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max)
            {
                modifier = item.data.buffs[i].modifier
            };
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