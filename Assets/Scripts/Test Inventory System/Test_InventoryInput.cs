using UnityEngine;

public class Test_InventoryInput : MonoBehaviour
{
    [SerializeField] private GameObject characterPanelGameObject; // includes both equipment and inventory
    [SerializeField] private GameObject equipmentPanelGameObject;
    [SerializeField] private GameObject inventoryPanelGameObject;
    [SerializeField] private KeyCode[] toggleEquipmentPanelKeys;
    [SerializeField] private KeyCode[] toggleInventoryKeys;

    // Update is called once per frame
    private void Update()
    {
        for (int i = 0; i < toggleEquipmentPanelKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleEquipmentPanelKeys[i]))
            {
                if (!characterPanelGameObject.activeSelf)
                {   // Nothing is open
                    characterPanelGameObject.SetActive(true);
                    equipmentPanelGameObject.SetActive(true);

                    ShowMouseCursor();
                }
                else if (!equipmentPanelGameObject.activeSelf && inventoryPanelGameObject.activeSelf)
                {   // Inventory is open
                    equipmentPanelGameObject.SetActive(true);
                }
                else if (equipmentPanelGameObject.activeSelf && !inventoryPanelGameObject.activeSelf)
                {   // Equipment is open
                    characterPanelGameObject.SetActive(false);
                    equipmentPanelGameObject.SetActive(false);
                    
                    HideMouseCursor();
                }
                else
                {   // Everything is open
                    equipmentPanelGameObject.SetActive(false);
                }

                break;
            }
        }

        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                if (!characterPanelGameObject.activeSelf)
                {   // Nothing is open
                    characterPanelGameObject.SetActive(true);
                    inventoryPanelGameObject.SetActive(true);

                    ShowMouseCursor();
                }
                else if (!equipmentPanelGameObject.activeSelf && inventoryPanelGameObject.activeSelf)
                {   // Only Inventory is open
                    characterPanelGameObject.SetActive(false);
                    inventoryPanelGameObject.SetActive(false);
                }
                else if (equipmentPanelGameObject.activeSelf && !inventoryPanelGameObject.activeSelf)
                {   // Only Equipment is open
                    inventoryPanelGameObject.SetActive(true);
                }
                else
                {   // Everything is open
                    characterPanelGameObject.SetActive(false);
                    inventoryPanelGameObject.SetActive(false);
                    equipmentPanelGameObject.SetActive(false);

                    HideMouseCursor();
                }
                break;
            }
        }
    }

    public void ToggleEquipmentPanel() // For the "Equipment" button
    {
        equipmentPanelGameObject.SetActive(!equipmentPanelGameObject.activeSelf);
    }

    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}