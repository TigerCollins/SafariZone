using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTracker : MonoBehaviour
{

    public PlayerData playerProfile;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToTalkCount()
    {
        playerProfile.peopleSpokenTo++;
    }

    public void AddToHuntCatchCount()
    {
        playerProfile.creaturesCaughtViaHunting++;
    }
}
