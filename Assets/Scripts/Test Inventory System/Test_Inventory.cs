using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Inventory : MonoBehaviour
{
    [SerializeField] List<Test_Item> _items;
    [SerializeField] Transform _itemsParent;
    [SerializeField] Test_ItemSlot[] _itemSlots;

    public event Action<Test_Item> OnItemRightClickedEvent;

    private void Awake()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }
    }

    private void OnValidate()
    {
        if (_itemsParent != null)
        {
            _itemSlots = _itemsParent.GetComponentsInChildren<Test_ItemSlot>();
        }

        RefreshUI();
    }

    private void RefreshUI()
    {
        // Uses same variable for both loops
        int i = 0;
        
        // When there is an item this loop works
        for (; i < _items.Count && i < _itemSlots.Length; i++)
        {
            _itemSlots[i].Item = _items[i];
        }

        // When there isn't an item this loop works
        for (; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].Item = null;
        }
    }

    public bool AddItem(Test_Item item)
    {
        if (IsFull())
        {
            return false;
        }
        _items.Add(item);
        RefreshUI();
        return true;
    }

    public bool RemoveItem(Test_Item item)
    {
        if (_items.Remove(item))
        {
            RefreshUI();
            return true;
        }
        return false;
    }

    public bool IsFull()
    {
        return _items.Count >= _itemSlots.Length;
    }
}
