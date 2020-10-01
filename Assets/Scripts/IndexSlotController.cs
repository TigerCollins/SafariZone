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
    public GameObject beenCaught;

    public void Start()
    {
        UpdateInfo();

       
    }

    public void UpdateInfo()
    {
        Text displayNameText = transform.Find("Species Name").GetComponent<Text>();
        Image displayImage = transform.Find("Creature Sprite").GetComponent<Image>();

        if (creature && creature.previouslyCaptured == true)
        {
            displayNameText.text = creature.creatureName;
            displayImage.sprite = creature.creatureIcon;
            beenCaught.SetActive(true);
        }

        if (creature && creature.previouslyCaptured == false)
        {
            displayNameText.text = "Undiscovered";
            displayImage.sprite = questionMark;
            beenCaught.SetActive(false);
        }

        if (creature == null)
        {
            displayNameText.text = "???";
            displayImage.sprite = null;
            beenCaught.SetActive(false);
        }
    }

    public void Use()
    {

    }

    public void UpdateInfoSelected()
    {
        flavourText = GameObject.Find("Flavour text").GetComponent<Text>();
        creatureImageMain = GameObject.Find("Creature Image").GetComponent<Image>();
        habitatImage = GameObject.Find("Habitat Symbol").GetComponent<Image>();
        creatureNameMainText = GameObject.Find("Creature Name").GetComponent<Text>();
        totalCaughtText = GameObject.Find("Total Caught Num").GetComponent<Text>();
        firstCaughtText = GameObject.Find("Date Caught text").GetComponent<Text>();
        heaviestCaughtText = GameObject.Find("Heaviest Caught text").GetComponent<Text>();
        valueText = GameObject.Find("Value Num").GetComponent<Text>();
        rarityText = GameObject.Find("Rarity").GetComponent<Text>();
        if (creature && creature.previouslyCaptured == true)
        {
            flavourText.text = creature.flavourText;
            creatureImageMain.sprite = creature.creatureIcon;
            habitatImage.sprite = creature.habitatIcon;
            creatureNameMainText.text = creature.name;
            totalCaughtText.text = creature.totalCaught.ToString();
            firstCaughtText.text = creature.dateFirstCaught;
            heaviestCaughtText.text = creature.heaviestCaught.ToString("f2") + "kg";
            valueText.text = creature.price.ToString();
            rarityText.text = creature.creatureRarity.ToString();
        }

        if(creature && creature.previouslyCaptured == false)
        {
            creatureNameMainText.text = "Undiscovered";
            flavourText.text = "Capture this creature to index it and discover more details.";
            creatureImageMain.sprite = questionMark;
            habitatImage.sprite = creature.habitatIcon;
            //creatureNameMainText.text = creature.name;
            totalCaughtText.text = creature.totalCaught.ToString();
            firstCaughtText.text = "--/--/--";
            heaviestCaughtText.text = "--- kg";
            valueText.text = "---";
            rarityText.text = creature.creatureRarity.ToString();
        }
    }


}

