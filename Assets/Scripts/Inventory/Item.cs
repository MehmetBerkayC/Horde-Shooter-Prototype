using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        HealthPotion,
        ManaPotion,
        YellowGem,
        RedGem,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default: 
            case ItemType.HealthPotion: return ItemAssets.Instance.HealthPotionSprite;
            case ItemType.ManaPotion:   return ItemAssets.Instance.ManaPotionSprite;
            case ItemType.YellowGem:    return ItemAssets.Instance.YellowGemSprite;
            case ItemType.RedGem:       return ItemAssets.Instance.RedGemSprite;
        }
    }
}
