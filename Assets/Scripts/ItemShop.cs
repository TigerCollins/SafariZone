using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemShop : MonoBehaviour
{
    public static ItemShop instance;

    [Header("Inventory and Shop Specific")]
    public int quantityWanted;
    public Item selectedItem;
    public InventoryObject inventoryObject;

    [Header("List Variables")]
    public List<Lure> lureList = new List<Lure>();// = new List<inventoryObject.Inventory>;   //new List<Item>();
    public List<Incense> incenseList = new List<Incense>();
    public List<Whistles> whistlesList = new List<Whistles>();

    [Header("Game Object Variables")]
    public GameObject player;
    public GameObject lureListContainer;
    public GameObject incenseListContainer;
    public GameObject whistlesListContainer;
    public GameObject itemContainerPrefab;
    public InputField quantityInputfield;

    private void Start()
    {
        instance = this;
       // list = inventoryObject.Inventory;

        for (int i = 0; i < lureList.Count; i++)
        {
            Instantiate(itemContainerPrefab, lureListContainer.transform);
            UpdateLureContainerSlots();
        }
        
        for (int i = 0; i < incenseList.Count; i++)
        {
            Instantiate(itemContainerPrefab, incenseListContainer.transform);
            UpdateIncenseContainerSlots();
        }
        
        for (int i = 0; i < whistlesList.Count; i++)
        {
            Instantiate(itemContainerPrefab, whistlesListContainer.transform);
            UpdateWhistleContainerSlots();
        }
        



    }

    public void BuyItem()
    {
        if (selectedItem != null)
        {
            if (inventoryObject.Inventory.Contains(selectedItem) == true)
            {
                if((selectedItem.quantity + quantityWanted) > 99)
                {
                    selectedItem.quantity = 99;
                    Debug.Log("Inventory Full");
                }

                else
                {
                    selectedItem.quantity += quantityWanted;
                    Debug.Log("+ " + quantityWanted + " added to " + selectedItem);
                }
                
            }

            else
            {
                inventoryObject.Inventory.Capacity = inventoryObject.Inventory.Capacity + 1;
                inventoryObject.Inventory.Add(selectedItem);
                selectedItem.quantity += quantityWanted;
                Debug.Log("Added" + selectedItem + "to your inventory");
            }
        }
    }


    void UpdateLureContainerSlots()
    {
        int index = 0;
        foreach (Transform child in lureListContainer.transform)
        {
            //Updates slot indexs name and icon
            ItemShopSlotController slot = child.GetComponent<ItemShopSlotController>();

            if (index < lureList.Count)
            {
                slot.item = lureList[index];
            }

            else
            {
                slot.item = null;
            }

            slot.UpdateInfo();

            index++;
        }
    }

    void UpdateWhistleContainerSlots()
    {
        int index = 0;
        foreach (Transform child in whistlesListContainer.transform)
        {
            //Updates slot indexs name and icon
            ItemShopSlotController slot = child.GetComponent<ItemShopSlotController>();

            if (index < whistlesList.Count)
            {
                slot.item = whistlesList[index];
            }

            else
            {
                slot.item = null;
            }

            slot.UpdateInfo();

            index++;
        }
    }

    void UpdateIncenseContainerSlots()
    {
        int index = 0;
        foreach (Transform child in incenseListContainer.transform)
        {
            //Updates slot indexs name and icon
            ItemShopSlotController slot = child.GetComponent<ItemShopSlotController>();

            if (index < incenseList.Count)
            {
                slot.item = incenseList[index];
            }

            else
            {
                slot.item = null;
            }

            slot.UpdateInfo();

            index++;
        }
    }

    public void UpdateQuantityWanted()
    {
        quantityWanted = int.Parse(quantityInputfield.text);
    }

    //COOL CODE
    /*if(Input.GetKeyDown(KeyCode.I))
    {
        inventoryEnabled = !inventoryEnabled;
    }
    */

}

