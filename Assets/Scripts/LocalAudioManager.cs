using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LocalAudioManager : MonoBehaviour
{
    public AudioManager audioManager;
    public PlayerVariables player;
    public Slider masterSlider;
    public Slider gameSlider;
    public Slider musicSlider;
    public Slider menuSlider;
    public float newVolume1;
    public float newVolume2;
    public float newVolume3;
    public float newVolume4;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if(SceneManager.GetActiveScene().name == "Options" || SceneManager.GetActiveScene().name == "GameWorld" || SceneManager.GetActiveScene().name == "Splash Screen")
        {
            if(gameObject.name == "LocalAudioManager")
            {
                masterSlider.value = player.masterVolumeCore;
                menuSlider.value = player.menuVolumeCore;
                gameSlider.value = player.gameVolumeCore;
                musicSlider.value = player.musicVolumeCore;
                audioManager.audioMixer.SetFloat("Master Volume", player.masterVolume);
                audioManager.audioMixer.SetFloat("Game Volume", player.gameVolume);
                audioManager.audioMixer.SetFloat("Music Volume", player.musicVolume);
                audioManager.audioMixer.SetFloat("Menu Volume", player.menuVolume);
            }

        }
    }

    public void ReutrnToMenuAudio()
    {
        PlayerPrefs.SetInt("Play Song", 1);
    }

    public void OneShotMenuClick(AudioClip audioClip)
    {
        if(audioManager !=null)
        {
            audioManager.OneShotMenuClick(audioClip);
        }
       
    }

    public void OneShotDialogue()
    {
        if (audioManager != null)
        {
            audioManager.OneShotDialogue();
        }

    }

    public void OneShotShrub(AudioClip gameClip)
    {
        if (audioManager != null)
        {
            audioManager.OneShotShrub(gameClip);
        }

    }

    public void OneShotSFX(AudioClip gameClip)
    {
        if (audioManager != null)
        {
            audioManager.OneShotSFX(gameClip);
        }

    }

    public void MasterVolume()
    {
        float volumeValue = masterSlider.value;

        if (volumeValue == 0)
        {
            audioManager.audioMixer.SetFloat("Master Volume", -80);
            newVolume1 = -80;
        }

        else
        {
            audioManager.audioMixer.SetFloat("Master Volume", -40f + (volumeValue / 2.25f));
            newVolume1 = -40f + (volumeValue / 2.25f);
        }
        player.masterVolume = newVolume1;
        player.masterVolumeCore = volumeValue;

    }

    public void GameVolume()
    {
        float volumeValue = gameSlider.value;
        if (volumeValue == 0)
        {
            audioManager.audioMixer.SetFloat("Game Volume", -80);
            newVolume2 = -80;
        }

        else
        {
            audioManager.audioMixer.SetFloat("Game Volume", -40f + (volumeValue / 2.25f));
            newVolume2 = -40f + (volumeValue / 2.25f);
        }
        player.gameVolume = newVolume2;
        player.gameVolumeCore = volumeValue;
    }

    public void MusicVolume()
    {
        float volumeValue = musicSlider.value;
        if (volumeValue == 0)
        {
            audioManager.audioMixer.SetFloat("Music Volume", -80);
            newVolume3 = -80;
        }

        else
        {
            audioManager.audioMixer.SetFloat("Music Volume", -40f + (volumeValue / 2.25f));
            newVolume3 = -40f + (volumeValue / 2.25f);
        }
        player.musicVolume = newVolume3;
        player.musicVolumeCore = volumeValue;
    }

    public void MenuVolume()
    {
        float volumeValue = menuSlider.value;
        if (volumeValue == 0)
        {
            audioManager.audioMixer.SetFloat("Menu Volume", -80);
            newVolume4 = -80;
        }

        else
        {
            audioManager.audioMixer.SetFloat("Menu Volume", -40f + (volumeValue / 2.25f));
            newVolume4 = -40f + (volumeValue / 2.25f);
        }
        player.menuVolume = newVolume4;
        player.menuVolumeCore = volumeValue;
    }
}
