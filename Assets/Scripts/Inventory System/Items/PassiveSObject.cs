using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemName", menuName ="Inventory System/Items/Passive")]
public class PassiveSObject : ItemSObject
{
    // Add Stats for the item later
    public int AdditionalHealth;

    public void Awake()
    {
        Type = ItemType.Passive;
    }
}
// The ItemObject is there to easily make new items without setting its type
// For example every time we want a Passive Item, we don't need to change the parameter
// It will be created as type:Passive