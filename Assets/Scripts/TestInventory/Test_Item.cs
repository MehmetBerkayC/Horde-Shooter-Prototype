using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(BoxCollider2D))]
public class Test_Item : MonoBehaviour
{
    [SerializeField] private Test_ItemSO _itemSO;

    SpriteRenderer _spriteRenderer;
    BoxCollider2D _boxCollider2D;

    ItemType _itemType;
    int _itemAmount;
    bool _isStackable;
    
 
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        CreateItem();
    }

    private void CreateItem()
    {
        _itemType = _itemSO.ItemType;
        _itemAmount = _itemSO.ItemAmount;
        _isStackable = _itemSO.IsStackable;
        _spriteRenderer.sprite = _itemSO.ItemSprite;

        _boxCollider2D.isTrigger = true;
    }

}
