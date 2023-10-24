using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    [SerializeField] ItemData _item;
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

    private void Start()
    {
        _spriteRenderer.sprite = _item.Sprite;
        if (_item.Amount > 1)
        {
            _textAmount.SetText(_item.Amount.ToString());
        }
        else
        {
            _textAmount.SetText("");
        }
    }

    public ItemData GetItem()
    {
        return _item;
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
