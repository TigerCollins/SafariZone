using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ItemShopUIManager : MonoBehaviour
{
    public EventSystem eventSystem;
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
                eventSystem.SetSelectedGameObject(luresContainer.transform.GetChild(0).GetChild(0).GetChild(0).gameObject);
              //  luresContainer.transform.parent.parent.gameObject.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
                luresContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemShopSlotController>().UpdateInventorySelected();

            }

            if (incenseToggle.isOn)
            {
                incenseContainer.SetActive(true);
                luresContainer.SetActive(false);
                whistlesContainer.SetActive(false);
                travelPassContainer.SetActive(false);
                incenseContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemShopSlotController>().Awoke();
                eventSystem.SetSelectedGameObject(incenseContainer.transform.GetChild(0).GetChild(0).GetChild(0).gameObject);
               // incenseContainer.transform.parent.parent.gameObject.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
                incenseContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemShopSlotController>().UpdateInventorySelected();

            }

            if (whistlesToggle.isOn)
            {
                whistlesContainer.SetActive(true);
                luresContainer.SetActive(false);
                incenseContainer.SetActive(false);
                travelPassContainer.SetActive(false);
                whistlesContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemShopSlotController>().Awoke();
              //  whistlesContainer.transform.parent.parent.gameObject.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
              eventSystem.SetSelectedGameObject(whistlesContainer.transform.GetChild(0).GetChild(0).GetChild(0).gameObject);
                whistlesContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemShopSlotController>().UpdateInventorySelected();

            }
            if (travelPassToggle.isOn)
            {
                whistlesContainer.SetActive(false);
                luresContainer.SetActive(false);
                incenseContainer.SetActive(false);
                travelPassContainer.SetActive(true);
                travelPassContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemShopSlotController>().Awoke();
                eventSystem.SetSelectedGameObject(travelPassContainer.transform.GetChild(0).GetChild(0).GetChild(0).gameObject);
               // travelPassContainer.transform.parent.parent.gameObject.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
                travelPassContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemShopSlotController>().UpdateInventorySelected();

            }
        }
    }
}
