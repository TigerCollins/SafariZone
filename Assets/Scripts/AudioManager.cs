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
    public float footstepsMinPitch;
    public float footstepsMaxPitch;
    public float footstepsCurrentPitch;
    public float dialogueMinPitch;
    public float dialogueMaxPitch;
    public float dialogueCurrentPitch;

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

    [Header("Menu SFX")]
    public AudioSource menuHover;
    public AudioSource menuClick;

    [Header("Dialogue SFX")]
    public AudioClip dialogueClip;
    public AudioSource dialogueSource;

    public static AudioManager Instance;
    private int previousRoute;

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

        if (currentScene!= "GameWorld")
        {
            soundtrackSource.clip = menuMusic;
            inMenu = false;
            ChangeLowPass();
        }

        soundtrackSource.Play();


    }

    public void ChangeRouteSoundtrackClip(int routeNum,bool dontChangeTrack)
    {
        if(dontChangeTrack == false)
        {
            soundtrackSource.clip = routeSoundtrack[routeNum];
            soundtrackSource.Play();
            previousRoute = routeNum;
        }

        else
        {
           // soundtrackSource.clip = routeSoundtrack[0];
        }
        
       
    }

    IEnumerator ChangePitch()
    {
        yield return new WaitForSecondsRealtime(.5f);
        footstepsCurrentPitch = Random.Range(footstepsMinPitch, footstepsMaxPitch);
        dialogueCurrentPitch = Random.Range(dialogueMinPitch, dialogueMaxPitch);
        if (currentScene == "GameWorld")
        {
            StartCoroutine(ChangePitch());
        }

    }

    public void OneShotFootsteps()
    {
        footstep.pitch = footstepsCurrentPitch;
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

    public void OneShotDialogue()
    {
        dialogueSource.pitch = dialogueCurrentPitch;
        dialogueSource.clip = dialogueClip;
        dialogueSource.Play();
     

    }

    private void OnLevelWasLoaded(int sceneBuildIndex)
    {
        previousRoute = 0;
       
        currentScene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex).name;
        soundtrackSource = transform.GetChild(0).GetComponent<AudioSource>();

        if (currentScene != "GameWorld")
        {
            inMenu = false;
            soundtrackSource.clip = menuMusic;
            if(PlayerPrefs.GetInt("Play Song")==1)
            {
                soundtrackSource.Play();
                PlayerPrefs.SetInt("Play Song", 0);
            }
            
        }

        else
        {
            inMenu = false;
            soundtrackSource.clip = routeSoundtrack[PlayerPrefs.GetInt("AreaID")];
            soundtrackSource.Play();
            ChangeRouteSoundtrackClip(PlayerPrefs.GetInt("AreaID"), false);
         //.   menuHover = GameObject.Find
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
