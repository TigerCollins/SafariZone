using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{

    public PlayerData playerData;
    public bl_SceneLoader sceneLoader;
    public CanvasGroup splashScreenCanvas;
    public CanvasGroup communityMessageCanvas;
    public bool showCommmunityMessage;
    public Button communityMessageButton;
    public float buttonTimer;
    public AudioClip continueButtonClip;
    public LocalAudioManager localAudioManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Splash Screen")
        {
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {

                playerData.controllerInput = false;
                if(showCommmunityMessage == false)
                {
                    localAudioManager.audioManager.OneShotMenuClick(continueButtonClip);
                    StartCoroutine(WaitAndPrint(1f));
                    
                }

                else
                {
                    splashScreenCanvas.alpha = 0;
                    communityMessageCanvas.alpha = 1;

                }
                //
            }
        }

        if(communityMessageCanvas.alpha ==1)
        {
            buttonTimer -= Time.deltaTime;
            if(buttonTimer <= 0)
            {
                communityMessageButton.interactable = true;
            }
        }
        // if()
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        if (playerData.firstTime == true)
        {
            PlayerPrefs.SetInt("AreaID", 0);
            sceneLoader.LoadLevel("GameWorld");

        }

        else
        {
            sceneLoader.LoadLevel("Main Menu");
        }
    }

    public void ChangeBoolFirstTime()
    {
        playerData.firstTime = true;
    }
}
