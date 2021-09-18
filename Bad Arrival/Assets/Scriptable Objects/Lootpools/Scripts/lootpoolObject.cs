using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lootpool", menuName = "Inventory System/Lootpool")]
public class lootpoolObject : ScriptableObject
{
    public lootpoolItem[] lootpool;


    public ItemObject GenerateItem()
    {
        int totalWeight = 0;
        foreach (var item in lootpool)
        {
            totalWeight += item.weight;
        }

        int drawnNumber = UnityEngine.Random.Range(1, totalWeight);

        foreach (var item in lootpool)
        {
            if (drawnNumber <= item.weight)
            {
                return item.item;
            }
            else
            {
                drawnNumber -= item.weight;
            }
        }
        return null;
    }
}

[Serializable]
public class lootpoolItem
{
    public ItemObject item;
    public int weight;
}
