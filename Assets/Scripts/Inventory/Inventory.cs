using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    List<Item> _itemList;

    public Inventory()
    {
        _itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.RedGem, amount = 1 });
        Debug.Log(_itemList.Count);
    }

    public void AddItem(Item item)
    {
        _itemList.Add(item);
    }

    public List<Item> GetItemList()
    {
        return _itemList;
    }
}
