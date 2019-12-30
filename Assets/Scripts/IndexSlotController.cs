using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndexSlotController : MonoBehaviour
{
    public Creature creature;
    public Text flavourText;
    public Image creatureImageMain;
    public Image habitatImage;
    public Text creatureNameMainText;
    public Text totalCaughtText;
    public Text firstCaughtText;
    public Text heaviestCaughtText;
    public Text valueText;
    public Sprite questionMark;
    public Text rarityText;

    public void Start()
    {
        UpdateInfo();

        flavourText = GameObject.Find("Flavour text").GetComponent<Text>();
        creatureImageMain = GameObject.Find("Creature Image").GetComponent<Image>();
        habitatImage = GameObject.Find("Habitat Symbol").GetComponent<Image>();
        creatureNameMainText = GameObject.Find("Creature Name").GetComponent<Text>();
        totalCaughtText = GameObject.Find("Total Caught Num").GetComponent<Text>();
        firstCaughtText = GameObject.Find("Date Caught text").GetComponent<Text>();
        heaviestCaughtText = GameObject.Find("Heaviest Caught text").GetComponent<Text>();
        valueText = GameObject.Find("Value Num").GetComponent<Text>();
        rarityText = GameObject.Find("Rarity").GetComponent<Text>();
    }

    public void UpdateInfo()
    {
        Text displayNameText = transform.Find("Species Name").GetComponent<Text>();
        Image displayImage = transform.Find("Creature Sprite").GetComponent<Image>();
        Text beenCaught = transform.Find("Species Capture").GetComponent<Text>();

        if (creature && creature.previouslyCaptured == true)
        {
            displayNameText.text = creature.creatureName;
            displayImage.sprite = creature.creatureIcon;
            beenCaught.text = "Captured";
        }

        if (creature && creature.previouslyCaptured == false)
        {
            displayNameText.text = creature.creatureName;
            displayImage.sprite = questionMark;
            beenCaught.text = "";
        }

        if (creature == null)
        {
            displayNameText.text = "???";
            displayImage.sprite = null;
            beenCaught.text = "";
        }
    }

    public void Use()
    {

    }

    public void UpdateInfoSelected()
    {
        if (creature)
        {
            flavourText.text = creature.flavourText;
            creatureImageMain.sprite = creature.creatureIcon;
            habitatImage.sprite = creature.habitatIcon;
            creatureNameMainText.text = creature.name;
            totalCaughtText.text = creature.totalCaught.ToString();
            firstCaughtText.text = creature.dateFirstCaught;
            heaviestCaughtText.text = creature.heaviestCaught.ToString();
            valueText.text = creature.price.ToString();
            rarityText.text = creature.creatureRarity.ToString();
        }

        if(creature && creature.previouslyCaptured == false)
        {
            flavourText.text = "???";
            creatureImageMain.sprite = questionMark;
            habitatImage.sprite = creature.habitatIcon;
            creatureNameMainText.text = creature.name;
            totalCaughtText.text = creature.totalCaught.ToString();
            firstCaughtText.text = "N/A";
            heaviestCaughtText.text = creature.heaviestCaught.ToString();
            valueText.text = "???";
            rarityText.text = creature.creatureRarity.ToString();
        }
    }


}

