using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public EventSystem eventSystem;
    public PlatformDetection platformDetection;
    public CanvasGroup controlsMenu;
    public bool controlsShown = true;
    private string sceneName;

    [Header("General UI")]
    public GameObject backButton;
    public GameObject alternateButton; // Button selected when back button goes away
    public ScrollRect scrollGroup1;
    public ScrollRect scrollGroup2;
    public ScrollRect scrollGroup3;
    public ScrollRect scrollGroup4;
    public RectTransform contentPanel1;
    public RectTransform contentPanel2;
    public RectTransform contentPanel3;
    public RectTransform contentPanel4;

    [Header("Main Menu")]
    public GameObject quitMenu;
    public GameObject quitController;
    public GameObject quitKeyboard;
    public GameObject playButton;


    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            Time.timeScale = 1;
        }

        if (SceneManager.GetActiveScene().name == "ItemShop")
        {
        }
    }

    public void SnapTo(RectTransform target)
    {
        if(platformDetection.controllerInput)
        {
          //  Canvas.ForceUpdateCanvases();
            if(contentPanel1 != null && scrollGroup1 != null)
            {
              //  contentPanel1.anchoredPosition = (Vector2)scrollGroup1.transform.InverseTransformPoint(contentPanel1.position) - (Vector2)scrollGroup1.transform.InverseTransformPoint(target.position);
               // float //normalizePosition = (float)contentPanel1.transform.GetSiblingIndex() / (float)scrollGroup1.content.transform.childCount;
               // scroll.verticalNormalizedPosition = 1 - normalizePosition;

                float normalizePosition = contentPanel1.anchorMin.y - target.anchoredPosition.y;
                normalizePosition += (float)target.transform.GetSiblingIndex() / (float)scrollGroup1.content.transform.childCount;
                normalizePosition /= 1500f;
                normalizePosition = Mathf.Clamp01(1 - normalizePosition);
                scrollGroup1.verticalNormalizedPosition = normalizePosition;
                scrollGroup1.verticalScrollbar.interactable = false;
            }
            if(contentPanel2 != null && scrollGroup2 != null)
            {
                float normalizePosition = contentPanel2.anchorMin.y - target.anchoredPosition.y;
                normalizePosition += (float)target.transform.GetSiblingIndex() / (float)scrollGroup2.content.transform.childCount;
                normalizePosition /= 1500f;
                normalizePosition = Mathf.Clamp01(1 - normalizePosition);
                scrollGroup2.verticalNormalizedPosition = normalizePosition;
                scrollGroup2.verticalScrollbar.interactable = false;
            }
            if(contentPanel3 != null && scrollGroup3 != null)
            {
                float normalizePosition = contentPanel3.anchorMin.y - target.anchoredPosition.y;
                normalizePosition += (float)target.transform.GetSiblingIndex() / (float)scrollGroup3.content.transform.childCount;
                normalizePosition /= 1500f;
                normalizePosition = Mathf.Clamp01(1 - normalizePosition);
                scrollGroup3.verticalNormalizedPosition = normalizePosition;
                scrollGroup3.verticalScrollbar.interactable = false;
            }
            if(contentPanel4 != null && scrollGroup4 != null)
            {
                float normalizePosition = contentPanel4.anchorMin.y - target.anchoredPosition.y;
                normalizePosition += (float)target.transform.GetSiblingIndex() / (float)scrollGroup4.content.transform.childCount;
                normalizePosition /=    1500f;
                normalizePosition = Mathf.Clamp01(1 - normalizePosition);
                scrollGroup4.verticalNormalizedPosition = normalizePosition;
                scrollGroup4.verticalScrollbar.interactable = false;
            }

        }
       
        
    }

    public void OpenQuitMenu(GameObject selectedButton)
    {
        
        if (quitMenu.activeInHierarchy==true)
        {
            CloseQuitMenu(playButton);

        }

        else
        {
            quitMenu.SetActive(true);
            eventSystem.SetSelectedGameObject(selectedButton);
            if (platformDetection.controllerInput)
            {
                ShowControls();
            }
           
        }
       
       
      

    }
    public  void CloseQuitMenu(GameObject selectedButton)
    {

            quitMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(selectedButton);
        if (quitMenu.activeInHierarchy == false && platformDetection.controllerInput)
        {

            ShowControls();

        }

    }

    public void LoadNewScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowControls()
    {
        controlsShown = !controlsShown;
    }

    public void Update()
    {
        if(controlsShown )
        {
            if(controlsMenu.alpha != 1)
            {
                controlsMenu.alpha += Time.deltaTime * 4f;
            }
        }

        else
        {
            if (controlsMenu.alpha != 0)
            {
                controlsMenu.alpha -= Time.deltaTime *4f;
            }
        }

        if(sceneName == "Main Menu")
        {
            if (quitMenu.activeInHierarchy==true && platformDetection.controllerInput == true)
            {
                quitController.SetActive(true);
                quitKeyboard.SetActive(false);
            }
            else if(quitMenu.activeInHierarchy == true && platformDetection.controllerInput == false)
            {
                
                quitKeyboard.SetActive(true);
                quitController.SetActive(false);
            }
        }

        if(sceneName != "Main Menu" || sceneName != "SplashScreen" || sceneName != "GameWorld")
        {
            if(platformDetection.controllerInput == true)
            {
                backButton.SetActive(false);
                if(eventSystem.currentSelectedGameObject == null)
                {
                    eventSystem.SetSelectedGameObject(alternateButton);
                }
            }

            else
            {
                backButton.SetActive(true);
            }
            
        }

        if(platformDetection.controllerInput == false && sceneName == "ItemShop" || sceneName == "Inventory")
        {

                if (scrollGroup1 != null)
                {
                    scrollGroup1.verticalScrollbar.interactable = true;
                }
                if (scrollGroup2 != null)
                {
                    scrollGroup2.verticalScrollbar.interactable = true;
                }
                if (scrollGroup3 != null)
                {
                    scrollGroup3.verticalScrollbar.interactable = true;
                }
                if (scrollGroup4 != null)
                {
                    scrollGroup4.verticalScrollbar.interactable = true;
                }
        }

        else if(platformDetection.controllerInput)
        {
            if (scrollGroup1 != null)
            {
                scrollGroup1.verticalScrollbar.interactable = false;
            }
            if (scrollGroup2 != null)
            {
                scrollGroup2.verticalScrollbar.interactable = false;
            }
            if (scrollGroup3 != null)
            {
                scrollGroup3.verticalScrollbar.interactable = false;
            }
            if (scrollGroup4 != null)
            {
                scrollGroup4.verticalScrollbar.interactable = false;
            }
        }
    }
}
