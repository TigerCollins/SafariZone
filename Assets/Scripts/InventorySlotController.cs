using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Aura2API;

public class InventorySlotController : MonoBehaviour
{
    public GameController gameController;
    public Inventory inventory;
    public Item item;
    public Lure trap;
    public Toggle toggle;

    public Text quantityText;
    public Text flavourText;
    public Text itemNameExtra;
    public Image itemDisplay;
    public ToggleGroup toggleGroup;

    public Text itemQuanityText;

    public void Awake()
    {
        if(SceneManager.GetActiveScene().name == "GameWorld")
        {
            gameController = GameObject.Find("_ScriptController").GetComponent<GameController>();

        }
       
       
        if (SceneManager.GetActiveScene().name != "GameWorld")
        {
            inventory = GameObject.Find("ScriptController").GetComponent<Inventory>();
        }

        else
        {
            inventory = GameObject.Find("_ScriptController").GetComponent<GameController>().inventoryScript;
        }
      //  UpdateInfo();


        quantityText = GameObject.Find("num").GetComponent<Text>();
            flavourText = GameObject.Find("Item Disc.").GetComponent<Text>();
            itemNameExtra = GameObject.Find("Item Display Name").GetComponent<Text>();
            itemDisplay = GameObject.Find("ItemDisplay").GetComponent<Image>();
            toggleGroup = gameObject.transform.parent.GetComponent<ToggleGroup>();
            gameObject.transform.GetComponentInChildren<Toggle>().group = toggleGroup;
       
      
       
      /* if(item != null)
        {
            if (item.itemTypes == Item.ItemTypes.Lure)
            {
                for (int i = 0; i < inventory.trap.Count; i++)
                {
                    if (item.name == inventory.trap[i].name)
                    {
                        trap = inventory.trap[i];
                    }
                }
                //gameObject.AddComponent<SelectTrap>();
            }
        }
        */

        //REMOVES SELECT BOX
        if (item.itemTypes != Item.ItemTypes.Lure)
        {
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
        }
    }

    public void SelectToggle()
    {
        if (item.itemTypes == Item.ItemTypes.Lure)
        {
            toggle.isOn = true;
        }
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameWorld" && item!=null)
        {
            itemQuanityText.text = item.quantity.ToString();
            if (gameObject.transform.parent.gameObject.GetComponent<ToggleGroup>().AnyTogglesOn() == false)
            {
            //    gameController.equippedLure = null;
               // gameController.lureIconHolder.color = gameController.invisibleInk;
            }

            else
            {
              //  gameController.lureIconHolder.color = gameController.visibleInk;
            }
        }
        if(item == null)
        {
            gameObject.Destroy();
        }
        else
        {
            if(item.quantity<=0)
            {
                gameObject.Destroy();
            }
        }
    }

    public void UpdateInfo()
    {
        Text displayNameText = transform.Find("ItemName").GetComponent<Text>();
        Text displayQuantityText = transform.Find("ItemQuantity").GetComponent<Text>();
        Image displayImage = transform.Find("ItemImage").GetComponent<Image>();
        
        if (item!=null)
        {
            displayNameText.text = item.itemName;
            displayQuantityText.text = item.quantity.ToString();
            displayImage.sprite = item.icon;
            itemQuanityText.text = item.quantity.ToString();
        }
        
        else
        {
            displayNameText.text = "";
            displayQuantityText.text = "";
            displayImage.sprite = null;
            itemQuanityText.text = "";
        }


    }

    public void Use()
    {
        
        if(item && SceneManager.GetActiveScene().name == "Backpack")
        {
            print("ahhhh");
            if (item.itemTypes == Item.ItemTypes.Lure)
            {
                for (int i = 0; i < inventory.trap.Count; i++)
                {
                    if (item.name == inventory.trap[i].name)
                    {
                        trap = inventory.trap[i];
                    }
                }
                //gameObject.AddComponent<SelectTrap>();
            }
            if (trap != null)
            {
                for (int i = 0; i < inventory.trap.Count; i++)
                {
                    if(GameObject.Find("ScriptController").GetComponent<GameController>() != null)
                    {
                        if (GameObject.Find("ScriptController").GetComponent<GameController>().equippedLure == inventory.trap[i])
                        {

                            inventory.trap[i].equipped = true;
                        }

                        else
                        {
                            inventory.trap[i].equipped = false;
                        }
                    }

                }
               
            } 
        }

        if(item && SceneManager.GetActiveScene().name != "Backpack")
        {
             if (item.itemTypes == Item.ItemTypes.Lure)
             {
                 for (int i = 0; i < inventory.trap.Count; i++)
                 {
                     if (item.name == inventory.trap[i].name)
                     {
                         trap = inventory.trap[i];
                     }
                 }
                 //gameObject.AddComponent<SelectTrap>();

             } 
        }


    }
    
    public void UpdateInventorySelected()
    {
        if(item != null && itemDisplay!=null)
        {

            itemDisplay.sprite = item.icon;
            itemNameExtra.text = item.itemName;
            quantityText.text = "x" + item.quantity;
            flavourText.text = item.flavourText;
            
        }
        
        else if(itemDisplay!=null)
        {
            itemNameExtra.text = "";
            quantityText.text = "";
            flavourText.text = "Select an item";
        }
        
    }
}
