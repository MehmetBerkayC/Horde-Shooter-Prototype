using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test_ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image _image;
    [SerializeField] Test_ItemTooltip _tooltip;


    public event Action<Test_Item> OnRightClickEvent;
    public Test_Item Item
    {
        get { return _item; }
        
        set
        { 
            _item = value;
            if (_item == null)
            {
                _image.enabled = false;
            }
            else
            {
                _image.sprite = _item.Icon;
                _image.enabled = true;
            }
        }
    }

    private Test_Item _item;

    protected virtual void OnValidate()
    {
        if (_image == null)
        {
            _image = GetComponent<Image>();
        }

        if (_tooltip == null)
        {
            _tooltip = FindObjectOfType<Test_ItemTooltip>(); // Call findobjectof... only in Onvalidate() -> works only on editor not builds
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (Item != null && OnRightClickEvent != null)
            {
                OnRightClickEvent(Item);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Item is Test_EquippableItem)
        {
            _tooltip.ShowTooltip((Test_EquippableItem)Item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltip.HideTooltip();
    }
}
