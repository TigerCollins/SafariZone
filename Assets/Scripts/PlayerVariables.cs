using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TextureResolutionFactor { low, Medium, High }
public enum AntiAliasingFactor { None, MSAAx2, MSAAx4, MSAAx8, MSAAx16 }
public enum AmbientOcclusionFactor { None, low, Medium, High }

[CreateAssetMenu(fileName = "new playerprofile", menuName = "Player Profile")]
[System.Serializable]


public class PlayerVariables : ScriptableObject
{
    public bool firstTime;
    public bool controllerInput;
    public string latestCommunityMessage;
    public string latestCommunityDate;

    [Header("Player Info")]
    public string playerName;
    public int wallet;
    public float playTime;
    public int runsCompleted;
    public float totalDistanceTravelled;    
    public int creaturesCaught;    
    public int timePlayed;
    public bool maleGender;

    [Header("Just For Achievements")]
    public int creaturesCaughtViaHunting;
    public bool visitedJungle;
    public int peopleSpokenTo;
    public int moneySpentToDate;

    [Header("Spawn Areas")]
    public bool guidanceIsle;
    public bool egressCave;
    public bool kebarVillage;
    public bool parharVillage;
    public bool AzaleaForest;
    public bool lakePerano;

    [Header("Player Options")]
    //Volume
    public float masterVolume;
    public float masterVolumeCore;
    public float gameVolume;
    public float gameVolumeCore;
    public float menuVolume;
    public float menuVolumeCore;
    public float musicVolume;
    public float musicVolumeCore;
    //Visual
    public bool chromaticAberration;
    public bool filmGrain;
    public bool vignette;
    public bool dynamicResolution;
    public bool anistropicFiltering;
    public bool VSync;
    public TextureResolutionFactor textureResolution;
    public AntiAliasingFactor antiAliasing;
    public bool ambientOcclusion;
    public bool fullScreen;
    public int resolutionWidth;
    public int resolutionHeight;




}
