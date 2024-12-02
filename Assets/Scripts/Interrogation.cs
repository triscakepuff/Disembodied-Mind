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

    public Dialogue dialogueDefault;
    public Dialogue dialogueTrue;
    public Dialogue dialogueFalse;
    // Start is called before the first frame update
    void Start()
    {
        Item selectedItem = InventoryManager.GetComponent<Inventory>().GetSelectedItem();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            interrogationPanel.SetActive(false);
        }
    }

   public void InterrogationFunc()
    {
    //     var inventory = InventoryManager.GetComponent<Inventory>();
        
    //     Item selectedItem = inventory.GetSelectedItem();
    //    // TriggerDialogue(selectedItem.itemName == Evidence ? dialogueTrue : dialogueFalse);

    //     if (inventory.IsItemSelected())
    //     {
    //         if(selectedItem.itemName == Evidence)
    //         {
    //             interrogationPanel.SetActive(false);
    //             DialogueManager.Instance.StartDialogue(dialogueTrue);
    //             gameObject.GetComponent<Button>().enabled = false;
    //             Objection = true;
    //         }
    //         else
    //         {
    //             interrogationPanel.SetActive(false);
    //             DialogueManager.Instance.StartDialogue(dialogueFalse);
    //         }
           
    //     }
    //     else 
    //     {
    //         interrogationPanel.SetActive(false);
    //         DialogueManager.Instance.StartDialogue(dialogueDefault);
    //     }
    }


}
