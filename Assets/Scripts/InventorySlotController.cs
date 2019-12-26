using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{
    public Item item;

    public Text quantityText;
    public Text flavourText;
    public Text itemNameExtra;

    public void Start()
    {
        UpdateInfo();
        quantityText = GameObject.Find("num").GetComponent<Text>();
        flavourText = GameObject.Find("Item Disc.").GetComponent<Text>();
        itemNameExtra = GameObject.Find("Item Display Name").GetComponent<Text>();

    }

    public void UpdateInfo()
    {
        
        Text displayNameText = transform.Find("ItemName").GetComponent<Text>();
        Image displayImage = transform.Find("ItemImage").GetComponent<Image>();

        if (item)
        {
            displayNameText.text = item.itemName;
            displayImage.sprite = item.icon;
        }

        else
        {
            displayNameText.text = "";
            displayImage.sprite = null; 
        }
    }

    public void Use()
    {
        if(item)
        {
            Debug.Log("You clicked " + item.itemName);
        }
    }

    public void UpdateInventorySelected()
    {
        if(item)
        {
            itemNameExtra.text = item.itemName;
            quantityText.text = "x" + item.quantity;
            flavourText.text = item.flavourText;
        }
        
        else
        {
            itemNameExtra.text = "";
            quantityText.text = "";
            flavourText.text = "Select an item";
        }
    }
}
