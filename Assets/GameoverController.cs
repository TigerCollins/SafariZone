using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverController : MonoBehaviour
{
    public bool gameOver = false;
    private int gameoverCount = 0;
    public CanvasGroup canvasGroup;

    [Header("Script Links")]
    public GameController gameController;
    public PlayerController playerController;
    public Index indexScript;
    public Inventory inventoryScript;

    [Header("Base Variables")]
    public float timeCount;
    public string timeCountString;
    public int peopleSpokenTo;
    public string peopleSpokenToString;
    public int chestsOpened;
    public string chestsOpenedString;

    //public Li

    [Header("UI Variables")]
    public Text timeText;
    public Text spokenToText;
    public Text chestsOpenedText;
    public Text coinsText;

    [Header("Item Variables")]
    public List<Item> items;
    public List<Item> itemsObtained;
    public List<Item> itemsUsed;
    public GameObject itemIconPrefab;
    public GameObject itemObtainedLayoutGroup;
    public GameObject itemUsedLayoutGroup;

    [Header("Creature Variables")]
    public List<Creature> creatures;
    public List<Creature> creaturesObtained;
    public GameObject creatureIconPrefab;
    public GameObject creatureIconExtraPrefab;
    public GameObject creatureCaughtLayoutGroup;

    public void Start()
    {
        creatures = indexScript.creatureList;
        canvasGroup.alpha = 0;
    }


    public void UpdateTimeText()
    {
        //Set time variables
         float timeMinutes = Mathf.Floor(timeCount / 60);
        float timeSeconds = Mathf.RoundToInt(timeCount % 60);

        //Update Text
        timeCountString = timeMinutes.ToString("00") + ":" + timeSeconds.ToString("00") + " Minutes";
        timeText.text = timeCountString;
    }

    public void UpdateCreatureIcons()
    {
      
        for (int i = 0; i < creaturesObtained.Count; i++)
        {
            if (creaturesObtained.Count == 0)
            {
                Instantiate(creatureIconExtraPrefab, creatureCaughtLayoutGroup.transform);
            }
            if (creatureCaughtLayoutGroup.transform.childCount < 4)
            {
                Instantiate(creatureIconPrefab, creatureCaughtLayoutGroup.transform);
            }

            if(creatureCaughtLayoutGroup.transform.childCount == 4)
            {
                Instantiate(creatureIconExtraPrefab, creatureCaughtLayoutGroup.transform);
            }
        }
        
    }

    public void UpdateItemsObtainedIcons()
    {
        if(itemsObtained.Count == 0)
        {
            Instantiate(creatureIconExtraPrefab, itemObtainedLayoutGroup.transform);
        }
        for (int i = 0; i < itemsObtained.Count; i++)
        {
            if (itemObtainedLayoutGroup.transform.childCount < 4)
            {
                Instantiate(itemIconPrefab, itemObtainedLayoutGroup.transform);
            }

            if (itemObtainedLayoutGroup.transform.childCount == 4)
            {
                Instantiate(creatureIconExtraPrefab, itemObtainedLayoutGroup.transform);
            }
        }


    }

    public void UpdateCoins()
    {
        coinsText.text = gameController.currencyScript.wallet.ToString();
    }

    public void UpdateItemsUsedIcons()
    {
        if (itemsUsed.Count == 0)
        {
             Instantiate(creatureIconExtraPrefab, itemUsedLayoutGroup.transform);
        }
        for (int i = 0; i < itemsUsed.Count; i++)
        {
            if (itemUsedLayoutGroup.transform.childCount < 4)
            {
                Instantiate(itemIconPrefab, itemUsedLayoutGroup.transform);
            }

            if (itemUsedLayoutGroup.transform.childCount == 4)
            {
                Instantiate(creatureIconExtraPrefab, itemUsedLayoutGroup.transform);
            }
        }

    }

    public void CaughtCreature(Creature newCreature)
    {

                creaturesObtained.Add(newCreature);
                // return;

    }

    public void ItemUsed(Item newItem)
    {
        itemsUsed.Add(newItem);
    }
      public void ItemObtained(Item newItem)
    {
        itemsObtained.Add(newItem);
    }

    public void UpdatePeopleSpokenTo()
    {
        peopleSpokenTo = playerController.peopleSpokenTo;
        peopleSpokenToString = peopleSpokenTo + " People";
        spokenToText.text = peopleSpokenToString;
    }

    public void UpdateChestsOpened()
    {
        chestsOpened = playerController.chestsOpened;
        chestsOpenedString = chestsOpened + " Opened";
        chestsOpenedText.text = chestsOpenedString;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver == false)
        {
            UpdateTime();
        }


        gameOver = gameController.gameOver;

        if (gameOver == true && gameoverCount <= 0)
        {
            canvasGroup.alpha = Time.deltaTime;
            UpdateTimeText(); 
            UpdatePeopleSpokenTo(); 
            UpdateChestsOpened();
            UpdateCreatureIcons();
            UpdateItemsUsedIcons();
            UpdateItemsObtainedIcons();
            UpdateCoins();
            gameoverCount += 1;

        }
    }

    public void UpdateTime()
    {
        if(gameOver==false)
        {
            timeCount += Time.unscaledDeltaTime;
        }
    }
}
