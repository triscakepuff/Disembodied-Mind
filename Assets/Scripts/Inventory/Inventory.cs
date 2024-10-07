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
  
    private Dictionary<Item, GameObject> itemButtonMap = new Dictionary<Item, GameObject>();  
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
        }
    }
    public void AddItem(Item newItem)
    {
        items.Add(newItem);
        Debug.Log("Added " + newItem.itemName + " to inventory.");

        // Create a new button for the item in the UI
        GameObject newButton = Instantiate(inventoryButtonPrefab, inventoryPanel);
        newButton.GetComponent<InventoryButton>().Setup(newItem);

        itemButtonMap[newItem] = newButton;
    }

    // Remove an item from the inventory
    public void RemoveItem(Item itemToRemove)
    {
        if (items.Contains(itemToRemove))
        {
            items.Remove(itemToRemove);
            Debug.Log("Removed " + itemToRemove.itemName + " from inventory.");
            
            // Find the button associated with the item and destroy it
            if (itemButtonMap.TryGetValue(itemToRemove, out GameObject button))
            {
                Destroy(button); // Destroy the button GameObject
                itemButtonMap.Remove(itemToRemove); // Remove the mapping
            }
        }
    }

      // Method for selecting an item
    public void SelectItem(Item item, GameObject button)
    {
        if (selectedItem == item)
        {
            DeselectItem();  // Deselect the item if it's already selected
        }
        else
        {
            selectedItem = item;
            selectedButton = button;
            selectedButton.GetComponent<Image>().color = Color.black;
            description.text = item.itemDescription;  // Show the description
            Debug.Log(item.itemName);
            StartCoroutine(InstantiateDialogue());
            dialogueAnimator.SetBool("FadeIn", true);
        }
    }

    // Method for deselecting the current item
    public void DeselectItem()
    {
        selectedButton.GetComponent<Image>().color = Color.white;
        selectedItem = null;
        selectedButton = null;
        
        description.text = "";  // Clear the description text
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
}
