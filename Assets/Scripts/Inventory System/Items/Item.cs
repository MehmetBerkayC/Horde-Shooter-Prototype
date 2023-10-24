using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string ItemName;
    public Item (ItemData item)
    {
        ItemName = item.name;
    }
}