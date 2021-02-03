using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.InputSystem.Utilities;

using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PopMinigameButton : MonoBehaviour
{
    public EventSystem eventSystem;
    public PopMinigameController popMinigameController;
    public GameController gameController;
    public Text keyInputText1;
    public Text keyInputText2;
    public Text keyInputText3;
    public Text keyInputText4;
    public Text keyInputText5;
    public string buttonInputRequired;
    public PlatformDetection platformDetection;
    public GameObject QTEIconHolder1;
    public GameObject QTEIconHolder2;
    public GameObject QTEIconHolder3;
    public GameObject QTEIconHolder4;
    public GameObject QTEIconHolder5;
    public int activeButtonResult;
    public string neededButton;
    public int neededButtonInt;
    public GameObject reticle;
    public GameObject thisButton;

    [Header("Action")]
    public int quicktimeInput;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    private int counter;

    [Header("Canvas Groups")]
    public CanvasGroup MasterGroup;
    public CanvasGroup Option1Icon;
    public CanvasGroup Option1Text;
    public CanvasGroup Option2Icon;
    public CanvasGroup Option2Text;
    public CanvasGroup Option3Icon;
    public CanvasGroup Option3Text;
    public CanvasGroup Option4Icon;
    public CanvasGroup Option4Text;
    public CanvasGroup Option5Icon;
public CanvasGroup Option5Text;


    // Start is called before the first frame update
    void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        neededButton = transform.GetChild(activeButtonResult).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text;
        popMinigameController = FindObjectOfType<PopMinigameController>();
        //string neededButton = pmb.transform.GetChild(pmb.activeButtonResult).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text;
        keyInputText1.text = buttonInputRequired;
        keyInputText2.text = buttonInputRequired;
        keyInputText3.text = buttonInputRequired;
        keyInputText4.text = buttonInputRequired;
        keyInputText5.text = buttonInputRequired;
        gameController = FindObjectOfType<GameController>();
        platformDetection = FindObjectOfType<PlatformDetection>();
        CanvasGroupController();
        SetQTE();
    }

    public void Update()
    {
        CanvasGroupController();
        if (thisButton !=null)
        {
            if(eventSystem.currentSelectedGameObject == thisButton)
            {
                reticle.SetActive(true);
            }

            else
            {
                reticle.SetActive(false);
            }
        }

        if(popMinigameController.captureTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetQTE()
    {
        

        
        
        quicktimeInput = Random.Range(0, 4);
        activeButtonResult = quicktimeInput;
        neededButtonInt = quicktimeInput;
        if (platformDetection.controllerInput == false)
        {
            /*
         
            if (gameController.playerData.quicktimeUseMouse == false)
            {

                if (quicktimeInput == 0)
                {
                    button1.SetActive(true);
                }
                if (quicktimeInput == 1)
                {
                    button2.SetActive(true);
                }
                if (quicktimeInput == 2)
                {
                    button3.SetActive(true);
                }
                if (quicktimeInput == 3)
                {
                    button4.SetActive(true);
                }
                if (quicktimeInput == 4)
                {
                    button5.SetActive(true);
                }
                keyInputText1.gameObject.SetActive(true);
                keyInputText2.gameObject.SetActive(true);
                keyInputText3.gameObject.SetActive(true);
                keyInputText4.gameObject.SetActive(true);
                keyInputText5.gameObject.SetActive(true);
                QTEIconHolder1.SetActive(false);
                QTEIconHolder2.SetActive(false);
                QTEIconHolder3.SetActive(false);
                QTEIconHolder4.SetActive(false);
                QTEIconHolder5.SetActive(false);
            }
            else
            {
                keyInputText1.gameObject.SetActive(false);
                keyInputText2.gameObject.SetActive(false);
                keyInputText3.gameObject.SetActive(false);
                keyInputText4.gameObject.SetActive(false);
                keyInputText5.gameObject.SetActive(false);
                //buttonInputRequired = null;
            }

    */
        }

        else
        {
            if (quicktimeInput == 0)
            {
                button1.SetActive(true);
            }
            if (quicktimeInput == 1)
            {
                button2.SetActive(true);
            }
            if (quicktimeInput == 2)
            {
                button3.SetActive(true);
            }
            if (quicktimeInput == 3)
            {
                button4.SetActive(true);
            }
            if (quicktimeInput == 4)
            {
                button5.SetActive(true);
            }
         /*   QTEIconHolder1.SetActive(true);
            QTEIconHolder2.SetActive(true);
            QTEIconHolder3.SetActive(true);
            QTEIconHolder4.SetActive(true);
            QTEIconHolder5.SetActive(true);
            */
        }
     
       // keyInputText1.gameObject.SetActive(false);
       // keyInputText2.gameObject.SetActive(false);
      //  keyInputText3.gameObject.SetActive(false);
       // keyInputText4.gameObject.SetActive(false);
       // keyInputText5.gameObject.SetActive(false);

    }

    public void PopIcon()
    {

       if(platformDetection.controllerInput == true)
        {
            if (counter == 0)
            {
                counter += 1;
                popMinigameController.PopIcon();
              
                //counter = 0;
            }
        }

       else
        {
            popMinigameController.PopIcon();
        }
        
       
    }

    public void ClosePopIcon()
    {
        
        Destroy(gameObject);
      
       // popMinigameController.PopIcon();
   
    }

    public void CloseTutorial()
    {
        popMinigameController.CloseTutorial();
    }

    //public void AttemptClick

    //public void

    public void CanvasGroupController()
    {
        if(platformDetection.controllerInput == false)
        {
            MasterGroup.alpha = 1;
            // IF KEYBOARD IS DISABLED   if()
            Option1Icon.alpha = 0;
            Option2Icon.alpha = 0;
            Option3Icon.alpha = 0;
            Option4Icon.alpha = 0;
            Option5Icon.alpha = 0;
        }

        else
        {
            MasterGroup.alpha = 1;
            Option1Icon.alpha = 1;
            Option2Icon.alpha = 1;
            Option3Icon.alpha = 1;
            Option4Icon.alpha = 1;
            Option5Icon.alpha = 1;
        }
    }
}

