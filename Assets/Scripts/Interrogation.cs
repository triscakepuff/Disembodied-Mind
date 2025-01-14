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

    public Dialogue dialogueTrue;
    public Dialogue dialogueFalse;

    public bool solved = false;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(solved);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            interrogationPanel.SetActive(false);
        }

    }

   public void InterrogationFunc()
    {
        Item selectedItem = InventoryManager.GetComponent<Inventory>().GetSelectedItem();

        if(selectedItem.itemName == Evidence)
        {
            InventoryManager.GetComponent<Inventory>().RemoveItem(selectedItem, 1);
            GameManager.Instance.DeactivateUI();
            FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueTrue);
            button.interactable = false;
            solved = true;
        }else
        {
            GameManager.Instance.DeactivateUI();
            FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueFalse);
        }
    }


}
