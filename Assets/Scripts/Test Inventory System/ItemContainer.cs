using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainer : MonoBehaviour, IItemContainer
{
    [SerializeField] protected Test_BaseItemSlot[] _itemSlots;

    public virtual bool AddItem(Test_ItemSO item) // Check all slots, if empty put item there
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].Item == null || (_itemSlots[i].Item.ID == item.ID && _itemSlots[i].Amount < item.MaximumStackSize))
            {
                _itemSlots[i].Item = item;
                _itemSlots[i].Amount++;
                return true;
            }
        }
        return false;
    }

    public virtual Test_ItemSO RemoveItem(string itemID)
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            Test_ItemSO item = _itemSlots[i].Item;

            if (item != null && item.ID == itemID)
            {
                _itemSlots[i].Amount--;
                if (_itemSlots[i].Amount == 0)
                {
                    _itemSlots[i].Item = null;
                }
                return item;
            }
        }
        return null;
    }

    public virtual bool RemoveItem(Test_ItemSO item) // Check all slots, if item exists remove it
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].Item == item)
            {
                _itemSlots[i].Amount--;
                if (_itemSlots[i].Amount == 0)
                {
                    _itemSlots[i].Item = null;
                }
                return true;
            }
        }
        return false;
    }

    public virtual bool IsFull()
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

    public virtual int ItemCount(string itemID)
    {
        int number = 0;
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].Item.ID == itemID)
            {
                number++;
            }
        }
        return number;
    }
}
