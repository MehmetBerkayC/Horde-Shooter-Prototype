using System;
using System.Collections.Generic;
using UnityEngine;

public class Test_Inventory : ItemContainer
{
    [SerializeField] private List<Test_ItemSO> _startingItems;
    [SerializeField] private Transform _itemsParent;

    public EventHandler<Test_BaseItemSlot> OnPointerEnterEvent;
    public EventHandler<Test_BaseItemSlot> OnPointerExitEvent;
    public EventHandler<Test_BaseItemSlot> OnRightClickEvent;
    public EventHandler<Test_BaseItemSlot> OnBeginDragEvent;
    public EventHandler<Test_BaseItemSlot> OnDragEvent;
    public EventHandler<Test_BaseItemSlot> OnEndDragEvent;
    public EventHandler<Test_BaseItemSlot> OnDropEvent;

    private void Start()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            _itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            _itemSlots[i].OnRightClickEvent += OnRightClickEvent;
            _itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            _itemSlots[i].OnDragEvent += OnDragEvent;
            _itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            _itemSlots[i].OnDropEvent += OnDropEvent;
        }
    }

    private void OnValidate()
    {
        if (_itemsParent != null)
        {
            _itemSlots = _itemsParent.GetComponentsInChildren<Test_BaseItemSlot>();
        }

        SetStartingItems();
    }

    private void SetStartingItems()
    {
        // Uses same variable for both loops
        int i = 0;

        // When there is an item this loop works
        for (; i < _startingItems.Count && i < _itemSlots.Length; i++)
        {
            _itemSlots[i].Item = _startingItems[i].GetCopy();
            _itemSlots[i].Amount = 1;
        }

        // When there isn't an item this loop works
        for (; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].Item = null;
            _itemSlots[i].Amount = 0;
        }
    }

}