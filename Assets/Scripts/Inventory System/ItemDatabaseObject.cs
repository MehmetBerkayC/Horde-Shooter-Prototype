using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemData[] Items;
    public Dictionary<ItemData, int> GetID = new Dictionary<ItemData, int>();
    public Dictionary<int, ItemData> GetItem = new Dictionary<int, ItemData>();

    public void OnAfterDeserialize()
    {
        GetID = new Dictionary<ItemData, int>();
        GetItem = new Dictionary<int, ItemData>();

        for (int i = 0; i < Items.Length; i++)
        {
            GetID.Add(Items[i], i);
            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
    }
}
