using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.InputSystem.Utilities;

using System.Collections.Generic;


public class PopMinigameButton : MonoBehaviour
{
    public PopMinigameController popMinigameController;
    public GameController gameController;
    public Text keyInputText;
    public string buttonInputRequired;
    private PlatformDetection platformDetection;
    public GameObject QTEIconHolder;

    [Header("Action")]
    public InputActionReference inputActionReference1;
    public InputActionReference inputActionReference2;
    public InputActionReference inputActionReference3;
    public InputActionReference inputActionReference4;
    public InputActionReference inputActionReference5;
    public InputActionReference inputControlActionReference1;
    public InputActionReference inputControlActionReference2;
    public InputActionReference inputControlActionReference3;
    public InputActionReference inputControlActionReference4;
    public InputActionReference inputControlActionReference5;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        SetQTE();
    }


    public void SetQTE()
    {
        int quicktimeInput;
        quicktimeInput = Random.Range(0, 4);
        if(gameController.playerData.quicktimeUseMouse == false)
        {
            QTEIconHolder.SetActive(true);
            if (platformDetection.controllerInput)
            {
                if(quicktimeInput == 0)
                {
                    buttonInputRequired = inputControlActionReference1.ToString();
                }
                if(quicktimeInput == 1)
                {
                    buttonInputRequired = inputControlActionReference2.ToString();
                }
                if(quicktimeInput == 2)
                {
                    buttonInputRequired = inputControlActionReference3.ToString();
                }
                if(quicktimeInput == 3)
                {
                    buttonInputRequired = inputControlActionReference4.ToString();
                }
                if(quicktimeInput == 4)
                {
                    buttonInputRequired = inputControlActionReference5.ToString();
                }

            }

            else
            {
                /*
                if (quicktimeInput == 0)
                {
                    buttonInputRequired = inputActionReference1.ToString();
                }
                if (quicktimeInput == 1)
                {
                    buttonInputRequired = inputActionReference2.ToString();
                }
                if (quicktimeInput == 2)
                {
                    buttonInputRequired = inputActionReference3.ToString();
                }
                if (quicktimeInput == 3)
                {
                    buttonInputRequired = inputActionReference4.ToString();
                }
                if (quicktimeInput == 4)
                {
                    buttonInputRequired = inputActionReference5.ToString();
                }
                */
            }
        }
        
        else
        {
            QTEIconHolder.SetActive(false);
        }
    }

    public void PopIcon()
    {
        popMinigameController = GameObject.Find("Pop Minigame").GetComponent<PopMinigameController>();
        popMinigameController.PopIcon();
    }

    public void ClosePopIcon()
    {
        popMinigameController.ClosePopIcon(gameObject);
    }

    public void CloseTutorial()
    {
        popMinigameController.CloseTutorial();
    }
}

