using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BackpackController : MonoBehaviour
{
    [Header("Toggle Changer / Pages")]
    public Toggle allItems;
    public Toggle lureToggle;
    public Toggle incenseToggle;
    public Toggle whistlesToggle;
    public Inventory inventory;

    public GameObject allItemsContainer;
    public GameObject luresContainer;
    public GameObject incenseContainer;
    public GameObject whistlesContainer;
    public GameObject emptyTextPrefab;

    public CanvasGroup allItemsCanvas;
    public CanvasGroup luresCanvas;
    public CanvasGroup incenseCanvas;
    public CanvasGroup whistlesCanvas;

    private int invisible = 0;
    private int visible = 1;

    // Start is called before the first frame update
    void Start()
    {
      // CategoryChanger();
    }

    private void Awake()
    {
        // CategoryChanger();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CategoryChanger()
    {
        if (allItems.isOn)
        {
            allItemsCanvas.alpha = visible;
            allItemsCanvas.interactable = true;
            allItemsCanvas.blocksRaycasts = true;
            //allItemsContainer.SetActive(true);
            luresCanvas.alpha = invisible;
            luresCanvas.interactable = false;
            luresCanvas.blocksRaycasts = false;
            //luresContainer.SetActive(false);
            incenseCanvas.alpha = invisible;
            incenseCanvas.interactable = false;
            incenseCanvas.blocksRaycasts = false;
            //incenseContainer.SetActive(false);
            whistlesCanvas.alpha = invisible;
            whistlesCanvas.interactable = false;
            whistlesCanvas.blocksRaycasts = false;
            //whistlesContainer.SetActive(false);
            if (allItemsContainer.transform.GetChild(0).GetChild(0).GetChild(0) != null)
            {
                allItemsContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<InventorySlotController>().UpdateInventorySelected();
            }

            //inventory.inventoryContainer.transform.GetChild(0).transform.GetChild(5).GetComponent<Button>().Select();
             if (inventory.inventoryContainer.transform.GetChild(0).transform.GetChild(5) != null)
            {
              //   inventory.inventoryContainer.transform.GetChild(0).transform.GetChild(5).GetComponent<Button>().onClick.Invoke();
              
             }
        }

        if (lureToggle.isOn)
        {
            allItemsCanvas.alpha = invisible;
            allItemsCanvas.interactable = false;
            allItemsCanvas.blocksRaycasts = false;
            //allItemsContainer.SetActive(true);
            luresCanvas.alpha = visible;
            luresCanvas.interactable = true;
            luresCanvas.blocksRaycasts = true;
            //luresContainer.SetActive(false);
            incenseCanvas.alpha = invisible;
            incenseCanvas.interactable = false;
            incenseCanvas.blocksRaycasts = false;
            //incenseContainer.SetActive(false);
            whistlesCanvas.alpha = invisible;
            whistlesCanvas.interactable = false;
            whistlesCanvas.blocksRaycasts = false;
            //whistlesContainer.SetActive(false);


            if (luresContainer.transform.GetChild(0).GetChild(0).transform.childCount > 0)
            {
                if (luresContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<InventorySlotController>() != null)
                {
                    luresContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<InventorySlotController>().UpdateInventorySelected();
                }
            }

            else
            {
                Instantiate(emptyTextPrefab, luresContainer.transform.GetChild(0).GetChild(0));
                luresContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<InventorySlotController>().UpdateInventorySelected();
            }

              if (inventory.lureListContainer.transform.GetChild(0).transform.GetChild(5) != null)
              {
                // inventory.lureListContainer.transform.GetChild(0).transform.GetChild(5).GetComponent<Button>().onClick.Invoke();
              }
        }

        if (incenseToggle.isOn)
        {
            allItemsCanvas.alpha = invisible;
            allItemsCanvas.interactable = false;
            allItemsCanvas.blocksRaycasts = false;
            //allItemsContainer.SetActive(true);
            luresCanvas.alpha = invisible;
            luresCanvas.interactable = false;
            luresCanvas.blocksRaycasts = false;
            //luresContainer.SetActive(false);
            incenseCanvas.alpha = visible;
            incenseCanvas.interactable = true;
            incenseCanvas.blocksRaycasts = true;
            //incenseContainer.SetActive(false);
            whistlesCanvas.alpha = invisible;
            whistlesCanvas.interactable = false;
            whistlesCanvas.blocksRaycasts = false;
            //whistlesContainer.SetActive(false);


            if (incenseContainer.transform.GetChild(0).GetChild(0).transform.childCount > 0)
            {
                if(incenseContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<InventorySlotController>() != null)
                {
                    incenseContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<InventorySlotController>().UpdateInventorySelected();
                }

            }

              else
            {
                Instantiate(emptyTextPrefab, incenseContainer.transform.GetChild(0).GetChild(0));
                incenseContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<InventorySlotController>().UpdateInventorySelected();
            }
            

              if (inventory.incenseListContainer.transform.GetChild(0).transform.GetChild(5) != null)
              {
               /// inventory.incenseListContainer.transform.GetChild(0).transform.GetChild(5).GetComponent<Button>().onClick.Invoke();
             }
        }

        if (whistlesToggle.isOn)
        {
            allItemsCanvas.alpha = invisible;
            allItemsCanvas.interactable = false;
            allItemsCanvas.blocksRaycasts = false;
            //allItemsContainer.SetActive(true);
            luresCanvas.alpha = invisible;
            luresCanvas.interactable = false;
            luresCanvas.blocksRaycasts = false;
            //luresContainer.SetActive(false);
            incenseCanvas.alpha = invisible;
            incenseCanvas.interactable = false;
            incenseCanvas.blocksRaycasts = false;
            //incenseContainer.SetActive(false);
            whistlesCanvas.alpha = visible;
            whistlesCanvas.interactable = true;
            whistlesCanvas.blocksRaycasts = true;
            //whistlesContainer.SetActive(false);

            if (whistlesContainer.transform.GetChild(0).GetChild(0).transform.childCount > 0 )
            {
                if (whistlesContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<InventorySlotController>() != null)
                {
                    whistlesContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<InventorySlotController>().UpdateInventorySelected();
                }
            }


            else
            {
                Instantiate(emptyTextPrefab, whistlesContainer.transform.GetChild(0).GetChild(0));
                whistlesContainer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<InventorySlotController>().UpdateInventorySelected();
            }
             if(inventory.whistlesListContainer.transform.GetChild(0).transform.GetChild(5) != null)
             {
               //  inventory.whistlesListContainer.transform.GetChild(0).transform.GetChild(5).GetComponent<Button>().onClick.Invoke();
            }

        }
    }
}
