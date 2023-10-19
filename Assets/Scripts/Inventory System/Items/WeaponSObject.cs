using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponItemName", menuName = "Inventory System/Items/Weapons")]
public class WeaponSObject : ItemSObject
{
    // Don't need to use one of these
    public int Damage = 10;
    public float Range = 0; 
    public int BulletsPerMinute = 200;

    private void Awake()
    {
        Type = ItemType.WeaponRanged;
    }
}
