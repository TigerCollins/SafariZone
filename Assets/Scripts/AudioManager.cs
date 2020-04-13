using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public string currentScene;
    public AudioSource soundtrackSource;
    public AudioSource soundEffectSource;
    public AudioMixer audioMixer;

    [Header("Pitch Effects")]
    public float minPitch;
    public float maxPitch;
    public float currentPitch;

    [Header("Post Effects")]
    public bool inMenu;
    public float lowPassMenuFrequency;
    public float lowPassGameFrequency;

    [Header("Soundtracks")]
    //public int
    public AudioClip[] routeSoundtrack;
    public AudioClip menuMusic;

    [Header("SFX")]
    public AudioSource footstep;

    [Header("SFX")]
    public AudioSource menuHover;
    public AudioSource menuClick;

    public static AudioManager Instance;

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
    }

        // Start is called before the first frame update
        void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        soundtrackSource =  transform.GetChild(0).GetComponent<AudioSource>();
        soundEffectSource = transform.GetChild(1).GetComponent<AudioSource>();
        StartCoroutine(ChangePitch());

        if (currentScene!= "World")
        {
            soundtrackSource.clip = menuMusic;
            inMenu = false;
            ChangeLowPass();
        }

        soundtrackSource.Play();


    }

    public void ChangeRouteSoundtrackClip(int routeNum)
    {
        soundtrackSource.clip = routeSoundtrack[routeNum];
        soundtrackSource.Play();
    }

    IEnumerator ChangePitch()
    {
        yield return new WaitForSecondsRealtime(.5f);
        currentPitch = Random.Range(minPitch, maxPitch);
        if(currentScene == "World")
        {
            StartCoroutine(ChangePitch());
        }

    }

    public void OneShotFootsteps()
    {
        footstep.pitch = currentPitch;
        footstep.Play();
    }

    public void OneShotMenuHover(AudioClip audioClip)
    {
        menuHover.clip = audioClip;
        menuHover.Play();
    }

    public void OneShotMenuClick(AudioClip audioClip)
    {
        menuClick.clip = audioClip;
        menuClick.Play();
    }


    private void OnLevelWasLoaded(int sceneBuildIndex)
    {
        currentScene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex).name; ;

        if (currentScene != "World")
        {
            inMenu = false;
          //  soundtrackSource.clip = menuMusic;
        }

        else
        {
            inMenu = false;
        }

        ChangeLowPass();
    }


    public void ChangeLowPass()
    {
        if(inMenu)
        {

            audioMixer.SetFloat("Music Lowpass", lowPassMenuFrequency);
        }

        else
        {
            audioMixer.SetFloat("Music Lowpass", lowPassGameFrequency);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
