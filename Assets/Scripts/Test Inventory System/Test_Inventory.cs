using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Inventory : MonoBehaviour
{
    [SerializeField] List<Test_Item> _items;
    [SerializeField] Transform _itemsParent;
    [SerializeField] Test_ItemSlot[] _itemSlots;

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
        for (int i = 0; i < _items.Count && i < _itemSlots.Length; i++)
        {
            _itemSlots[i].Item = _items[i];
        }

        for (int i = 0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].Item = null;
        }
    }
}
