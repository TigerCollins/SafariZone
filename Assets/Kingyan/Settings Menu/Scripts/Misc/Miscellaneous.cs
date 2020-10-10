using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

//author: Kingyan
/// <summary>
/// Saves and load less common variable types from PlayerPrefs
/// </summary>
public class PlayerPrefsExtra
{
    public static bool GetBool(string key, bool defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            if (PlayerPrefs.GetInt(key) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (defaultValue)
            {
                PlayerPrefs.SetInt(key, 1);
                return true;
            }
            else
            {
                PlayerPrefs.SetInt(key, 0);
                return false;
            }
        }
    }

    public static void SetBool(string key, bool value)
    {
        if (value)
        {
            PlayerPrefs.SetInt(key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }

    public static KeyCode GetKeyCode(string key, KeyCode defaultValue)
    {
        int keyCode;
        if (PlayerPrefs.HasKey(key))
        {
            keyCode = PlayerPrefs.GetInt(key);
            return (KeyCode) keyCode;
        }
        else
        {
            PlayerPrefs.SetInt(key, (int)defaultValue);
            return defaultValue;
        }
    }

    public static void SetKeyCode(string key, KeyCode value)
    {
        PlayerPrefs.SetInt(key, (int)value);
    }
}
