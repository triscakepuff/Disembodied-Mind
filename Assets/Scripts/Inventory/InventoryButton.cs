using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryButton : MonoBehaviour
{
    public Item item;  // The item this button represents
    private Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;
    }

    // Set up the button with the item information
    public void Setup(Item newItem)
    {
        GetComponent<Image>().sprite = newItem.itemIcon;
    }

    public void DestroyButton()
    {
        Destroy(gameObject);
    }
}
