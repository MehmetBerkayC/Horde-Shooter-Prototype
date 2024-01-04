using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test_ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image _image;

    public EventHandler<Test_ItemSlot> OnPointerEnterEvent;
    public EventHandler<Test_ItemSlot> OnPointerExitEvent;
    public EventHandler<Test_ItemSlot> OnRightClickEvent;
    public EventHandler<Test_ItemSlot> OnBeginDragEvent;
    public EventHandler<Test_ItemSlot> OnDragEvent;
    public EventHandler<Test_ItemSlot> OnEndDragEvent;
    public EventHandler<Test_ItemSlot> OnDropEvent;

    public Test_Item Item
    {
        get { return _item; }

        set
        {
            _item = value;
            if (_item == null)
            {
                _image.color = Color.clear;
            }
            else
            {
                _image.sprite = _item.Icon;
                _image.color = Color.white;
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