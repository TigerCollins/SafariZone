using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTracker : MonoBehaviour
{
    public PlayerData playerData;
    public float playerTime;
    public int wallet;
    public int runsCompleted;
    public float totalDistanceTravelled;
    public int creaturesCaught;
    public int timesPlayed;


    public void Start()
    {
        playerTime = playerData.playTime;
        wallet = playerData.wallet;
        runsCompleted = playerData.runsCompleted;
        totalDistanceTravelled = playerData.totalDistanceTravelled;
        creaturesCaught = playerData.creaturesCaught;
        timesPlayed = playerData.timePlayed;
    }

    // Update is called once per frame
    void Update()
    {
        playerTime += Time.fixedDeltaTime;
        playerData.playTime = playerTime;
        playerData.totalDistanceTravelled = totalDistanceTravelled;
    }
}
