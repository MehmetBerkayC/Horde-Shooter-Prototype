using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Inventory : MonoBehaviour
{
    [SerializeField] List<Test_Item> _startingItems;
    [SerializeField] Transform _itemsParent;
    [SerializeField] Test_ItemSlot[] _itemSlots;

    public EventHandler<Test_ItemSlot> OnPointerEnterEvent;
    public EventHandler<Test_ItemSlot> OnPointerExitEvent;
    public EventHandler<Test_ItemSlot> OnRightClickEvent;
    public EventHandler<Test_ItemSlot> OnBeginDragEvent;
    public EventHandler<Test_ItemSlot> OnDragEvent;
    public EventHandler<Test_ItemSlot> OnEndDragEvent;
    public EventHandler<Test_ItemSlot> OnDropEvent;

    private void Start()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].OnPointerEnterEvent   += OnPointerEnterEvent;
            _itemSlots[i].OnPointerExitEvent    += OnPointerExitEvent;
            _itemSlots[i].OnRightClickEvent     += OnRightClickEvent;
            _itemSlots[i].OnBeginDragEvent      += OnBeginDragEvent;
            _itemSlots[i].OnDragEvent           += OnDragEvent;
            _itemSlots[i].OnEndDragEvent        += OnEndDragEvent;
            _itemSlots[i].OnDropEvent           += OnDropEvent;
        }
    }

    private void OnValidate()
    {
        if (_itemsParent != null)
        {
            _itemSlots = _itemsParent.GetComponentsInChildren<Test_ItemSlot>();
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
            _itemSlots[i].Item = _startingItems[i];
        }

        // When there isn't an item this loop works
        for (; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].Item = null;
        }
    }

    public bool AddItem(Test_Item item) // Check all slots, if empty put item there 
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].Item == null)
            {
                _itemSlots[i].Item = item;
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(Test_Item item) // Check all slots, if item exists remove it
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].Item == item)
            {
                _itemSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }

    public bool IsFull()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].Item == null)
            {
                return false;
            }
        }
        return true;
    }
}
