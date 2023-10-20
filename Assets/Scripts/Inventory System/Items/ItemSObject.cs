using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Passive,
    WeaponRanged,
    WeaponMelee
}

public class ItemSObject : ScriptableObject
{
    public Sprite Sprite;
    public GameObject Prefab;
    public ItemType Type;

    public bool IsStackable;
    public int Amount;

    [TextArea(15,20)]
    public string Description; // Use if needed
}
