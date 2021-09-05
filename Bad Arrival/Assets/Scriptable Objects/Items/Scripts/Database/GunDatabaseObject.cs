using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Gun Database")]
public class GunDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public GunObject[] GunObjects;

    //[ContextMenu("Update ID's")]
    //public void UpdateID()
    //{
    //    for (int i = 0; i < GunObjects.Length; i++)
    //    {
    //        if (GunObjects[i].data.Id != i)
    //            GunObjects[i].data.Id = i;
    //    }
    //}
    public void OnAfterDeserialize()
    {
        //UpdateID();
    }

    public void OnBeforeSerialize()
    {

    }
}
