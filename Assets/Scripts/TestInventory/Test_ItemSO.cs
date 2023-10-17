using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemName",menuName = "ScriptableObjects/CreateItem")]
public class Test_ItemSO : ScriptableObject
{
    public ItemType ItemType;
    public int ItemAmount;
    public Sprite ItemSprite;
    public bool IsStackable;
}

public enum ItemType
{
    HealthPotion,
    ManaPotion,
    RedGem,
    YellowGem
}
