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
        //toggle = gameObject.transform.GetChild(3).GetComponent<Toggle>();
        if (SceneManager.GetActiveScene().name == "GameWorld")
        {
            useItemScript = GameObject.Find("ScriptController").GetComponent<UseItem>();
            useItemButton = GameObject.Find("USE BUTTON").GetComponent<Button>();
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
        if(inventorySlotController.item.itemTypes == Item.ItemTypes.Lure)
        {
            toggle.isOn = true;
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

        }

        if (SceneManager.GetActiveScene().name == "GameWorld" && inventorySlotController.trap)
        {
            if (inventorySlotController.trap.itemTypes == Item.ItemTypes.Lure)
            {
                useItemButton.gameObject.SetActive(false);
            }
            useItemScript.gameController.equippedLure = inventorySlotController.trap;
            inventorySlotController.trap.equipped = true;
            CheckEquippedOrNot();
        }
    }
}
