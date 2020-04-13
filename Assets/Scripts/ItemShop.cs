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
    public Currency currencyScript;
    public PlayerVariables playerVariables;

    [Header("List Variables")]
    public List<Lure> lureList = new List<Lure>();// = new List<inventoryObject.Inventory>;   //new List<Item>();
    public List<Incense> incenseList = new List<Incense>();
    public List<Whistles> whistlesList = new List<Whistles>();
    public List<TravelPass> travelPassList = new List<TravelPass>();

    [Header("Game Object Variables")]
    public GameObject player;
    public GameObject lureListContainer;
    public GameObject incenseListContainer;
    public GameObject whistlesListContainer;
    public GameObject travelPassListContainer;
    public GameObject itemContainerPrefab;
    public InputField quantityInputfield;
    public Text bagQuantity;
    public Text walletAmount;

    public void Awake()
    {
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

        for (int i = 0; i < travelPassList.Count; i++)
        {
            Instantiate(itemContainerPrefab, travelPassListContainer.transform);
            UpdateTravelPassContainerSlots();
        }
    }

    private void Start()
    {
        instance = this;
        walletAmount.text = currencyScript.wallet.ToString();
        // list = inventoryObject.Inventory;

        print(lureListContainer.transform.GetChild(0));
        if(lureListContainer.transform.GetChild(0) != null)
        {
            lureListContainer.transform.GetChild(0).GetComponent<ItemShopSlotController>().UpdateInventorySelected();
        }

    }

    public void BuyItem()
    {
        if (selectedItem != null)
        {
            if(selectedItem.price <= currencyScript.wallet)
            {
                if (inventoryObject.Inventory.Contains(selectedItem) == true)
                {
                    
                    if (selectedItem.itemTypes != Item.ItemTypes.Lure)
                    {
                        if ((selectedItem.quantity + quantityWanted) > 99 && selectedItem.itemTypes != Item.ItemTypes.Lure)
                        {
                            int currentItemQuantity = selectedItem.quantity;
                            selectedItem.quantity = 99;
                            currencyScript.wallet -= selectedItem.price * (99 - currentItemQuantity);
                            walletAmount.text = currencyScript.wallet.ToString();
                            Debug.Log("Inventory Full");


                        }

                        if ((selectedItem.quantity + quantityWanted) < 99 && selectedItem.itemTypes != Item.ItemTypes.Lure)
                        {
                            selectedItem.quantity += quantityWanted;
                            currencyScript.wallet -= selectedItem.price * quantityWanted;
                            walletAmount.text = currencyScript.wallet.ToString();
                        }

                    }

                    if (selectedItem.itemTypes == Item.ItemTypes.Lure)
                    {
                        if (selectedItem.canHaveMultiple == true && selectedItem.quantity + quantityWanted > 99)
                        {
                            Debug.Log("Inventory Full");
                            int currentItemQuantity = selectedItem.quantity;
                            selectedItem.quantity = 99;
                            currencyScript.wallet -= selectedItem.price * (99 - currentItemQuantity);
                            walletAmount.text = currencyScript.wallet.ToString();
                        }

                        if (selectedItem.canHaveMultiple == false && selectedItem.quantity == 0)
                        {
                            selectedItem.quantity += 1;
                            //currencyScript.wallet -= selectedItem.price;
                            walletAmount.text = currencyScript.wallet.ToString();
                        }

                    }
                    if (selectedItem.itemTypes == Item.ItemTypes.Lure && selectedItem.canHaveMultiple == true && selectedItem.quantity + quantityWanted < 99)
                    {
                        selectedItem.quantity += quantityWanted;
                        currencyScript.wallet -= selectedItem.price * quantityWanted;
                        walletAmount.text = currencyScript.wallet.ToString();
                    }
                   
                }

                else
                {
                    inventoryObject.Inventory.Capacity = inventoryObject.Inventory.Capacity + 1;
                    inventoryObject.Inventory.Add(selectedItem);
                    selectedItem.quantity += quantityWanted;
                    currencyScript.wallet -= selectedItem.price;
                    walletAmount.text = currencyScript.wallet.ToString();
                    Debug.Log("Added" + selectedItem + "to your inventory");
                }

                if (selectedItem.travelPass && !selectedItem.isOwned)
                {
                    if (selectedItem.itemName == "Guidance Isle Travel Pass")
                    {
                        playerVariables.guidanceIsle = true;
                    }

                    if (selectedItem.itemName == "Azalea Forest Travel Pass")
                    {
                        playerVariables.AzaleaForest = true;
                    }

                    if (selectedItem.itemName == "Kebar Village Travel Pass")
                    {
                        playerVariables.kebarVillage = true;
                    }

                    if (selectedItem.itemName == "Lake Perano Travel Pass")
                    {
                        playerVariables.lakePerano = true;
                    }

                    if (selectedItem.itemName == "Parhar Village Travel Pass")
                    {
                        playerVariables.parharVillage = true;
                    }

                    if (selectedItem.itemName == "Egress Cave Travel Pass")
                    {
                        playerVariables.egressCave = true;
                    }
                }

                UpdateLureContainerSlots();
                UpdateWhistleContainerSlots();
                UpdateIncenseContainerSlots();
                UpdateTravelPassContainerSlots();
            }
            
        }
        if (selectedItem != null)
        {
            bagQuantity.text = "x" + selectedItem.quantity.ToString();
        }
    }


    void UpdateTravelPassContainerSlots()
    {
        int index = 0;
        foreach (Transform child in travelPassListContainer.transform)
        {
            //Updates slot indexs name and icon
            ItemShopSlotController slot = child.GetComponent<ItemShopSlotController>();

            if (index < travelPassList.Count)
            {
                slot.item = travelPassList[index];
            }

            else
            {
                slot.item = null;
            }

            slot.UpdateInfo();

            index++;
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

