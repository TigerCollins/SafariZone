using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameController gameController;
    public static Inventory instance;
    public Item selectedItem;
    public List<Lure> wholeLureList;
    public List<Whistles> wholeWhistleList;
    public List<Incense> wholeIncenseList;




    public InventoryObject inventoryObject;
    public List<Item> list;// = new List<inventoryObject.Inventory>;   //new List<Item>();
    public List<Lure> trap;
    public List<Whistles> whistles;
    public List<Incense> incenses;

    public GameObject player;
    public GameObject inventoryContainer;
    public GameObject lureListContainer;
    public GameObject whistlesListContainer;
    public GameObject incenseListContainer;
    public GameObject itemContainerPrefab;




    void ForceSerialization()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    private void Awake()
    {
        // inventoryContainer.transform.GetChild(0).GetComponent<InventorySlotController>().UpdateInventorySelected();
        list = inventoryObject.Inventory;
        if(SceneManager.GetActiveScene().name == "GameWorld")
        {
            gameController = GameObject.Find("_ScriptController").GetComponent<GameController>();
        }

        else
        {
            gameController = GameObject.Find("ScriptController").GetComponent<GameController>();
        }

        for (int i = 0; i < trap.Count; i++)
        {
            if (trap[i].equipped == true)
            {
                gameController.equippedLure = trap[i];
                break;
            }
        }

        DestroyItem();
        DestroyIncense();
        DestroyLure();
        DestroyWhistle();

        if (list.Count > 0)
        {

            for (int i = 0; i < list.Count; i++)
            {

                Instantiate(itemContainerPrefab, inventoryContainer.transform);
                if (list[i].itemTypes == Item.ItemTypes.Lure)
                {
                    for (int ii = 0; ii < wholeLureList.Count; ii++)
                    {
                        if (wholeLureList[ii].itemName == list[i].itemName)
                        {
                            trap.Add(wholeLureList[ii]);
                        }
                    }
                }

                if (list[i].itemTypes == Item.ItemTypes.Whistle)
                {
                    for (int ii = 0; ii < wholeWhistleList.Count; ii++)
                    {
                        if (wholeWhistleList[ii].itemName == list[i].itemName)
                        {
                            whistles.Add(wholeWhistleList[ii]);
                        }
                    }
                }

                if (list[i].itemTypes == Item.ItemTypes.Incense)
                {
                    for (int ii = 0; ii < wholeIncenseList.Count; ii++)
                    {
                        if (wholeIncenseList[ii].itemName == list[i].itemName)
                        {
                            incenses.Add(wholeIncenseList[ii]);
                        }
                    }
                }
            }

            //Lures
            for (int i = 0; i < trap.Count; i++)
            {
                Instantiate(itemContainerPrefab, lureListContainer.transform);
                UpdateLureContainerSlots();
            }

            for (int i = 0; i < incenses.Count; i++)
            {
                Instantiate(itemContainerPrefab, incenseListContainer.transform);
                UpdateIncenseContainerSlots();
            }

            for (int i = 0; i < whistles.Count; i++)
            {
                Instantiate(itemContainerPrefab, whistlesListContainer.transform);
                UpdateWhistleContainerSlots();
            }
            UpdateContainerSlots();
            ForceSerialization();


        }
    }

    private void OnEnable()
    {
        DestroyItem();
        DestroyIncense();
        DestroyLure();
        DestroyWhistle();
    }

    private void Start()
    {
        instance = this;


        //All

       if(inventoryContainer.transform.GetChild(0)!=null)
        {
            eventSystem.SetSelectedGameObject(inventoryContainer.transform.GetChild(0).gameObject);
            inventoryContainer.transform.GetChild(0).GetComponent<InventorySlotController>().UpdateInventorySelected();
        }
        //print(inventoryContainer.transform.GetChild(0).gameObject);
        //


    }

    public void DestroyItem()
    {
        foreach (Transform child in inventoryContainer.transform)
        {
            InventorySlotController slot = child.GetComponent<InventorySlotController>();
            if (slot.item.quantity <= 0)
            {

 
                for (int i = 0; i < inventoryObject.Inventory.Count; i++)
                {
                    if (slot.item == inventoryObject.Inventory[i])
                    {
                        inventoryObject.Inventory.Remove(inventoryObject.Inventory[i]);
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    if (slot.item == list[i])
                    {
                        list.Remove(list[i]);
                    }
                }
                Destroy(child.gameObject);
            }
        }
    }

    public void DestroyIncense()
    {
        foreach (Transform child in incenseListContainer.transform)
        {
            InventorySlotController slot = child.GetComponent<InventorySlotController>();
            if (slot.item.quantity < 1)
            {
                Destroy(child.gameObject);
                for (int i = 0; i < incenses.Count; i++)
                {
                    if (slot.item == incenses[i])
                    {
                        incenses.Remove(incenses[i]);
                    }
                }
            }
        }
    }

    public void DestroyLure()
    {
        foreach (Transform child in lureListContainer.transform)
        {
            InventorySlotController slot = child.GetComponent<InventorySlotController>();
            if (slot.item.quantity < 1)
            {
                Destroy(child.gameObject);
                for (int i = 0; i < trap.Count; i++)
                {
                    if (slot.item == trap[i])
                    {
                        trap.Remove(trap[i]);
                    }
                }
            }
        }
    }

    public void DestroyWhistle()
    {
        foreach (Transform child in whistlesListContainer.transform)
        {
            InventorySlotController slot = child.GetComponent<InventorySlotController>();
            if (slot.item.quantity < 1)
            {
                Destroy(child.gameObject);
                for (int i = 0; i < whistles.Count; i++)
                {
                    if (slot.item == whistles[i])
                    {
                        whistles.Remove(whistles[i]);
                    }
                }
            }
        }
    }

    public void UpdateContainerSlots()
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

            ForceSerialization();
            index++;
        }
        ForceSerialization();
        
    }

    public void UpdateIncenseContainerSlots()
    {
        int index = 0;
        foreach (Transform child in incenseListContainer.transform)
        {
            //Updates slot indexs name and icon
            InventorySlotController slot = child.GetComponent<InventorySlotController>();

            if (index < incenses.Count)
            {
                slot.item = incenses[index];
            }

            else
            {
                slot.item = null;
            }

            if (slot.item.quantity < 1)
            {
                for (int i = 0; i < incenses.Count; i++)
                {
                    if (slot.item == incenses[i])
                    {
                        list.Remove(incenses[i]);
                    }
                }
            }

            slot.UpdateInfo();

            ForceSerialization();
            index++;
        }
        ForceSerialization();
        incenseListContainer.transform.GetChild(0).GetComponent<InventorySlotController>().UpdateInventorySelected();
    }
    public void UpdateLureContainerSlots()
    {
        int index = 0;
        foreach (Transform child in lureListContainer.transform)
        {
            //Updates slot indexs name and icon
            InventorySlotController slot = child.GetComponent<InventorySlotController>();

            if (index < trap.Count)
            {
                slot.item = trap[index];
            }

            else
            {
                slot.item = null;
            }

            if (slot.item.quantity < 1)
            {
                for (int i = 0; i < trap.Count; i++)
                {
                    if (slot.item == trap[i])
                    {
                        list.Remove(trap[i]);
                    }
                }
            }

            slot.UpdateInfo();

            ForceSerialization();
            index++;
        }
        ForceSerialization();
        lureListContainer.transform.GetChild(0).GetComponent<InventorySlotController>().UpdateInventorySelected();
    }

    public void UpdateWhistleContainerSlots()
    {
        int index = 0;
        foreach (Transform child in whistlesListContainer.transform)
        {
            //Updates slot indexs name and icon
            InventorySlotController slot = child.GetComponent<InventorySlotController>();

            if (index < whistles.Count)
            {
                slot.item = whistles[index];
            }

            else
            {
                slot.item = null;
            }

            if (slot.item.quantity < 1)
            {
                for (int i = 0; i < whistles.Count; i++)
                {
                    if (slot.item == whistles[i])
                    {
                        list.Remove(whistles[i]);
                    }
                }
            }

            slot.UpdateInfo();

            ForceSerialization();
            index++;
        }
        ForceSerialization();
    }

    //COOL CODE
    /*if(Input.GetKeyDown(KeyCode.I))
    {
        inventoryEnabled = !inventoryEnabled;
    }
    */
    public void Update()
    {

    }
}
