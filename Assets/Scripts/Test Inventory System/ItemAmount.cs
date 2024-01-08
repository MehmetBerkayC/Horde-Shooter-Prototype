using System;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
    public Test_ItemSO Item;

    [Range(1, 99)]
    public int Amount;
}