using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coruk.CharacterStats;
using UnityEngine.UI;

public class Test_Character : MonoBehaviour
{
    public CharacterStat Strength;
    public CharacterStat Speed;
    public CharacterStat Health;
    public CharacterStat Damage;

    [SerializeField] Test_Inventory inventory;
    [SerializeField] Test_EquipmentPanel equipmentPanel;
    [SerializeField] Test_StatPanel statPanel;
    [SerializeField] Test_ItemTooltip ItemTooltip;
    [SerializeField] Image image;

    private void Awake()
    {
        // Setup Events
        //Right Click
        inventory.OnRightClickEvent += Equip;
        equipmentPanel.OnRightClickEvent += UnEquip;

        // Pointer Enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;

        // Pointer Exit
        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;

        // Begin Drag
        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;

        // Drag
        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;

        // End Drag
        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;

        // Drop
        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
    }

    private void Start()
    {
        statPanel.SetStats(Strength, Speed, Health, Damage);
        statPanel.UpdateStatValues(); // Will make it not manual
    }

    private void Equip(object sender, Test_ItemSlot itemSlot)
    {
        if (itemSlot.Item is Test_EquippableItem)
        {
            var equippableItem = itemSlot.Item as Test_EquippableItem;
            if (equippableItem != null)
            {
                EquipItem(equippableItem);
            }
        }
    }
    
    private void UnEquip(object sender, Test_ItemSlot itemSlot)
    {
        if (itemSlot.Item is Test_EquippableItem)
        {
            var equippableItem = itemSlot.Item as Test_EquippableItem;
            if (equippableItem != null)
            {
                UnequipItem(equippableItem);
            }
        }
    }
    
    private void ShowTooltip(object sender, Test_ItemSlot itemSlot)
    {
        if (itemSlot.Item is Test_EquippableItem)
        {
            var equippableItem = itemSlot.Item as Test_EquippableItem;
            ItemTooltip.ShowTooltip(equippableItem);
        }
    }
    
    private void HideTooltip(object sender, Test_ItemSlot itemSlot)
    {
        ItemTooltip.HideTooltip();
    }

    private void BeginDrag(object sender, Test_ItemSlot itemSlot)
    {

    }
    private void Drag(object sender, Test_ItemSlot itemSlot)
    {

    }
    private void EndDrag(object sender, Test_ItemSlot itemSlot)
    {

    }
    private void Drop(object sender, Test_ItemSlot itemSlot)
    {

    }

    public void EquipItem(Test_EquippableItem item)
    {
        if (inventory.RemoveItem(item)) // When successfully removed from inventory
        {
            Test_EquippableItem previousItem;
            if (equipmentPanel.AddItem(item, out previousItem)) // Try equipping and check if slot is empty
            {
                if (previousItem != null) // not empty
                {
                    inventory.AddItem(previousItem); // send previous item to inventory
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else // couldn't equip item
            {
                inventory.AddItem(item); // back to inventory
            }
        }
    }

    public void UnequipItem(Test_EquippableItem item)
    {
        if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }
}
