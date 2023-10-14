using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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

    /// How Switch Case Works
    /// Switch case, returns value/code that it first sees until an executable code appears
    /// In this case Both yellow gems and potions will be stackable
    /// Alongside, the default behavior will make item stackable
    
    /// CodeMonkey:
    /// default works just like any other case, it "falls down" to the next case
    /// until it finds a break; You don't need to add a break for every single case,
    /// you can have multiple cases (including default) doing the same logic 
    /// and hitting the same break point.
    /// You can put it at the end as long as you put a break after it
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion:
            case ItemType.ManaPotion:
            case ItemType.YellowGem:
                return true;
            case ItemType.RedGem:
                return false;
        }
    }
}
