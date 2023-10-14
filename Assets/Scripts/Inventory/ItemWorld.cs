using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.PrefabItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }

    Item _item;
    SpriteRenderer _spriteRenderer;
    TextMeshPro _textMeshPro;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _textMeshPro = GetComponentInChildren<TextMeshPro>();
        //if (_textMeshPro != null)
        //{
        //    Debug.Log("Found Component");
        //}
    }

    public void SetItem(Item item)
    {
        this._item = item;
        _spriteRenderer.sprite = item.GetSprite();
        if (item.amount > 1)
        {
            _textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            _textMeshPro.SetText("");
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
