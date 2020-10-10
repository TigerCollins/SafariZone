using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

//author: Kingyan
/// <summary>
/// code for the settings menu (doesn't include keybinding)
/// </summary>
public class SettingsMenuScript : MonoBehaviour
{
    public Camera mainCamera;
    public Slider FOVSlider, SensitivitySlider, volumeSlider;
    public TMP_Dropdown languageDropdown;
    public AudioMixer mixer;
    public Toggle vSyncToggle, fullscreenToggle;
    public ResolutionType[] resolutions;
    public int selectedRes;
    public float sensitivity;
    //text that displays the selected resolution
    public TextMeshProUGUI resText;

    //which language has been selected
    string language;
    MouseLook mouseLook;

    // Start is called before the first frame update
    void Start()
    {
        //setup player prefs
        GetPlayerPrefs();

        mouseLook = FindObjectOfType<MouseLook>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseLook.mouseSensitivity = sensitivity;
    }

    public void GetPlayerPrefs()
    {
        FOVSlider.value = PlayerPrefs.GetInt("fov", 60);
        SensitivitySlider.value = PlayerPrefs.GetInt("sensitivity", 100);
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume", 1);

        //language dropdown init
        string lang = PlayerPrefs.GetString("gamelang");
        if (lang == "en")
        {
            languageDropdown.value = 0;
        }
        else if (lang == "fr")
        {
            languageDropdown.value = 1;
        }

        //graphics settings init
        fullscreenToggle.isOn = PlayerPrefsExtra.GetBool("fullscreen", true);

        if (PlayerPrefsExtra.GetBool("vSync", false))
        {
            vSyncToggle.isOn = true;
        }
        else
        {
            vSyncToggle.isOn = false;
        }

        selectedRes = PlayerPrefs.GetInt("screenRes", 0);
        resText.text = resolutions[selectedRes].width + " x " + resolutions[selectedRes].height;
    }

    public void SaveSettings()
    {
        //set graphics settings
        SetResolution();
        ApplyFullscreen();
        ApplyVSync();

        //save misc player prefs
        PlayerPrefs.SetInt("fov", (int)mainCamera.fieldOfView);
        PlayerPrefs.SetInt("sensitivity", (int)sensitivity);
        PlayerPrefs.SetFloat("masterVolume", volumeSlider.value);
        PlayerPrefs.SetString("gamelang", language);

        //graphcs player prefs
        PlayerPrefs.SetInt("screenRes", selectedRes);

        //sets time scale back to 1 and locks cursor
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetFOV()
    {
        float sliderValue = FOVSlider.value;
        mainCamera.fieldOfView = sliderValue;
    }

    public void SetSensitivity()
    {
        float sliderValue = SensitivitySlider.value;
        sensitivity = sliderValue;
    }

    public void SetMasterVolume()
    {
        float sliderValue = volumeSlider.value;
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void LanguageDropdown()
    {
        int value = languageDropdown.value;

        if(value == 0)
        {
            language = "en";
        } 
        else if (value == 1)
        {
            language = "fr";
        }
    }

    public void ResolutionLeft()
    {
        if(selectedRes > 0)
        {
            selectedRes--;
        }

        resText.text = resolutions[selectedRes].width + " x " + resolutions[selectedRes].height;
    }

    public void ResolutionRight()
    {
        if(selectedRes < resolutions.Length)
        {
            selectedRes++;
        }

        resText.text = resolutions[selectedRes].width + " x " + resolutions[selectedRes].height;
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    void SetResolution()
    {
        Screen.SetResolution(resolutions[selectedRes].width, resolutions[selectedRes].height, fullscreenToggle.isOn);
    }

    void ApplyFullscreen()
    {
        if (fullscreenToggle.isOn)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
    }

    void ApplyVSync()
    {
        if (vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }
}


[System.Serializable]
public class ResolutionType
{
    public int width;
    public int height;
}