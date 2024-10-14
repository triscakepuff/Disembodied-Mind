using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Interrogation : MonoBehaviour
{
    public GameObject InventoryManager;
    public GameObject interrogationPanel;
    public string Evidence;
    private Item selectedItem;
    public bool Objection;
    // Start is called before the first frame update
    void Start()
    {
        Item selectedItem = InventoryManager.GetComponent<Inventory>().GetSelectedItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void InterrogationFunc()
    {
        var inventory = InventoryManager.GetComponent<Inventory>();
        
        Item selectedItem = inventory.GetSelectedItem();
       // TriggerDialogue(selectedItem.itemName == Evidence ? dialogueTrue : dialogueFalse);

        if (selectedItem.itemName == Evidence)
        {
            gameObject.GetComponent<Button>().enabled = false;
            Objection = true;
        }
    }


}
