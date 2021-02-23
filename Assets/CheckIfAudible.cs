using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfAudible : MonoBehaviour
{
    public AudioListener audioListener;
    public AudioSource audioSource;
    float distanceFromPlayer;
    public bool isAudibleDebug;

    void Start()
    {
     //
        // Finds the Audio Listener and the Audio Source on the object
       audioListener = Camera.main.GetComponent<AudioListener>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, audioListener.transform.position);
    
      //  if(audioSource.spatialBlend !=0)
     //   {
            if (distanceFromPlayer <= audioSource.maxDistance)
            {
                ToggleAudioSource(true);
            }
            else
            {
                ToggleAudioSource(false);
            }
     //   }

    }

    void ToggleAudioSource(bool isAudible)
    {
        if (isAudible == false)
        {
          
            audioSource.mute = !isAudible;
            isAudibleDebug = !isAudible;
        }
        else
            print(gameObject.name + "isn't muted");
        {
            audioSource.mute = !isAudible;
            isAudibleDebug = !isAudible;
        }
    }
}