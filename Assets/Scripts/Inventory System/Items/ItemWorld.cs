using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    [SerializeField] Item _item;
    SpriteRenderer _spriteRenderer;
    TextMeshPro _textAmount;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _textAmount = GetComponentInChildren<TextMeshPro>();
        //if (_textMeshPro != null)
        //{
        //    Debug.Log("Found Component");
        //}
    }

    public void SetItem(Item item)
    {
        this._item = item;
        _spriteRenderer.sprite = item.ItemData.Sprite;
        if (item.ItemData.Amount > 1)
        {
            _textAmount.SetText(item.ItemData.Amount.ToString());
        }
        else
        {
            _textAmount.SetText("");
        }
    }

    public Item GetItem()
    {
        return _item;
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
