using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public ItemSObject Item;

    SpriteRenderer _spriteRenderer;
    TextMeshPro _textAmount;

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.sprite = Item.Sprite;

        _textAmount = GetComponentInChildren<TextMeshPro>();
        if (Item.Amount > 1)
        {
            _textAmount.SetText(Item.Amount.ToString());
        }
        else
        {
            _textAmount.SetText("");
        }
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
