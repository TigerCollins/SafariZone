using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    public PlayerData playerData;
    public Button spawnpoint1;
    public Button spawnpoint2;
    public Button spawnpoint3;
    public Button spawnpoint4;
    public Button spawnpoint5;
    public Button spawnpoint6;

    public Image padlock1;
    public Image padlock2;
    public Image padlock3;
    public Image padlock4;
    public Image padlock5;
    public Image padlock6;

    public Toggle[] toggle;

    private int areaID;

    // Start is called before the first frame update
    void Awake()
    {
        if (playerData.guidanceIsle == true)
        { 
            toggle[0].interactable = true;
            toggle[0].isOn = true;
            spawnpoint1.interactable = true;
            padlock1.gameObject.SetActive(false);
        }

        if (playerData.egressCave == true)
        {
            toggle[1].interactable = true;
            toggle[1].isOn = true;
            spawnpoint2.interactable = true;
            padlock2.gameObject.SetActive(false);
        }

        if (playerData.kebarVillage == true)
        {
            toggle[2].interactable = true;
            toggle[2].isOn = true;
            spawnpoint3.interactable = true;
            padlock3.gameObject.SetActive(false);
        }

        if (playerData.parharVillage == true)
        {
            toggle[3].interactable = true;
            toggle[3].isOn = true;
            spawnpoint4.interactable = true;
            padlock4.gameObject.SetActive(false);
        }

        if (playerData.AzaleaForest == true)
        {
            toggle[4].interactable = true;
            toggle[4].isOn = true;
            spawnpoint5.interactable = true;
            padlock5.gameObject.SetActive(false);
        }

        if (playerData.lakePerano == true)
        {
            toggle[5].interactable = true;
            toggle[5].isOn = true;
            spawnpoint6.interactable = true;
            padlock6.gameObject.SetActive(false);
        }
    }

    public void ChangeSpawnpoint(int value)
    {
        PlayerPrefs.SetInt("AreaID", value);
    }

    public void ChangeSpawnPointMenu(int value)
    {
        toggle[value].isOn = true;
    }
}
