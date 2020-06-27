using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public PlayerData playerData;
    public bool distanceTravelled;
    public bool creaturesCaught;
    public bool sessionTime;
    public bool runsCompleted;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        if(distanceTravelled)
        {
            text.text = playerData.totalDistanceTravelled.ToString("f0") + "m";
        }

        else if(creaturesCaught)
        {
            text.text = playerData.creaturesCaught.ToString();
        }

        else if(sessionTime)
        {
            text.text = (playerData.playTime / 60).ToString("f2") + " minutes";
        }

        else if (runsCompleted)
        {
            text.text = playerData.runsCompleted.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
