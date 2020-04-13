using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectTrap : MonoBehaviour
{
    public InventorySlotController inventorySlotController;
    public UseItem useItemScript;
    public Button useItemButton;

    public void Awake()
    {
        if (SceneManager.GetActiveScene().name == "World")
        {
            useItemScript = GameObject.Find("ScriptController").GetComponent<UseItem>();
            useItemButton = GameObject.Find("USE BUTTON").GetComponent<Button>();
        }
    }

    public void SelectedObject()
    {
        if (SceneManager.GetActiveScene().name == "World" && inventorySlotController.item != null)
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
    }
}
