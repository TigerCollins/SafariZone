using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLaunch : MonoBehaviour
{
    public int timesLoaded;
    public GameObject firstTimePlayPopup;
    // Start is called before the first frame update
    void Awake()
    {
        timesLoaded = PlayerPrefs.GetInt("Times Loaded");

        if (timesLoaded < 1)
        {
            firstTimePlayPopup.SetActive(true);
        }
        timesLoaded += 1;
        PlayerPrefs.SetInt("Times Loaded", timesLoaded);
    }

    public void ClosePopup()
    {
        firstTimePlayPopup.SetActive(false);
    }


    public void ClearPlayerPref()
    {
        PlayerPrefs.SetInt("Times Loaded", 0);
    }
}
