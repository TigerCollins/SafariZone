using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuPrefabController : MonoBehaviour
{
    
    public EventSystem eventSystem;
    public GameObject resumeButton;
    public Animator mainMenuAnimator;
    public SceneChanger sceneChanger;
    private string privateSceneName;

    [Header("Menu Prefabs")]
    public Animator parkPadAnimator;
    public GameObject otherMenuHolder;
    public GameObject mainMenuPrefab;
    public GameObject indexMenuPrefab;
    public Animator indexMenuAnimator;
    public Animator backpackMenuAnimator;
    public GameObject optionsMenuPrefab;
    public Animator optionsMenuAnimator;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
       // CloseMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void Hide

    public void CloseMenu()
    {
        backpackMenuAnimator.SetBool("IsOpen", false);
        parkPadAnimator.SetBool("IsOpen", false);
        indexMenuAnimator.SetBool("IsOpen", false);
        FindObjectOfType<GameController>().DeselectButton();
        gameObject.GetComponent<GameController>().gamePaused = false;
        gameObject.GetComponent<GameController>().audioManager.inMenu = false;
        gameObject.GetComponent<GameController>().audioManager.ChangeLowPass();
        FindObjectOfType<GameController>().playerScript.canMove = true;
    }




    public void GoToMainMenuFromGame()
    {
        otherMenuHolder.SetActive(true);
        indexMenuAnimator.SetBool("IsOpen", false);
        backpackMenuAnimator.SetBool("IsOpen", false);
        
        mainMenuPrefab.SetActive(true);
        eventSystem.SetSelectedGameObject(resumeButton);
        parkPadAnimator.SetBool("IsOpen", true);
    }

        public void GoToMainMenu()
    {
        
        optionsMenuAnimator.SetBool("IsOpen", false);
        backpackMenuAnimator.SetBool("IsOpen", false);
        indexMenuAnimator.SetBool("IsOpen", false);
        parkPadAnimator.SetBool("IsOpen", true);

        //eventSystem.SetSelectedGameObject(resumeButton);



    }

   /* IEnumerator GoToMenu()
    {
        //sceneChanger.OpenPanelNoSceneChange();
        //yield return new WaitForSecondsRealtime(sceneChanger.animationTime);
        mainMenuAnimator.SetBool("IsOpen", true);
        otherMenuHolder.SetActive(true);
        indexMenuPrefab.SetActive(false);
        backpackMenuPrefab.SetActive(false);
        ;
        mainMenuPrefab.SetActive(true);
        sceneChanger.ClosePanelNoSceneChange();
    }
    */
        public void GoToIndexPrefab(GameObject selectedButton)
    {
        eventSystem.SetSelectedGameObject(selectedButton);

        backpackMenuAnimator.SetBool("IsOpen", false);
        indexMenuAnimator.SetBool("IsOpen", true);
        parkPadAnimator.SetBool("IsOpen", false);
       // ;
        indexMenuPrefab.GetComponent<Index>().UpdateCreatureContainerSlots();
    }


    public void GoToCreditsPrefab(GameObject selectedButton)
    {
        StartCoroutine("GoToCredits");
        eventSystem.SetSelectedGameObject(selectedButton);
    }

    IEnumerator GoToCredits()
    {
        sceneChanger.OpenPanelNoSceneChange();
        yield return new WaitForSecondsRealtime(sceneChanger.animationTime);
        mainMenuPrefab.SetActive(false);
       // backpackMenuPrefab.SetActive(false);
        indexMenuPrefab.SetActive(false);
        //;
        sceneChanger.ClosePanelNoSceneChange();
    }

    public void GoToBackpackPrefab(GameObject selectedButton)
    {
        //sceneChanger.OpenPanelNoSceneChange();
       // yield return new WaitForSecondsRealtime(sceneChanger.animationTime);

        backpackMenuAnimator.SetBool("IsOpen", true);
        eventSystem.SetSelectedGameObject(resumeButton);
        parkPadAnimator.SetBool("IsOpen", false);
        //backpackMenuPrefab.SetActive(true);
        // sceneChanger.ClosePanelNoSceneChange();
        eventSystem.SetSelectedGameObject(selectedButton);
    }

    IEnumerator GoToBackpack()
    {
        sceneChanger.OpenPanelNoSceneChange();
        yield return new WaitForSecondsRealtime(sceneChanger.animationTime);
        mainMenuPrefab.SetActive(false);
        ;
        indexMenuPrefab.SetActive(false);
        //backpackMenuPrefab.SetActive(true);
        sceneChanger.ClosePanelNoSceneChange();
    }

        public void GoToMapPrefab(GameObject selectedButton)
    {
        mainMenuPrefab.SetActive(false);
      //  backpackMenuPrefab.SetActive(false);
        ;
        indexMenuPrefab.SetActive(false);
        eventSystem.SetSelectedGameObject(selectedButton);
    }
    public void GoToOptionsPrefab(GameObject selectedButton)
    {
        optionsMenuAnimator.SetBool("IsOpen", true);
        eventSystem.SetSelectedGameObject(resumeButton);
        parkPadAnimator.SetBool("IsOpen", false);
        //backpackMenuPrefab.SetActive(true);
        // sceneChanger.ClosePanelNoSceneChange();
        eventSystem.SetSelectedGameObject(selectedButton);
    }

    IEnumerator GoToOptions()
    {
        sceneChanger.OpenPanelNoSceneChange();
        yield return new WaitForSecondsRealtime(sceneChanger.animationTime);
        mainMenuPrefab.SetActive(false);
        //backpackMenuPrefab.SetActive(false);
        optionsMenuPrefab.SetActive(true);
        indexMenuPrefab.SetActive(false);
        sceneChanger.ClosePanelNoSceneChange();
    }
    public void QuitGame(string sceneName)
    {
        StartCoroutine("QuitWorld");
        privateSceneName = sceneName;
    }

    IEnumerator QuitWorld()
    {
        sceneChanger.OpenPanelNoSceneChange();
        yield return new WaitForSecondsRealtime(sceneChanger.animationTime);

        Application.LoadLevel(privateSceneName);
    }
    }

/*menu colours
 DB673D
 893719

    849371
    5E6555

    D64646
    A13C3C

    E08AA6
    894C60
 * 
 * 
 * */
