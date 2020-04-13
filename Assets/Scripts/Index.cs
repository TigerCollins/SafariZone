using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Index : MonoBehaviour
{
    public static Index instance;

    // [Header("Index Specific")]


    [Header("List Variables")]
    public List<Creature> creatureList = new List<Creature>();// = new List<inventoryObject.Inventory>;   //new List<Item>();

    [Header("Game Object Variables")]
    public GameObject creatureContainerPrefab;
    public GameObject creatureListContainer;
    


    private void Start()
    {
        instance = this;
        // list = inventoryObject.Inventory;

        for (int i = 0; i < creatureList.Count; i++)
        {
            Instantiate(creatureContainerPrefab, creatureListContainer.transform);
            UpdateCreatureContainerSlots();
        }

    }

   public void UpdateCreatureContainerSlots()
    {
        int index = 0;
        foreach (Transform child in creatureListContainer.transform)
        {
            //Updates slot indexs name and icon
            IndexSlotController slot = child.GetComponent<IndexSlotController>();

            if (index < creatureList.Count)
            {
                slot.creature = creatureList[index];
            }

            else
            {
                slot.creature = null;
            }

            slot.UpdateInfo();

            index++;
        }
    }
    
}

