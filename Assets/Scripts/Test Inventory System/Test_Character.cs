using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coruk.CharacterStats;

public class Test_Character : MonoBehaviour
{
    CharacterStats Strength;
    CharacterStats Speed;
    CharacterStats Agility;
    CharacterStats Vitality;

    [SerializeField] Test_Inventory _inventory;
    [SerializeField] Test_EquipmentPanel _equipmentPanel;

    private void Awake()
    {
        _inventory.OnItemRightClickedEvent += EquipFromInventory;
        _equipmentPanel.OnItemRightClickedEvent += UnequipFromEquipPanel;
    }

    private void EquipFromInventory(Test_Item item)
    {
        if (item is Test_EquippableItem)
        {
            Equip((Test_EquippableItem)item);
        }
    }

    private void UnequipFromEquipPanel(Test_Item item)
    {
        if (item is Test_EquippableItem)
        {
            Unequip((Test_EquippableItem)item);
        }
    }

    public void Equip(Test_EquippableItem item)
    {
        if (_inventory.RemoveItem(item)) // When successfully removed from inventory
        {
            Test_EquippableItem previousItem;
            if (_equipmentPanel.AddItem(item, out previousItem)) // Try equipping and check if slot is empty
            {
                if (previousItem != null) // not empty
                {
                    _inventory.AddItem(previousItem); // send previous item to inventory
                }
            }
            else // couldn't equip item
            {
                _inventory.AddItem(item); // back to inventory
            }
        }
    }

    public void Unequip(Test_EquippableItem item)
    {
        if (!_inventory.IsFull() && _equipmentPanel.RemoveItem(item))
        {
            _inventory.AddItem(item);
        }
    }
}
