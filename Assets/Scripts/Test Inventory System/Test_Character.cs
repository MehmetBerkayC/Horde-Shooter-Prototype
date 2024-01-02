using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coruk.CharacterStats;

public class Test_Character : MonoBehaviour
{
    public CharacterStat Strength;
    public CharacterStat Speed;
    public CharacterStat Health;
    public CharacterStat Damage;

    [SerializeField] Test_Inventory _inventory;
    [SerializeField] Test_EquipmentPanel _equipmentPanel;
    [SerializeField] Test_StatPanel _statPanel;

    private void Awake()
    {
        _inventory.OnItemRightClickedEvent += EquipFromInventory;
        _equipmentPanel.OnItemRightClickedEvent += UnequipFromEquipPanel;
    }

    private void Start()
    {
        _statPanel.SetStats(Strength, Speed, Health, Damage);
        _statPanel.UpdateStatValues(); // Will make it not manual
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
                    previousItem.Unequip(this);
                    _statPanel.UpdateStatValues();
                }
                item.Equip(this);
                _statPanel.UpdateStatValues();
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
            item.Unequip(this);
            _statPanel.UpdateStatValues();
            _inventory.AddItem(item);
        }
    }
}
