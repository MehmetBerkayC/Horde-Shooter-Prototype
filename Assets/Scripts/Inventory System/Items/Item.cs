using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public ItemData ItemData;
    public string ItemName;
    public int ID;

    public Item (ItemData item)
    {
        ItemName = item.name;
        ID = item.ID;
        ItemData = item;
    }
}