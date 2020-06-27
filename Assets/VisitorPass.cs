using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisitorPass : MonoBehaviour
{
    public PlayerData playerData;
    public InputField playerNameField;
    public Toggle isMale;
    public Toggle isFemale;

    // Start is called before the first frame update
    void Start()
    {
        playerNameField.text = playerData.playerName;

        if(playerData.maleGender)
        {
            isMale.isOn = true;
        }

        else
        {
            isFemale.isOn = true;
        }
    }

    // Update is called once per frame
    public void NewPlayerName()
    {
        playerData.playerName = playerNameField.text;
    }

    public void IsMale()
    {
        playerData.maleGender = true;
    }

    public void IsFemale()
    {
        playerData.maleGender = false;
    }
}
