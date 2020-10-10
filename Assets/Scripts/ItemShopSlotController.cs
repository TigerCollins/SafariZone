using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
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
    public Currency currencyScript;
    public Item mostRecent;
    public UIManager interfaceManager;

    public void Awake()
    {
        currencyScript = GameObject.Find("ScriptController").GetComponent<Currency>();
        flavourText = GameObject.Find("Item Disc.").GetComponent<Text>();
        itemNameExtra = GameObject.Find("Item Display Name").GetComponent<Text>();
        itemShopScript = GameObject.Find("ScriptController").GetComponent<ItemShop>();
        inInventory = GameObject.Find("num").GetComponent<Text>();
        itemSprite = GameObject.Find("CreatureImage").GetComponent<Image>();
       // currencyScript.textDisplay = GameObject.Find("currencyScript.textDisplayText").GetComponent<Text>();
        Awoke();
        currencyScript.ScriptAwoke();
        //pdateInfo();
        interfaceManager = GameObject.Find("ScriptController").GetComponent<UIManager>();
    }

    public void Awoke()
    {
        currencyScript = GameObject.Find("ScriptController").GetComponent<Currency>();
        flavourText = GameObject.Find("Item Disc.").GetComponent<Text>();
        itemNameExtra = GameObject.Find("Item Display Name").GetComponent<Text>();
        itemShopScript = GameObject.Find("ScriptController").GetComponent<ItemShop>();
        inInventory = GameObject.Find("num").GetComponent<Text>();
        itemSprite = GameObject.Find("CreatureImage").GetComponent<Image>();
      //  currencyScript.textDisplay = GameObject.Find("currencyScript.textDisplayText").GetComponent<Text>();
    }

    public void Hovered()
    {
        interfaceManager.SnapTo(gameObject.GetComponent<RectTransform>());
    }

    public void FixedUpdate()
    {
        //UpdateInventorySelected();
    }

    public void UpdateInfo()
    {
        currencyScript = GameObject.Find("ScriptController").GetComponent<Currency>();
        Text priceText = transform.Find("ItemPrice").GetComponent<Text>(); 
        Text displayNameText = transform.Find("ItemName").GetComponent<Text>();
        Image displayImage = transform.Find("ItemImage").GetComponent<Image>();

        if (item != null)
        {
            mostRecent = item;
            if (item.travelPass == true)
            {
                if (!mostRecent.isOwned)
                {
                    priceText.text = mostRecent.price.ToString();
                    displayNameText.text = mostRecent.itemName;
                    displayImage.sprite = mostRecent.icon;
                    currencyScript.textDisplay.text = currencyScript.wallet.ToString();
                    mostRecent.isOwned = false;
                   currencyScript.UpdatePlayerData();
                }

                else if(mostRecent.isOwned && displayNameText.text != mostRecent.itemName)
                {
                    priceText.text = "---";
                    displayNameText.text = mostRecent.itemName;
                    displayImage.sprite = mostRecent.icon;
                    currencyScript.textDisplay.text = currencyScript.wallet.ToString();
                    mostRecent.isOwned = true;
                     currencyScript.UpdatePlayerData();
                }

                else if(mostRecent.isOwned && displayNameText.text == mostRecent.itemName)
                {
                    priceText.text = "---";
                    displayNameText.text = mostRecent.itemName;
                    displayImage.sprite = mostRecent.icon;
                    currencyScript.textDisplay.text = currencyScript.wallet.ToString();
                    mostRecent.isOwned = true;
                    currencyScript.UpdatePlayerData();
                }
            }

            else
            {
                priceText.text = mostRecent.price.ToString();
                displayNameText.text = mostRecent.itemName;
                displayImage.sprite = mostRecent.icon;
                mostRecent.isOwned = true;
                currencyScript.UpdatePlayerData();
                currencyScript.textDisplay.text = currencyScript.wallet.ToString();
            }


          //  currencyScript.textDisplay.text = currencyScript.wallet.ToString();
        }

        else
        {
            priceText.text = "";
            displayNameText.text = "";
            displayImage.sprite = null;
          //  currencyScript.UpdatePlayerData();
            // itemShopScript.currencyScript.textDisplay.text = itemShopScript.currencyScript.wallet.ToString();
        }

    }

    public void Update()
    {
        currencyScript.textDisplay.text = currencyScript.wallet.ToString();
        //UpdateInventorySelected();
    }

    public void Use()
    {
        UpdateInventorySelected();
        if (itemShopScript != null)
        {
            itemShopScript.lastItem = gameObject;
        }
        
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
        if (item != null)
        {
            //TRAVEL PASS
            if(item.travelPass == true)
            {
                if(item.quantity > 0)
                {
                    item.isOwned = true;
                }

                else
                {
                    item.isOwned = false;
                }

                if(item.isOwned)
                {
                    itemNameExtra.text = item.itemName;
                    flavourText.text = item.flavourText;
                    inInventory.text = "x" + item.quantity.ToString();
                    itemSprite.sprite = item.icon;
                    currencyScript.UpdatePlayerData();
                }

                else
                {
                    itemNameExtra.text = item.itemName;
                    flavourText.text = item.flavourText;
                    inInventory.text = "x" + item.quantity.ToString();
                    itemSprite.sprite = item.icon;
                    currencyScript.UpdatePlayerData();
                }
               
            }
            //NORMAL ITEM
            else
            {
                itemNameExtra.text = item.itemName;
                flavourText.text = item.flavourText;
                inInventory.text = "x" + item.quantity.ToString();
                itemSprite.sprite = item.icon;
                currencyScript.UpdatePlayerData();
            }

           
        }

        else
        {
            itemNameExtra.text = "";
            flavourText.text = "Select an item";
            inInventory.text = "x" + 0;
            itemSprite.sprite = unknownSprite;
            currencyScript.UpdatePlayerData();
        }
    }


}

