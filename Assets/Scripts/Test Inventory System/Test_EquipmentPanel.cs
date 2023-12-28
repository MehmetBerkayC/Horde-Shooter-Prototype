using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_EquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform _equipmentSlotsParent;
    [SerializeField] Test_EquipmentSlot[] _equipmentSlots;

    public event Action<Test_Item> OnItemRightClickedEvent;

    private void Start()
    {
        for (int i = 0; i < _equipmentSlots.Length; i++)
        {
            _equipmentSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }    
    }

    private void OnValidate()
    {
        _equipmentSlots = _equipmentSlotsParent.GetComponentsInChildren<Test_EquipmentSlot>();
    }

    public bool AddItem(Test_EquippableItem item, out Test_EquippableItem previousItem)
    {
        for (int i = 0; i < _equipmentSlots.Length; i++)
        {
            if (_equipmentSlots[i].EquipmentType == item.EquipmentType)
            {
                previousItem = (Test_EquippableItem)_equipmentSlots[i].Item;
                _equipmentSlots[i].Item = item;
                return true;
            }
        }
        previousItem = null;
        return false;
    }
    public bool RemoveItem(Test_EquippableItem item)
    {
        for (int i = 0; i < _equipmentSlots.Length; i++)
        {
            if (_equipmentSlots[i].Item == item)
            {
                _equipmentSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }


}
