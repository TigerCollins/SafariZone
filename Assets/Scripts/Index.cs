using Aura2API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private int indexTracker;
    


    private void Start()
    {
        instance = this;
        // list = inventoryObject.Inventory;

      if(SceneManager.GetActiveScene().name != "GameWorld")
        {
            CreateList();
        }
       

    }

    public void UpdateBlurb()
    {

            int index = 0;
            foreach (Transform child in creatureListContainer.transform)
            {
                //Updates slot indexs name and icon
                IndexSlotController slot = child.GetComponent<IndexSlotController>();

                if(slot!=null && index == 0)
                {
                    slot.UpdateInfoSelected();
                }
                index++;
            }


    }

    public void RepopulateList()
    {
       // WipeList();
        CreateList();
    }

    public void CreateList()


    {
        if (SceneManager.GetActiveScene().name!="GameWorld")
        {
            for (int i = 0; i < creatureList.Count; i++)
            {
                Instantiate(creatureContainerPrefab, creatureListContainer.transform);
                UpdateCreatureContainerSlots();
            }
        }

        
        else if (creatureListContainer.transform.childCount == 0)
        {

            for (int i = 0; i < creatureList.Count; i++)
            {
                Instantiate(creatureContainerPrefab, creatureListContainer.transform);
                UpdateCreatureContainerSlots();
            }
            indexTracker = 0;
        }
        //creatureListContainer.transform.GetChild(0).GetComponent<IndexSlotController>().UpdateInfo();
    }

 /*   public void WipeList()
    {
        indexTracker = 0;
        creatureCount = creatureListContainer.transform.childCount;
        if (creatureListContainer.transform.childCount > 0)
        {

            foreach (Transform litty in creatureListContainer.transform)
            {
                GameObject indexButton = litty.gameObject;

                indexTracker++;
                Destroy(indexButton) ;
            }
            CreateList();
            indexTracker = 0;
            for (int i = 0; i < creatureList.Count; i++)
            {
                Instantiate(creatureContainerPrefab, creatureListContainer.transform);
                UpdateCreatureContainerSlots();
            }
        }

    }
    */
   public void UpdateCreatureContainerSlots()
    {
        print("Update Container Slots Index Is:" + indexTracker);
        foreach (Transform child in creatureListContainer.transform)
        {
            //Updates slot indexs name and icon
            IndexSlotController slot = child.GetComponent<IndexSlotController>();
            if (indexTracker < creatureList.Count)
            {

                slot.creature = creatureList[indexTracker];
            }

            else
            {
                slot.creature = null;
            }

            slot.UpdateInfo();

            indexTracker++;
        }
        indexTracker = 0;
    }
    
}

