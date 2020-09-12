using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopUIManager : MonoBehaviour
{
    [Header("Toggle Changer / Pages")]
    public Toggle lureToggle;
    public Toggle incenseToggle;
    public Toggle whistlesToggle;
    public Toggle travelPassToggle;

    public GameObject luresContainer;
    public GameObject incenseContainer;
    public GameObject whistlesContainer;
    public GameObject travelPassContainer;

    public int toggleCount;
    public int baseToggle;

    public ItemShop itemShop;
    // Start is called before the first frame update
    void Start()
    {
       // itemShop.CreateLists();
        CategoryChanger();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CategoryChanger()
    {
        if (baseToggle <= toggleCount)
        {
            baseToggle = toggleCount;
            toggleCount++;

            itemShop.UpdateLureContainerSlots();
            itemShop.UpdateTravelPassContainerSlots();
            itemShop.UpdateWhistleContainerSlots();
            itemShop.UpdateIncenseContainerSlots();


            // itemShop.CreateLists();

            if (lureToggle.isOn)
            {
                luresContainer.SetActive(true);
                incenseContainer.SetActive(false);
                whistlesContainer.SetActive(false);
                travelPassContainer.SetActive(false);
                luresContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemShopSlotController>().Awoke();
                luresContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemShopSlotController>().UpdateInventorySelected();

            }

            if (incenseToggle.isOn)
            {
                incenseContainer.SetActive(true);
                luresContainer.SetActive(false);
                whistlesContainer.SetActive(false);
                travelPassContainer.SetActive(false);
                incenseContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemShopSlotController>().Awoke();
                incenseContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemShopSlotController>().UpdateInventorySelected();

            }

            if (whistlesToggle.isOn)
            {
                whistlesContainer.SetActive(true);
                luresContainer.SetActive(false);
                incenseContainer.SetActive(false);
                travelPassContainer.SetActive(false);
                whistlesContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemShopSlotController>().Awoke();
                whistlesContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemShopSlotController>().UpdateInventorySelected();

            }
            if (travelPassToggle.isOn)
            {
                whistlesContainer.SetActive(false);
                luresContainer.SetActive(false);
                incenseContainer.SetActive(false);
                travelPassContainer.SetActive(true);
                travelPassContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemShopSlotController>().Awoke();
                travelPassContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemShopSlotController>().UpdateInventorySelected();

            }
        }
    }
}
