using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour
{
    public InventorySlotController inventorySlotController;
    public UseItem useItemScript;
    public Button useItemButton;
   // public Button equipItemButton;
    public Toggle toggle;

    public void Awake()
    {
        toggle = GetComponent<Toggle>();
        if (SceneManager.GetActiveScene().name == "GameWorld")
        {
            useItemScript = GameObject.Find("_ScriptController").GetComponent<UseItem>();
            useItemButton = GameObject.Find("_ScriptController").GetComponent<GameController>().buyButton;
         //   equipItemButton = GameObject.Find("EQUIP BUTTON").GetComponent<Button>();

        }
    }

    public void Start()
    {
        if(inventorySlotController.trap && SceneManager.GetActiveScene().name == "GameWorld")
        {
            if (inventorySlotController.trap.equipped == true)
            {
                toggle.isOn = true;
            }

            else
            {
                //this.equippedImage.gameObject.SetActive(false);
            }
        }
      
    }

    public void ChangeEquippedIcon()
    {
        print("bruh");

    }

    public void CheckEquippedOrNot()
    {
        


    }


    public void SelectedObject()
    {
       
        if(inventorySlotController.item.itemTypes == Item.ItemTypes.Lure && toggle.isOn == false)
        {
           // toggle.isOn = true;
        }

        if (SceneManager.GetActiveScene().name == "GameWorld" && inventorySlotController.item != null)
        {
            useItemScript.selectedItem = inventorySlotController.item;
            useItemScript.itemPrefabHolder = gameObject;

            if (inventorySlotController.item.itemTypes == Item.ItemTypes.Incense || inventorySlotController.item.itemTypes == Item.ItemTypes.Whistle)
            {
                useItemButton.gameObject.SetActive(true);
            }
            else
            {
                useItemButton.gameObject.SetActive(false);
            }

            if (inventorySlotController.trap != null && inventorySlotController.trap.itemTypes == Item.ItemTypes.Lure)
            {
                useItemButton.gameObject.SetActive(false);
                inventorySlotController.trap.equipped = true;
                useItemScript.gameController.equippedLure = inventorySlotController.trap;
            }

        }

        if (SceneManager.GetActiveScene().name == "GameWorld" && inventorySlotController.item != null)
        {
           
           

            CheckEquippedOrNot();
        }
    }
}
