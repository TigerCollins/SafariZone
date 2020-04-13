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
    public Image itemSprite;
    public Sprite unknownSprite;

    public void Awake()
    {
        UpdateInfo();
      
        flavourText = GameObject.Find("Item Disc.").GetComponent<Text>();
        itemNameExtra = GameObject.Find("Item Display Name").GetComponent<Text>();
        itemShopScript = GameObject.Find("ScriptController").GetComponent<ItemShop>();
        inInventory = GameObject.Find("num").GetComponent<Text>();
        itemSprite = GameObject.Find("CreatureImage").GetComponent<Image>();

    }

    public void UpdateInfo()
    {
        Text priceText = transform.Find("ItemPrice").GetComponent<Text>(); 
        Text displayNameText = transform.Find("ItemName").GetComponent<Text>();
        Image displayImage = transform.Find("ItemImage").GetComponent<Image>();

        if (item != null)
        {
            if (item.travelPass == true)
            {
                if (item.isOwned)
                {
                    priceText.text = "0";
                    displayNameText.text = item.itemName;
                    displayImage.sprite = item.icon;
                }

                else
                {
                    priceText.text = item.price.ToString();
                    displayNameText.text = item.itemName;
                    displayImage.sprite = item.icon;
                }
            }

            else
            {
                priceText.text = item.price.ToString();
                displayNameText.text = item.itemName;
                displayImage.sprite = item.icon;
            }


            
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
            if(item.itemTypes == Item.ItemTypes.Lure && item.canHaveMultiple == false)
            {
                itemShopScript.quantityInputfield.gameObject.SetActive(false);
            }
            else
            {
                itemShopScript.quantityInputfield.gameObject.SetActive(true);
            }
        }
    }

    public void UpdateInventorySelected()
    {
        if (item)
        {
            //TRAVEL PASS
            if(item.travelPass == true)
            {
                if(item.isOwned)
                {
                    itemNameExtra.text = item.itemName;
                    flavourText.text = item.flavourText;
                    inInventory.text = "x" + item.quantity.ToString();
                    itemSprite.sprite = item.icon;
                }

                else
                {
                    itemNameExtra.text = item.itemName;
                    flavourText.text = item.flavourText;
                    inInventory.text = "x" + item.quantity.ToString();
                    itemSprite.sprite = item.icon;
                }
               
            }
            //NORMAL ITEM
            else
            {
                itemNameExtra.text = item.itemName;
                flavourText.text = item.flavourText;
                inInventory.text = "x" + item.quantity.ToString();
                itemSprite.sprite = item.icon;
            }

           
        }

        else
        {
            itemNameExtra.text = "";
            flavourText.text = "Select an item";
            inInventory.text = "x" + 0;
            itemSprite.sprite = unknownSprite;
        }
    }


}

