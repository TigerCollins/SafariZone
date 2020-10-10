using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;


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
    private string deviceLayoutName;

    [Header("UI Elements - Xbox")]
    public Sprite xboxBack;
    public Sprite xboxSelect;
    public Sprite xboxBumperRight;
    public Sprite xboxBumperLeft;

    [Header("UI Elements - Xbox")]
    public Sprite playstationBack;
    public Sprite playstationSelect;
    public Sprite playstationBumperRight;
    public Sprite playstationBumperLeft;

    [Header("UI Elements - Keyboard")]
            public Sprite keyboardBack;
    public Sprite keyboardSelect;
    public Sprite keyboardLeft;
    public Sprite keyboardRight;

    [Header("Main Menu")]
    public Image Select;
    public Image Quit;
    public Image quitYes;
    public Image quitNo;

    [Header("Item Shop/Inventory")]
    public Image pageLeft;
    public Image pageRight;

    public static PlatformDetection Instance;
    private string sceneName;
    

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
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
            if (xboxController)
            {
                Select.sprite = xboxSelect;
                Quit.sprite = xboxBack;
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
            }


            else
            {
                Select.sprite = playstationSelect;
                Quit.sprite = playstationBack;
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

            }
        }



        else
        {
            Select.sprite = keyboardSelect;
            Quit.sprite = keyboardBack;
            pageLeft.sprite = keyboardLeft;
            pageRight.sprite = keyboardRight;
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
