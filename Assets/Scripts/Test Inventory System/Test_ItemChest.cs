using UnityEngine;

public class Test_ItemChest : MonoBehaviour
{
    [SerializeField] private Test_Item item;
    private Test_Inventory inventory;

    [SerializeField] private KeyCode itemPickupKeycode = KeyCode.E;

    private bool _isInRange;

    private void Update()
    {
        if (_isInRange && Input.GetKeyDown(itemPickupKeycode))
        {
            inventory.AddItem(Instantiate(item));
            item = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Test_PlayerInventory inventoryScript))
        {
            _isInRange = true;
            inventory = inventoryScript.PlayerInventory;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isInRange = false;
        inventory = null;
    }
}