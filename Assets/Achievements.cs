using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    [SerializeField] private PlayerData playerProfile;
    
    [SerializeField] private Image[] achievementIcons;
    public Creature[] creatureList;
    public Item[] itemList;
    public Color unlockedColour;
    public Color lockedColour;
    public Image miniIcon;

    public Text achievementDescription;
    public Text achievementCounter;
    private int achievementCount = 0;
    public int achievementID;

    [Header("Achievement List")]
    [SerializeField] private bool bigSpender;
    [SerializeField] private bool completionist;
    [SerializeField] private bool greatWall;
    [SerializeField] private bool knowItAll;
    [SerializeField] private bool lecturer;
    [SerializeField] private bool legendaryHunter;
    [SerializeField] private bool supportGroup;
    [SerializeField] private bool trainingWheels;
    [SerializeField] private bool welcomeToTheJungle;
    [SerializeField] private bool zaWarudo;
    public bool[] achievementBool;

    // Start is called before the first frame update
    void Start()
    {
        ActivateAchievements();
        UpdateAchievement();
        achievementIcons[0].gameObject.GetComponent<Toggle>().isOn = true;
    }

    public void UpdateAchievementID(int ID)
    {
        //Ticks first then goes to locked item
        achievementID = ID;
    }

    public void UpdateLockedDescription(string Description)
    {
        if(achievementBool[achievementID] == false)
        {
            achievementDescription.text = Description;
            miniIcon.color = lockedColour;
            miniIcon.sprite = achievementIcons[achievementID].sprite;
        }

    }

    public void UpdateUnlockedDescription(string Description)
    {
        if (achievementBool[achievementID] == true)
        {
            achievementDescription.text = Description;
            miniIcon.color = unlockedColour;
            miniIcon.sprite = achievementIcons[achievementID].sprite;
        }
    }

    void UpdateAchievement()
    {
        if(bigSpender == true)
        {
            achievementIcons[0].color = unlockedColour;
            achievementBool[0] = true;
            achievementCount++;
        }

        if (greatWall == true)
        {
            achievementIcons[1].color = unlockedColour;
            achievementBool[1] = true;
            achievementCount++;
        }

        if (knowItAll == true)
        {
            achievementIcons[2].color = unlockedColour;
            achievementBool[2] = true;
            achievementCount++;
        }

        if (lecturer == true)
        {
            achievementIcons[3].color = unlockedColour;
            achievementBool[3] = true;
            achievementCount++;
        }

        if (legendaryHunter == true)
        {
            achievementIcons[4].color = unlockedColour;
            achievementBool[4] = true;
            achievementCount++;
        }

        if (supportGroup == true)
        {
            achievementIcons[5].color = unlockedColour;
            achievementBool[5] = true;
            achievementCount++;
        }

        if (trainingWheels == true)
        {
            achievementIcons[6].color = unlockedColour;
            achievementBool[6] = true;
            achievementCount++;
        }

        if (welcomeToTheJungle == true)
        {
            achievementIcons[7].color = unlockedColour;
            achievementBool[7] = true;
            achievementCount++;
        }

        if (zaWarudo == true)
        {
            achievementIcons[8].color = unlockedColour;
            achievementBool[8] = true;
            achievementCount++;
        }

        if (completionist == true)
        {
            achievementIcons[9].color = unlockedColour;
            achievementBool[9] = true;
            achievementCount++;
        }

        achievementCounter.text = achievementCount.ToString() + "/10";
    }

    void ActivateAchievements()
    {
        //Activates tutorial achievement
        if(!playerProfile.firstTime)
        {
            trainingWheels = true;
        }

        //Activates great wall achievement
        if(playerProfile.totalDistanceTravelled >= 42392f)
        {
            greatWall = true;
        }

        //Activates index completion achievement
        int creatureCaughtCount = 0;
        foreach (Creature creature in creatureList)
        {
            if(creature.previouslyCaptured == true)
            {
                creatureCaughtCount++;
            }
        }

        //Activates index completion achievement
        int itemsObtainedCount = 0;
        foreach (Item item in itemList)
        {
            if (item.amountOwnedLifetime >= 1)
            {
                itemsObtainedCount++;
            }
        }
        if (itemsObtainedCount == itemList.Length)
        {
            knowItAll = true;
        }

        //Activates hunting pin
        if(playerProfile.creaturesCaughtViaHunting >= 50)
        {
            legendaryHunter = true;
        }

        //Activates big spender
        if (playerProfile.moneySpentToDate >= 50000)
        {
            bigSpender = true;
        }

        //activates jungle pin
        if (playerProfile.visitedJungle)
        {
            welcomeToTheJungle = true;
        }

        //activates people spoken to pin
        if (playerProfile.peopleSpokenTo >= 60)
        {
            supportGroup = true;
        }

        //completes za warudo pin
        if(playerProfile.guidanceIsle == true && playerProfile.egressCave == true && playerProfile.kebarVillage == true && playerProfile.parharVillage == true && playerProfile.lakePerano)
        {
            zaWarudo = true;
        }

        //Activates 100% completion pin
        if (achievementCount >= 9)
        {
            completionist = true;
        }
    }
}
