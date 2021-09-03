using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Gun,
}

public class ItemObject : ScriptableObject
{
    public ItemType itemType;
    public string Name;
    [TextArea(15,20)]
    public string Description;
}
