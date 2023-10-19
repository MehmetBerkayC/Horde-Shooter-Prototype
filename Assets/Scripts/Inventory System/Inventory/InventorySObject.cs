using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="InventoryName", menuName ="Inventory System/Inventory")]
public class InventorySObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();

    public void AddItem(ItemSObject item, int amount)
    {
        bool hasItem = false;
        foreach (InventorySlot slot in Container)
        {
            if (slot.Item == item)
            {
                slot.AddAmount(amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Container.Add(new InventorySlot(item, amount));
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemSObject Item;
    public int Amount;

    public InventorySlot(ItemSObject item, int amount)
    {
        Item = item;
        Amount = amount;
    }

    public void AddAmount(int amount)
    {
        Amount += amount;
    }
}