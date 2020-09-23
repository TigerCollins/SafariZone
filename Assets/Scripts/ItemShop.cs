using Aura2API;
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
    // = new List<inventoryObject.Inventory>;   //new List<Item>();
    public List<Incense> incenseList = new List<Incense>();
    public List<Lure> lureList = new List<Lure>();
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
    public GameObject lastItem;

    public void Awake()
    {
        CreateLists();
    }
     
    public void CreateLists()
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

        if(lureListContainer.transform.GetChild(0) != null)
        {
            lureListContainer.transform.GetChild(0).GetComponent<ItemShopSlotController>().UpdateInventorySelected();
        }
        //pr
    }
/*
    public void DestroyList()
    {
        foreach (Transform child in travelPassListContainer.transform)
        {
            ItemShopSlotController slot = child.GetComponent<ItemShopSlotController>();

            slot.gameObject.Destroy();
        }

        foreach (Transform child in lureListContainer.transform)
        {
            ItemShopSlotController slot = child.GetComponent<ItemShopSlotController>();

            slot.gameObject.Destroy();
        }

        foreach (Transform child in whistlesListContainer.transform)
        {
            ItemShopSlotController slot = child.GetComponent<ItemShopSlotController>();

            slot.gameObject.Destroy();
        }

        foreach (Transform child in incenseListContainer.transform)
        {
            ItemShopSlotController slot = child.GetComponent<ItemShopSlotController>();

            slot.gameObject.Destroy();
        }
    }
    */
    public void BuyItem()
    {
        if (selectedItem != null)
        {
           
         
       
            foreach (Transform child in incenseListContainer.transform)
            {
                //Updates slot indexs name and icon
                ItemShopSlotController slot = child.GetComponent<ItemShopSlotController>();

                slot.UpdateInfo();
            }
            foreach (Transform child in lureListContainer.transform)
            {
                //Updates slot indexs name and icon
                ItemShopSlotController slot = child.GetComponent<ItemShopSlotController>();

                slot.UpdateInfo();
            }
            foreach (Transform child in whistlesListContainer.transform)
            {
                //Updates slot indexs name and icon
                ItemShopSlotController slot = child.GetComponent<ItemShopSlotController>();

                slot.UpdateInfo();
            }
            foreach (Transform child in travelPassListContainer.transform)
            {
                //Updates slot indexs name and icon
                ItemShopSlotController slot = child.GetComponent<ItemShopSlotController>();

                slot.UpdateInfo();
            }

            if (selectedItem.price <= currencyScript.wallet)
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
                            currencyScript.RemoveFromLocalWallet(selectedItem.price * (99 - currentItemQuantity));
                            playerVariables.moneySpentToDate += selectedItem.price * (99 - currentItemQuantity);
                            walletAmount.text = currencyScript.wallet.ToString();
                            Debug.Log("Inventory Full");
                            selectedItem.amountOwnedLifetime += quantityWanted;

                        }

                        if ((selectedItem.quantity + quantityWanted) < 99 && selectedItem.itemTypes != Item.ItemTypes.Lure)
                        {
                            selectedItem.quantity += quantityWanted;
                            currencyScript.RemoveFromLocalWallet(selectedItem.price * quantityWanted);
                            playerVariables.moneySpentToDate += selectedItem.price * quantityWanted;
                            walletAmount.text = currencyScript.wallet.ToString();
                            selectedItem.amountOwnedLifetime += quantityWanted;
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
                            currencyScript.RemoveFromLocalWallet(selectedItem.price * (99 - currentItemQuantity));
                            playerVariables.moneySpentToDate += selectedItem.price * (99 - currentItemQuantity);
                            walletAmount.text = currencyScript.wallet.ToString();
                            selectedItem.amountOwnedLifetime += quantityWanted;
                        }

                        if (selectedItem.canHaveMultiple == false && selectedItem.quantity == 0)
                        {
                            selectedItem.quantity += 0;
                            selectedItem.amountOwnedLifetime += 1;
                            //currencyScript.wallet -= selectedItem.price;
                            walletAmount.text = currencyScript.wallet.ToString();
                        }

                    }
                    if (selectedItem.itemTypes == Item.ItemTypes.Lure && selectedItem.canHaveMultiple == true && selectedItem.quantity + quantityWanted < 99)
                    {
                        selectedItem.quantity += quantityWanted;
                        selectedItem.amountOwnedLifetime += quantityWanted;
                        currencyScript.wallet -= selectedItem.price * quantityWanted;
                        currencyScript.RemoveFromLocalWallet(selectedItem.price * quantityWanted);
                        playerVariables.moneySpentToDate += selectedItem.price * quantityWanted;
                        walletAmount.text = currencyScript.wallet.ToString();
                    }
                   
                }

                else
                {
                    inventoryObject.Inventory.Capacity = inventoryObject.Inventory.Capacity + 1;
                    inventoryObject.Inventory.Add(selectedItem);
                    selectedItem.amountOwnedLifetime += quantityWanted;
                    selectedItem.quantity += quantityWanted;
                    currencyScript.wallet -= selectedItem.price;
                    currencyScript.RemoveFromLocalWallet(selectedItem.price);
                    playerVariables.moneySpentToDate += selectedItem.price;
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
        if (lastItem.GetComponent<ItemShopSlotController>() != null)
        {
            lastItem.transform.gameObject.GetComponent<ItemShopSlotController>().UpdateInfo();
        }
        if (selectedItem != null)
        {
            bagQuantity.text = "x" + selectedItem.quantity.ToString();
        }
    }


    public void UpdateTravelPassContainerSlots()
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

    public void UpdateLureContainerSlots()
    {
        int i = 0;
        foreach (Transform child in lureListContainer.transform)
        {
            //Updates slot indexs name and icon
            ItemShopSlotController slot = child.GetComponent<ItemShopSlotController>();

            if ( i < lureList.Count)
            {
                slot.item = lureList[i];
            }

            else
            {
                slot.item = null;
            }

            slot.UpdateInfo();

            i += 1;
        }
    }

    public void UpdateWhistleContainerSlots()
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

    public void UpdateIncenseContainerSlots()
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

