using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Item> items = new List<Item>();
    public TMP_Text description;
    public Transform inventoryPanel;
    public GameObject inventoryButtonPrefab;
    public int dialogueTime;
    public Animator dialogueAnimator;

    private Item selectedItem = null;
    private GameObject selectedButton = null;
    private Dictionary<Item, GameObject> itemButtonMap = new Dictionary<Item, GameObject>(); // Map item to UI buttons

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Add an item to the inventory
    public void AddItem(Item newItem)
    {
        // Check if the item already exists in the inventory
        Item existingItem = items.Find(item => item.itemName == newItem.itemName);

        if (existingItem != null)
        {
            // Increase the count of the existing item
            existingItem.count += 1;

            // Update the button text to reflect the new count
            if (itemButtonMap.TryGetValue(existingItem, out GameObject button))
            {
                UpdateButtonText(button, existingItem);
            }
        }
        else
        {
            // Add a new item to the inventory
            items.Add(newItem);

            // Create a new button for the item in the UI
            GameObject newButton = Instantiate(inventoryButtonPrefab, inventoryPanel);
            Button itemButton = newButton.GetComponent<Button>();

            itemButton.onClick.AddListener(() => SelectItem(newItem));
            itemButtonMap[newItem] = newButton;
            itemButton.GetComponent<Image>().sprite = newItem.itemIcon;

            // Add the count text to the button
            UpdateButtonText(newButton, newItem);
        }
        Debug.Log("Added " + newItem.itemName + " to inventory.");
    }

    // Remove a specific number of items from the inventory
    public void RemoveItem(Item itemToRemove, int amount)
    {
        Item existingItem = items.Find(item => item.itemName == itemToRemove.itemName);
        if (existingItem != null)
        {
            existingItem.count -= amount;

            if (existingItem.count <= 0)
            {
                // Remove the item completely if the count reaches 0
                items.Remove(existingItem);

                if (itemButtonMap.TryGetValue(existingItem, out GameObject button))
                {
                    Destroy(button); // Destroy the button GameObject
                    itemButtonMap.Remove(existingItem); // Remove the mapping
                }
            }
            else
            {
                // Update the button text to reflect the new count
                if (itemButtonMap.TryGetValue(existingItem, out GameObject button))
                {
                    UpdateButtonText(button, existingItem);
                }
            }
            Debug.Log("Removed " + amount + " of " + itemToRemove.itemName + " from inventory.");
        }
    }

    // Update the button text to show the item's count
    private void UpdateButtonText(GameObject button, Item item)
    {
        TMP_Text countText = button.transform.Find("CountText").GetComponent<TMP_Text>();
        countText.text = item.count > 1 ? item.count.ToString() : ""; // Show count if greater than 1
    }

    // Method for selecting an item
    public void SelectItem(Item item)
    {
        if (selectedItem == item)
        {
            DeselectItem(); // Deselect the item if it's already selected
        }
        else
        {
            selectedItem = item;
            description.text = item.itemDescription; // Show the description
            Debug.Log(item.itemName);
            StartCoroutine(InstantiateDialogue());
            dialogueAnimator.SetBool("FadeIn", true);
        }
    }

    // Method for deselecting the current item
    public void DeselectItem()
    {
        selectedItem = null;
        selectedButton = null;
        description.text = ""; // Clear the description text
    }

    // Method to check if an item is selected
    public bool IsItemSelected()
    {
        return selectedItem != null;
    }

    // Method to get the currently selected item
    public Item GetSelectedItem()
    {
        return selectedItem;
    }

    public IEnumerator InstantiateDialogue()
    {
        yield return new WaitForSeconds(dialogueTime);
        dialogueAnimator.SetBool("FadeIn", false);
    }

    public bool HasItem(string itemName)
    {
        // Look for an item with the specified name in the inventory
        Item existingItem = items.Find(item => item.itemName == itemName);
        return existingItem != null; // Return true if the item exists, false otherwise
    }

    // Update inventory UI (placeholder, use if needed)
    public void UpdateInventoryUI()
    {
        foreach (var item in items)
        {
            if (itemButtonMap.TryGetValue(item, out GameObject button))
            {
                UpdateButtonText(button, item);
            }
        }
    }
}
