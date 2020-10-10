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

    [Header("Inputs - Keyboard")]
    public KeyCode walkHorizontalLeftKeyboard;
    public KeyCode walkHorizontalLeftKeyboardAlt;
    public KeyCode walkHorizontalRightKeyboard;
    public KeyCode walkHorizontalRightKeyboardAlt;
    public KeyCode walkVerticalUpKeyboard;
    public KeyCode walkVerticalUpKeyboardAlt;
    public KeyCode walkVerticalDownKeyboard;
    public KeyCode walkVerticalDownKeyboardAlt;
    public KeyCode submitKeyboard;
    public KeyCode interactKeyboard;
    public KeyCode pauseKeyboard;
    public bool quicktimeUseMouse;
    public KeyCode quicktimeKeyboard1;
    public KeyCode quicktimeKeyboard2;
    public KeyCode quicktimeKeyboard3;
    public KeyCode quicktimeKeyboard4;
    public KeyCode quicktimeKeyboard5;


    [Header("Inputs - Gamepad")]
    public KeyCode submitGamepad;
    public KeyCode interactGamepad;
    public KeyCode pauseGamepad;
    public KeyCode quicktimeGamepad1;
    public KeyCode quicktimeGamepad2;
    public KeyCode quicktimeGamepad3;
    public KeyCode quicktimeGamepad4;
    public KeyCode quicktimeGamepad5;



}
