using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Test_BaseItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI amountText;

    public EventHandler<Test_BaseItemSlot> OnPointerEnterEvent;
    public EventHandler<Test_BaseItemSlot> OnPointerExitEvent;
    public EventHandler<Test_BaseItemSlot> OnRightClickEvent;

    public EventHandler<Test_BaseItemSlot> OnBeginDragEvent;
    public EventHandler<Test_BaseItemSlot> OnDragEvent;
    public EventHandler<Test_BaseItemSlot> OnEndDragEvent;
    public EventHandler<Test_BaseItemSlot> OnDropEvent;

    private Test_ItemSO _item;
    public Test_ItemSO Item
    {
        get { return _item; }

        set
        {
            _item = value;
            if (_item == null)
            {
                image.color = Color.clear;
            }
            else
            {
                image.sprite = _item.Icon;
                image.color = Color.white;
            }
        }
    }

    private int _amount;
    public int Amount
    {
        get
        {
            return _amount;
        }

        set
        {
            _amount = value;
            amountText.enabled = _item != null && _item.MaximumStackSize > 1 && _amount > 1;
            if (amountText.enabled)
            {
                amountText.text = _amount.ToString();
            }
        }
    }


    protected virtual void OnValidate()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
        
        if (amountText == null)
        {
            amountText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public virtual bool CanReceiveItem(Test_ItemSO item)
    {
        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null)
            {
                OnRightClickEvent(this, this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterEvent?.Invoke(this, this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitEvent?.Invoke(this, this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent?.Invoke(this, this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(this, this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent?.Invoke(this, this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropEvent?.Invoke(this, this);
    }
}