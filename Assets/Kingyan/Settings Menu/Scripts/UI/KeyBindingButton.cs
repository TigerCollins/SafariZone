using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

// author: Kingyan Incorporated
/// <summary>
/// Attach this code to the button that activates input detection for keybinding.
/// </summary>
public class KeyBindingButton : MonoBehaviour
{

    public DefaultInputActions inputActions;

    //name of the PlayerPrefs key that stores the key
    public string keyName;
    //text that displays which key has been saved
    public Text keyText;
    //default keybind
    public KeyCode defaultValue;
    //panel that pops up when input detection has started
    public GameObject keyPanel;

    //if the button has been selected
    bool selected = false;
    //keypress that has benn detected
    public KeyCode kCode;
    //private KeyCode code;

    public SaveKeybinding saveKeybinding;
    private bool hasBeenSet;
    public KeyCode currentKCode;


    // Start is called before the first frame update
    void Start()
    {
        saveKeybinding = GameObject.Find("ScriptController").GetComponent<SaveKeybinding>();
        keyText = transform.GetChild(0).GetComponent<Text>();
        keyPanel = transform.parent.transform.parent.transform.GetChild(2).gameObject;
        keyText.text = PlayerPrefsExtra.GetKeyCode(keyName, defaultValue).ToString();
        keyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            hasBeenSet = false;
            keyPanel.SetActive(true);
            DetectInput();
            keyText.text = "...";

           
               
                    if (kCode != KeyCode.None && hasBeenSet == false)
                    {
                        PlayerPrefsExtra.SetKeyCode(keyName, kCode);
                        keyText.text = kCode.ToString();
                        //selected = false;
                        kCode = KeyCode.None;

                        hasBeenSet = true;
                    }
                    else
                    {

                        selected = true;
                    }
                
            

            

            if(hasBeenSet == true)
            {
                selected = false;
            }
        }

        else if(!selected  )
        {
            keyPanel.SetActive(false);
           // 
        }

        else if(kCode == KeyCode.None)
        {
            keyText.text = "";
        }
    }

    public void Selected()
    {
        
        selected = true;
        
         
    }

    public void StoreControlOverrides()
    {
      ///  Binding

    }


    public void DetectInput()
    {
        
        foreach (InputActionMap vkey in System.Enum.GetValues(typeof(KeyCode)))
        {
       //     InputActionRebindingExtensions.PerformInteractiveRebinding(inputActions.Player.Interact.).Start();
          //  inputActions.Player.Interact.ChangeBinding().
              //  Input.PerformInteractiveRebinding().Start();
            /*
                for (int i = 0; i < saveKeybinding.keyBindingList.Count; i++)
                {
                   if( vkey != saveKeybinding.keyBindingList[i].currentKCode)
                    {
                        if (vkey != KeyCode.Return)
                        {
                            kCode = vkey; //this saves the key being pressed      
                            currentKCode = vkey;
                            saveKeybinding.SaveInput();
                        }
                     
                    }
                }

            }
            // && )*/
        }
    }
}
