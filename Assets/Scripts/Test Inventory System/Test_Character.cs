using Coruk.CharacterStats;
using UnityEngine;
using UnityEngine.UI;

public class Test_Character : MonoBehaviour
{
    // Make this script decoupled from movement and stat segments
    public CharacterStat Strength;
    public CharacterStat Speed;
    public CharacterStat Health;
    public CharacterStat Damage;

    [SerializeField] private Test_Inventory inventory;
    [SerializeField] private Test_EquipmentPanel equipmentPanel;
    [SerializeField] private Test_StatPanel statPanel;
    [SerializeField] private Test_ItemTooltip ItemTooltip;
    [SerializeField] private Image draggableItem;

    private Test_ItemSlot draggedItemSlot;

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
        if (itemSlot.Item != null)
        {
            draggedItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
            draggableItem.raycastTarget = false; // Raycast cannot know whats beneath if enabled while dropping
        }
    }

    private void Drag(object sender, Test_ItemSlot itemSlot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    private void EndDrag(object sender, Test_ItemSlot itemSlot)
    {
        draggedItemSlot = null;
        draggableItem.enabled = false;
        draggableItem.raycastTarget = true;
    }

    private void Drop(object sender, Test_ItemSlot droppeditemSlot)
    {
        // Swap possible
        if (droppeditemSlot.CanReceiveItem(draggedItemSlot.Item) && draggedItemSlot.CanReceiveItem(droppeditemSlot.Item))
        {
            // Swapping Equippable items
            Test_EquippableItem dragItem = draggedItemSlot.Item as Test_EquippableItem;
            Test_EquippableItem dropItem = droppeditemSlot.Item as Test_EquippableItem;

            if (draggedItemSlot is Test_EquipmentSlot)
            {
                if (dragItem != null) dragItem.Unequip(this);
                if (dropItem != null) dropItem.Equip(this);
            }

            if (droppeditemSlot is Test_EquipmentSlot)
            {
                if (dragItem != null) dragItem.Equip(this);
                if (dropItem != null) dropItem.Unequip(this);
            }

            // Items aren't equippable ones, swap normally
            Test_Item draggedItem = draggedItemSlot.Item;
            draggedItemSlot.Item = droppeditemSlot.Item;
            droppeditemSlot.Item = draggedItem;

            statPanel.UpdateStatValues();
        }
        else if (droppeditemSlot.CanReceiveItem(draggedItemSlot.Item) && droppeditemSlot.Item == null)
        {
            droppeditemSlot.Item = draggedItemSlot.Item;

            statPanel.UpdateStatValues();
        }
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