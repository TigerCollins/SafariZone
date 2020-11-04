using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;
using System;

public class PlatformDetection : MonoBehaviour
{
    public InputActionAsset inputs;
    public PlayerInput playerInput;
    public bool controllerInput;
    public bool xboxController;
    private Gamepad gamepad;
    private Keyboard keyboard;
    private Mouse mouse;
    public InputUser inputUser;
    public InputDevice inputDevice;
    public LayoutGroup layoutGroup;
    public bool onConsole;
    public GameObject[] notOnConsoleOptions;

    [Header("UI Elements - Xbox")]
    public Sprite xboxBack;
    public Sprite xboxSelect;
    public Sprite xboxBumperRight;
    public Sprite xboxBumperLeft;
    public Sprite xboxAnalogueLeft;

    [Header("UI Elements - Xbox")]
    public Sprite playstationBack;
    public Sprite playstationSelect;
    public Sprite playstationBumperRight;
    public Sprite playstationBumperLeft;
    public Sprite playstationAnalogueLeft;

    [Header("UI Elements - Keyboard")]
    public Sprite keyboardBack;
    public Sprite keyboardSelect;
    public Sprite keyboardLeft;
    public Sprite keyboardRight;
    public Sprite keyboardArrowScroll;

    [Header("Main Menu")]
    public Image Select;
    public Image Quit;
    public Image quitYes;
    public Image quitNo;

    [Header("Item Shop/Inventory")]
    public Image pageLeft;
    public Image pageRight;

    [Header("Index")]
    public GameObject select;

    [Header("Credits")]
    public Image scrollImage;

    public static PlatformDetection Instance;
    private string sceneName;


    // Start is called before the first frame update
    void Awake()
    {
        if (Application.platform != RuntimePlatform.WindowsPlayer || Application.platform != RuntimePlatform.OSXPlayer || Application.platform != RuntimePlatform.LinuxPlayer || Application.platform != RuntimePlatform.WindowsEditor)
        {
            onConsole = true;
        }
        gamepad = Gamepad.current;
        inputDevice = gamepad;
        mouse = Mouse.current;
        keyboard = Keyboard.current;


        if (gamepad != null)
        {
            controllerInput = true;
            //mouse.
            if (gamepad.layout == "XInputControllerWindows")
            {
                xboxController = true;
            }

            else
            {
                xboxController = false;
            }
        }

        playerInput = FindObjectOfType<PlayerInput>().gameObject.GetComponent<PlayerInput>();
        if (Application.platform == RuntimePlatform.XboxOne)
        {
            controllerInput = true;
            xboxController = true;
        }

        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            controllerInput = false;
            xboxController = false;
        }

        sceneName = SceneManager.GetActiveScene().name;

        if (onConsole != true && sceneName == "Options")
        {
            FindObjectOfType<UIManager>().verticalScrollPower = (FindObjectOfType<UIManager>().verticalScrollPower / 53) *18;
            for (int i = 0; i < notOnConsoleOptions.Length; i++)
            {
                notOnConsoleOptions[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetTing();

        gamepad = Gamepad.current;
        inputDevice = gamepad;
        mouse = Mouse.current;
        keyboard = Keyboard.current;

        MainMenu();

    } 

    public void MainMenu()
    {
        if (controllerInput)
        {
            if (sceneName == "Index")
            {
                select.SetActive(false);
                layoutGroup.SetLayoutHorizontal();
            }
            if (xboxController)
            {
                if (select != null)
                {
                    Select.sprite = xboxSelect;
                }
                if(Quit !=null)
                {
                    if (Quit.sprite != null)
                    {
                        Quit.sprite = xboxBack;
                    }
                }
               
    
                if (sceneName == "Main Menu")
                {
                    quitNo.sprite = xboxBack;
                    quitYes.sprite = xboxSelect;
                }

                else if (sceneName == "ItemShop")
                {
                    pageLeft.sprite = xboxBumperLeft;
                    pageRight.sprite = xboxBumperRight;
                }
                else if (sceneName == "Backpack")
                {
                    pageLeft.sprite = xboxBumperLeft;
                    pageRight.sprite = xboxBumperRight;
                }
                else if (sceneName == "Credits")
                {
                    scrollImage.sprite = xboxAnalogueLeft;
                }
            }


            else
            {
                if (select != null)
                {
                    Select.sprite = playstationSelect;
                }
                if (Quit.sprite != null)
                {
                    Quit.sprite = playstationBack;
                }
                if (sceneName == "Main Menu")
                {
                    quitNo.sprite = playstationBack;
                    quitYes.sprite = playstationSelect;
                }
                else if (sceneName == "ItemShop")
                {
                    pageLeft.sprite = playstationBumperLeft;
                    pageRight.sprite = playstationBumperRight;
                }
                else if (sceneName == "Backpack")
                {
                    pageLeft.sprite = playstationBumperLeft;
                    pageRight.sprite = playstationBumperRight;
                }
                else if (sceneName == "Credits")
                {
                    scrollImage.sprite = playstationAnalogueLeft;
                }
            }
        }



        else
        {
            if(select!=null)
            {
                Select.sprite = keyboardSelect;
            }
            if(Quit!= null)
            {
                if (Quit.sprite != null)
                {
                    Quit.sprite = keyboardBack;
                }
            }
          

            if(sceneName == "Backpack" || sceneName == "ItemShop")
            {
                pageLeft.sprite = keyboardLeft;
                pageRight.sprite = keyboardRight;

            }

            else if (sceneName == "Index")
            {
                select.SetActive(true);
                layoutGroup.SetLayoutHorizontal();
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)layoutGroup.transform);
            }
            else if (sceneName == "Credits")
            {
                scrollImage.sprite = keyboardArrowScroll;
            }

        }
    }
    

    public void SetTing()
    {
            gamepad = Gamepad.current;
            mouse = Mouse.current;
            keyboard = Keyboard.current;
           // deviceLayoutName = gamepad.displayName;
           if(gamepad!=null)
        {
            if (gamepad.wasUpdatedThisFrame)
            {
                controllerInput = true;
                //mouse.
                if (gamepad.wasUpdatedThisFrame && gamepad.layout == "XInputControllerWindows")
                {
                    xboxController = true;
                }

                else
                {
                    xboxController = false;
                }
            }
        }

        if (mouse != null || keyboard != null)
        {
            if (mouse.wasUpdatedThisFrame || keyboard.wasUpdatedThisFrame)
            {
                controllerInput = false;
                xboxController = false;
            }
        }
        
    
    }

        public void OnLevelWasLoaded(int level)
    {
        playerInput = FindObjectOfType<PlayerInput>().gameObject.GetComponent<PlayerInput>();
        //  playerInput.defaultControlScheme = playerInput.
    }
}
