using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_Inventory : MonoBehaviour
{
    Inventory _inventory;
    [SerializeField] Transform _itemSlotContainer;
    [SerializeField] Transform _itemSlotTemplate;

    public void SetInventory(Inventory inventory)
    {
        this._inventory = inventory;
        _inventory.OnItemListChanged += Inventory_OnItemListChanged; ;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    void RefreshInventoryItems()
    {
        foreach (Transform child in _itemSlotContainer) // if child has a template don't destroy, will make a new one
        {
            if (child == _itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0, y = 0;
        float itemSlotCellSize = 80f;
        foreach(Item item in _inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.ItemData.Sprite;

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("TextAmount").GetComponent<TextMeshProUGUI>();
            if (item.ItemData.Amount > 1)
            {
                uiText.SetText(item.ItemData.Amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }

            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }
    }
}
