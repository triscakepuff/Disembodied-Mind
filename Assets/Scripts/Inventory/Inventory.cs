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
    private Item firstSelectedItem = null;
    private Item secondSelectedItem = null;
    private Dictionary<Item, GameObject> itemButtonMap = new Dictionary<Item, GameObject>(); // Map item to UI buttons

    private Dictionary<(string, string), Item> combinationResults = new Dictionary<(string, string), Item>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitializeCombinationResults();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeCombinationResults()
    {
        // Initialize your combination results here
        combinationResults.Add(("Log", "Axe"), GameManager.Instance.Wood); // Reference Wood from GameManager
        combinationResults.Add(("Axe", "Log"), GameManager.Instance.Wood);
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
         // If the selected item is the same as the currently selected item, deselect it
        if (selectedItem == item)
        {
            DeselectItem();
            return;
        }

        // If no combination is in progress, select the item regularly
        if (firstSelectedItem == null)
        {
            selectedItem = item;
            description.text = item.itemDescription; // Show the description
            Debug.Log("Selected item: " + item.itemName);
            StartCoroutine(InstantiateDialogue());
            dialogueAnimator.SetBool("FadeIn", true);
            firstSelectedItem = item; // Set as the first item for potential combination
            return;
        }

         // If one item is already selected for combination
        if (firstSelectedItem != null && secondSelectedItem == null)
        {
            // Check if the user intends to combine items
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) // Example key for combination intent
            {
                secondSelectedItem = item;

                // Attempt to combine items
                if (TryCombineItems(firstSelectedItem, secondSelectedItem))
                {
                    Debug.Log("Items combined successfully.");
                }
                else
                {
                    Debug.Log("Items cannot be combined.");
                }

                // Reset combination state
                firstSelectedItem = null;
                secondSelectedItem = null;
                DeselectCombinationItems();
            }
            else
            {
                // If no combination intent, reset and select the new item
                selectedItem = item;
                description.text = item.itemDescription;
                Debug.Log("Selected new item: " + item.itemName);
                StartCoroutine(InstantiateDialogue());
                dialogueAnimator.SetBool("FadeIn", true);
                firstSelectedItem = item; // Set the new first item
            }

            return;
        }

        
        // If combination is not intended, reset and select the new item
        firstSelectedItem = null;
        secondSelectedItem = null;
        selectedItem = item;
        description.text = item.itemDescription;
        Debug.Log("Reset and selected new item: " + item.itemName);
        StartCoroutine(InstantiateDialogue());
        dialogueAnimator.SetBool("FadeIn", true);
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

    public bool TryCombineItems(Item item1, Item item2)
    {
        if (combinationResults.TryGetValue((item1.itemName, item2.itemName), out Item combinedItem))
        {
            RemoveItem(item1, 1);
            RemoveItem(item2, 1);
            if (combinedItem != null)
            {
                AddItem(combinedItem);  // Add the combined item (scriptable object)
                return true;
            }
            return true;
        }
        return false;
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

    public void DeselectCombinationItems()
    {
        if (firstSelectedItem != null)
        {
            firstSelectedItem = null;
        }
        if (secondSelectedItem != null)
        {
           secondSelectedItem = null;
        }
    }
}
