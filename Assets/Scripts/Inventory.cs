using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<Item> list = new List<Item>();

    public GameObject player;
    public GameObject inventoryContainer;
    public GameObject itemContainerPrefab;


    private void Start()
    {
        instance = this;

        for (int i = 0; i < list.Count; i++)
        {
            Instantiate(itemContainerPrefab, inventoryContainer.transform);
        }
        updateContainerSlots();
       
        
    }

    void updateContainerSlots()
    {
        int index = 0;
        foreach(Transform child in inventoryContainer.transform)
        {
            //Updates slot indexs name and icon
            InventorySlotController slot = child.GetComponent<InventorySlotController>();

            if(index < list.Count)
            {
                slot.item = list[index];
            }

            else
            {
                slot.item = null;
            }

            slot.UpdateInfo();

            index++;
        }
    }

        //COOL CODE
        /*if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;
        }
        */

}
