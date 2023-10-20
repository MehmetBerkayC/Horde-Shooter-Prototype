using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    public event EventHandler OnItemListChanged;
    List<Item> _itemList;

    public Inventory()
    {
        _itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        if (item.ItemData.IsStackable)
        {
            bool itemAlreadyInInventory = false;

            foreach (Item inventoryItem in _itemList)
            {
                if (inventoryItem == item)
                {
                    inventoryItem.ItemData.Amount += item.ItemData.Amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                _itemList.Add(item);
            }
        }
        else
        {
            _itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }    
    
    public void RemoveItem(Item item)
    {
        if (item.ItemData.IsStackable)
        {
            Item itemInInventory = null;

            foreach (Item inventoryItem in _itemList)
            {
                if (inventoryItem == item)
                {
                    inventoryItem.ItemData.Amount += item.ItemData.Amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.ItemData.Amount <= 0)
            {
                _itemList.Remove(itemInInventory);
            }
        }
        else
        {
            _itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return _itemList;
    }
}
