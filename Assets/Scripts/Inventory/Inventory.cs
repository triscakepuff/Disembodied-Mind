using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public TMP_Text description;

     // Add an item to the inventory
    public void AddItem(Item newItem)
    {
        items.Add(newItem);
        Debug.Log("Added " + newItem.itemName + " to inventory.");
    }

    // Remove an item from the inventory
    public void RemoveItem(Item itemToRemove)
    {
        if (items.Contains(itemToRemove))
        {
            items.Remove(itemToRemove);
            Debug.Log("Removed " + itemToRemove.itemName + " from inventory.");
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("DETECTED");
        foreach(Item item in items)
        {
            description.text = item.itemDescription;
        }
    }
}
