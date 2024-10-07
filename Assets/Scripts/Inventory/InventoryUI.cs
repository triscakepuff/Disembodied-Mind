using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public GameObject inventoryPanel;
    public GameObject inventoryPrefab;
    
     void Start()
    {
        UpdateInventoryUI();  // Initialize the UI with current inventory items
    }

    public void UpdateInventoryUI()
    {
        // Clear out the old slots
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Populate the inventory with new slots
        foreach (Item item in inventory.items)
        {
            GameObject slot = Instantiate(inventoryPrefab, inventoryPanel.transform);
            Image icon = slot.GetComponentInChildren<Image>();
            if (icon != null)
            {
                icon.sprite = item.itemIcon;  // Set item icon in the slot
            }
        }
    }
}
