using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="InventoryName", menuName ="Inventory System/Inventory")]
public class InventorySObject : ScriptableObject
{
    public event EventHandler OnItemListChanged;

    public List<InventorySlot> Container = new List<InventorySlot>();

    public void AddItem(ItemData item, int amount)
    {
        if (item.IsStackable)
        {
            bool itemAlreadyInInventory = false;
            foreach (InventorySlot slot in Container)
            {
                if (slot.Item == item)
                {
                    slot.AddAmount(amount);
                    itemAlreadyInInventory = true;
                    break;
                }
            }
            if (!itemAlreadyInInventory)
            {
                Container.Add(new InventorySlot(item, amount));
            }
        }
        else
        {
            Container.Add(new InventorySlot(item, amount));
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<InventorySlot> GetItemList()
    {
        return Container;
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemData Item;
    public int Amount;

    public InventorySlot(ItemData item, int amount)
    {
        Item = item;
        Amount = amount;
    }

    public void AddAmount(int amount)
    {
        Amount += amount;
    }
}