using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemContainer
{
    int ItemCount(string itemID);
    Test_ItemSO RemoveItem(string itemID);
    bool RemoveItem(Test_ItemSO item);
    bool AddItem(Test_ItemSO item);
    bool IsFull();
}
