using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class ResolutionDropdown : MonoBehaviour {

    [SerializeField]
    public Resolution[] resolutions;
    //public Dropdown dropdownMenu;
    // public Dropdown screenModeDropdown;
    public RectTransform contentPanel;
    public Transform toggleHolder;
    public int screenResLength;
    public GameObject resolutionTogglePrefab;
    public PlayerData playerData;
    public ToggleGroup toggleGroup;
    public Toggle selectedToggle;
    public bool canChangeRes;
    private int timesResChange = 0;
    public bool changingScenes;
    public float timer;

    public int x;
        public int y;

    public bool CurrentFullscreen;

    public int valueChangeCount = 0;


    public enum DisplayModes { Unknown, Fullscreen, Borderless, Windowed }

    public void Awake()
    {

       if (SceneManager.GetActiveScene().name == "Options" || SceneManager.GetActiveScene().name == "GameWorld")
        {

            resolutions = Screen.resolutions;
            screenResLength = resolutions.Length;
            SetupToggles();
            //SelectToggleOnStart();
            contentPanel = transform.parent.parent.GetComponent<ScrollRect>().content;
            
        }
        else
        {
            SetResolution(playerData.resolutionWidth, playerData.resolutionHeight);
        }
    }

    public void SnapTo(RectTransform target)
    {
            Canvas.ForceUpdateCanvases();
            contentPanel.anchoredPosition = (Vector2)toggleHolder.transform.InverseTransformPoint(contentPanel.position) - (Vector2)toggleHolder.transform.InverseTransformPoint(target.position);

         
    }
    public void SelectToggleOnStart()
    {
        int index = 0;
        foreach (Transform child in toggleHolder.transform)
        {
            if (toggleHolder.GetChild(index).GetComponent<ResolutionToggle>().resX == playerData.resolutionWidth && toggleHolder.GetChild(index).GetComponent<ResolutionToggle>().resY == playerData.resolutionHeight)
            {
                canChangeRes = true;
                selectedToggle = toggleHolder.GetChild(index).GetComponent<Toggle>();
                selectedToggle.isOn = true;
                SnapTo(selectedToggle.gameObject.GetComponent<RectTransform>());
            }

            index++;
        }

    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    void SetupToggles()
    {
        for (int i = 0; i <= screenResLength - 1; i++)
        {
            Instantiate(resolutionTogglePrefab, toggleHolder);
        }

        int index = 0;
        foreach (Transform child in toggleHolder.transform)
        {
            
            ResolutionToggle slot = child.GetComponent<ResolutionToggle>();

            if (index < screenResLength)
            {
                slot.resX = resolutions[index].width;
                slot.resY = resolutions[index].height;
                slot.hertz = resolutions[index].refreshRate;
                slot.selectedResNum = index;
                    slot.SetResolutionOptions();
                if(slot.selectedResNum == toggleHolder.transform.childCount - 1 )
                {
                    SelectToggleOnStart();

                }
            }
            
            index++;
        }
    }

    public void SetResolution(int resolutionX, int resolutionY)
    {
        if (SceneManager.GetActiveScene().name == "Options" || SceneManager.GetActiveScene().name == "GameWorld")
        {

            if (canChangeRes)
            {
                if (timesResChange > 1 && !changingScenes)
                {
                    Screen.SetResolution(resolutionX, resolutionY, true);
                    playerData.resolutionWidth = resolutionX;
                    playerData.resolutionHeight = resolutionY;
                    //print("On the scene called " + SceneManager.GetActiveScene().name + " the resolution changed");
                }
                timesResChange += 1;
            }
        }

        else
        {
            if(!changingScenes)
            {
                Screen.SetResolution(resolutionX, resolutionY, true);
                playerData.resolutionWidth = resolutionX;
                playerData.resolutionHeight = resolutionY;
                print("On the scene called " + SceneManager.GetActiveScene().name + " the resolution changed");
            }
            
        }

    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height + " @ " + res.refreshRate + "Hz" ;
    }

    public void IsChangingScenes()
    {
        if(!changingScenes)
        {
            changingScenes = true;
        }

        else
        {
            changingScenes = false;
        }
    }



    /*public void SetScreenMode()
    {
            if (screenModeDropdown.value == 0)
            {
                //Screen.fullScreen = true;
                Screen.SetResolution(resolutions[dropdownMenu.value].width , resolutions[dropdownMenu.value].height , true);
                //if (CurrentFullscreen == false)
               // {
                 //   Screen.SetResolution(resolutions[dropdownMenu.value].width, resolutions[dropdownMenu.value].height, true);
                //}
                CurrentFullscreen = true;
                PlayerPrefs.SetInt("ScreenMode", screenModeDropdown.value);
            }
            else if (screenModeDropdown.value == 1)
            {
                //Screen.fullScreen = false;
                Screen.SetResolution(resolutions[dropdownMenu.value].width , resolutions[dropdownMenu.value].height , false);
               // if (CurrentFullscreen == true)
                //{
                 //   Screen.SetResolution(resolutions[dropdownMenu.value].width, resolutions[dropdownMenu.value].height, false);
                //}
            CurrentFullscreen = false;
                Screen.SetResolution(resolutions[dropdownMenu.value].width, resolutions[dropdownMenu.value].height, false);
                PlayerPrefs.SetInt("ScreenMode", screenModeDropdown.value);
            }


    }
       

    public void SetPlayerPref()
    {
        
        if ( dropdownMenu.options.Count == screenResLength + 1 && valueChangeCount == 0)
        {
            Screen.SetResolution(PlayerPrefs.GetInt("Previous Width"), PlayerPrefs.GetInt("Previous Height"), CurrentFullscreen);
            dropdownMenu.value = PlayerPrefs.GetInt("Resolution Value");
            valueChangeCount += 1;

            
            //dropdownMenu.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[PlayerPrefs.GetInt("Resolution Value")].width, resolutions[PlayerPrefs.GetInt("Resolution Value")].height, CurrentFullscreen); });
        }
        if (valueChangeCount != 0)
        {
            PlayerPrefs.SetInt("Resolution Value", dropdownMenu.value);
            Screen.SetResolution(resolutions[PlayerPrefs.GetInt("Resolution Value")].width, resolutions[PlayerPrefs.GetInt("Resolution Value")].height, CurrentFullscreen);
           PlayerPrefs.SetInt("Previous Width", resolutions[PlayerPrefs.GetInt("Resolution Value")].width);
            PlayerPrefs.SetInt("Previous Height", resolutions[PlayerPrefs.GetInt("Resolution Value")].height);
            x = PlayerPrefs.GetInt("Previous Width");
            y = PlayerPrefs.GetInt("Previous Height");
        }
    }
    */
    
}


