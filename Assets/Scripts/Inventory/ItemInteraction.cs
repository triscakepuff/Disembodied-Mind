using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;  // Reference to the item ScriptableObject

    private void OnMouseDown()  // This assumes 2D point-and-click, where the player clicks on the item
    {
        Debug.Log("yes");
        Inventory playerInventory = FindObjectOfType<Inventory>();
        if (playerInventory != null)
        {
            playerInventory.AddItem(item);
            Destroy(gameObject);  // Remove the item from the scene after picking up
        }

        InventoryUI inventoryUI = FindObjectOfType<InventoryUI>();
        if (inventoryUI != null)
        {
            inventoryUI.UpdateInventoryUI();  // Update the UI to show the new item
        }
    }
}