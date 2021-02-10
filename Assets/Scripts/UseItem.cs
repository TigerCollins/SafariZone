using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UseItem : MonoBehaviour
{
    [Header("References")]
    public GameController gameController;
    public Inventory inventory;
    [Header("Variables")]
    public Item selectedItem;
    public Lure selectedTrap;
    public GameObject itemPrefabHolder;
    public Button useItemButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectItem()
    {
       // if()
    }

    public void ChooseLure()
    {

            gameController.gameObject.GetComponent<TrapManager>().placedTrap = selectedTrap;

    }

    public void UseSelectedItem()
    {
        print("UseItemScript");
        gameController.gameoverController.ItemUsed(selectedItem);
        if (SceneManager.GetActiveScene().name == "GameWorld")
        {

                selectedItem.quantity -= 1;
                itemPrefabHolder.GetComponent<InventorySlotController>().UpdateInfo();
                if (selectedItem.quantity <= 0)
                {
                    selectedItem.quantity = 0;
                    for (int i = 0; i < inventory.list.Count; i++)
                    {
                        if (inventory.list[i].name == selectedItem.name)
                        {
                            Destroy(itemPrefabHolder);
                            inventory.list.Remove(inventory.list[i]);
                            inventory.UpdateContainerSlots();

                            break;
                        }
                    }

                }
            
               

            if (selectedItem.itemTypes == Item.ItemTypes.Incense)
            {
                gameController.activeIncense = selectedItem;
                gameController.ItemUpdate();
            }

            if (selectedItem.itemTypes == Item.ItemTypes.Whistle)
            {
                gameController.activeWhistle = selectedItem;
                gameController.ItemUpdate();
            }
            if (gameController.activeIncense != null || gameController.activeWhistle != null)
            {
                useItemButton.interactable = false;
            }

            else
            {
                useItemButton.interactable = true;
            }
        }
    }
}
