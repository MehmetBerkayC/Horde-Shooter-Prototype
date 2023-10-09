using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    Inventory _inventory;
    [SerializeField] Transform _itemSlotContainer;
    [SerializeField] Transform _itemSlotTemplate;

    public void SetInventory(Inventory inventory)
    {
        this._inventory = inventory;
        RefreshInventoryItems();
    }

    void RefreshInventoryItems()
    {
        int x = 0, y = 0;
        float itemSlotCellSize = 110f;
        foreach(Item item in _inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
           
            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }
    }
}
