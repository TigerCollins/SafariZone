using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public bool opened = false;
    public Backpack backpack;
    public List<Item> CompleteItemList;
    public List<Item> possibleItemList;
    public Item selectedItem;
    public bool hasBeenTriggered = false;
    private bool hasBeenTriggeredPass2 = false;
    public string dialoguePopUp;
    public GameController gameController;

    [Header("Bubble Variables")]
    public GameObject player;
    public bool canSee;
    public float distance;
    public Animator animatorBubble;
    public Renderer renderer;
    public Material exclamationMark;
    public Material lureSymbol;
    public TrapManager trapManager;

    // Start is called before the first frame update
    void Awake()
    {
        hasBeenTriggered = false;
        hasBeenTriggeredPass2 = false;
        BubbleOpened();
        player = FindObjectOfType<PlayerController>().gameObject;
        gameController = FindObjectOfType<GameController>();
        trapManager = gameController.gameObject.GetComponent<TrapManager>();
    }

    public void BubbleClosed()
    {
       // if(ca)
        animatorBubble.SetBool("Closed", true);
        animatorBubble.SetBool("Triggered", false);

    }

    public void BubbleOpened()
    {
        if (canSee)
        {
            animatorBubble.SetBool("Closed", false);
            animatorBubble.SetBool("Triggered", true);
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        //Check distance
        if (!opened && Vector3.Distance(gameObject.transform.position, player.transform.position) < distance)
        {
            canSee = true;

        }
        
        else
        {
            canSee = false;
            BubbleClosed();
        }
        BubbleOpened();
       // BubbleClosed();
    }

    public void OnChestTrigger()
    {
        if(!opened)
        {
            BubbleClosed();
            if (selectedItem == selectedItem)
            {
                for (int i = 0; i < possibleItemList.Capacity; i++)
                {
                    selectedItem = possibleItemList[Random.Range(0, possibleItemList.Count)];
                }
            }

            if (selectedItem == selectedItem)
            {
                gameController.SetSelectedButton(trapManager.closeButton.gameObject);
                hasBeenTriggered = true;
                if (hasBeenTriggered)
                {
                    AddItem();
                    PopUps();
                    hasBeenTriggered = false;
                }
            }
            
        }
       
    }

    public void PopUps()
    {
        gameController.metaString = dialoguePopUp + selectedItem.itemName + "!";
        gameController.OpenMetaMenu();
    }

    public void AddItem()
    {
        hasBeenTriggered = true;
        opened = true;
        //Checks whole item list
        foreach (Item item in CompleteItemList)
        {
            //Stop Duplicates
            if (hasBeenTriggered)
            {
                //Checks inventory list
                for (int i = 0; i < backpack.Inventory.Capacity; i++)
                {
                    //Adds inventory item based on whole list
                    if(item == backpack.Inventory[i])
                    {
                        Debug.Log("A " + selectedItem + " extra was added.");
                        item.quantity+=1;
                    }

                    //Adds inventory item based on what inventory doesnt have
                    if (!backpack.Inventory.Contains(selectedItem))
                    {
                        backpack.Inventory.Add(selectedItem);
                        Debug.Log("Item Added: " + selectedItem);
                        item.quantity += 1;
                        item.amountOwnedLifetime += 1;
                        gameController.inventoryScript.ResetInventory();
                        gameController.inventoryScript.UpdateInventory();
                    }
                }
                

            }
            hasBeenTriggered = false;
        }
            
    }
}


