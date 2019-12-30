using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopSlotController : MonoBehaviour
{
    public ItemShop itemShopScript;

    public Item item;
    public Text flavourText;
    public Text itemNameExtra;
    public Text inInventory;

    public void Start()
    {
        UpdateInfo();
      
        flavourText = GameObject.Find("Item Disc.").GetComponent<Text>();
        itemNameExtra = GameObject.Find("Item Display Name").GetComponent<Text>();
        itemShopScript = GameObject.Find("ScriptController").GetComponent<ItemShop>();
        inInventory = GameObject.Find("num").GetComponent<Text>();

    }

    public void UpdateInfo()
    {
        Text priceText = transform.Find("ItemPrice").GetComponent<Text>(); 
        Text displayNameText = transform.Find("ItemName").GetComponent<Text>();
        Image displayImage = transform.Find("ItemImage").GetComponent<Image>();

        if (item)
        {
            priceText.text = item.price.ToString();
            displayNameText.text = item.itemName;
            displayImage.sprite = item.icon;
        }

        else
        {
            priceText.text = "";
            displayNameText.text = "";
            displayImage.sprite = null;
        }
    }

    public void Use()
    {
        if (item)
        {
            itemShopScript.selectedItem = item;
            Debug.Log("You selected " + item.itemName);
        }
    }

    public void UpdateInventorySelected()
    {
        if (item)
        {
            itemNameExtra.text = item.itemName;
            flavourText.text = item.flavourText;
            inInventory.text = "x" + item.quantity.ToString();
        }

        else
        {
            itemNameExtra.text = "";
            flavourText.text = "Select an item";
            inInventory.text = "x" + 0;
        }
    }


}

