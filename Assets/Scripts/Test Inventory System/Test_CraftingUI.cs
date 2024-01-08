using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_CraftingUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] RectTransform arrowParent;
    [SerializeField] Test_BaseItemSlot[] ItemSlots;

    [Header("Public Variables")]
    public ItemContainer ItemContainer;

    private Test_CraftingRecipeSO _craftingRecipe;
    public Test_CraftingRecipeSO CraftingRecipe
    {
        get { return _craftingRecipe; }
        //set { SetCraftingRecipe(value); }
    }

    public EventHandler<Test_BaseItemSlot> OnPointerEnterEvent;
    public EventHandler<Test_BaseItemSlot> OnPointerExitEvent;

    private void OnValidate()
    {
        ItemSlots = GetComponentsInChildren<Test_BaseItemSlot>(includeInactive: true);
    }

    private void Start()
    {
        foreach (Test_BaseItemSlot itemSlot in ItemSlots)
        {
            itemSlot.OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlot.OnPointerExitEvent += OnPointerExitEvent;
        }
    }
}
