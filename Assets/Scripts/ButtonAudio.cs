using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler
{
    public AudioManager audioManager;
    public AudioClip audioClip;

    void Start()
    {
        if(GameObject.Find("AudioManager")!=null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }

        else
        {
            Debug.LogWarning("Couldn't find Audio Manager script");
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(audioClip != null)
        {
            audioManager.OneShotMenuHover(audioClip);
        }

        else
        {
            Debug.LogWarning("No audio clip attached to " + gameObject.transform.name);
        }

    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }
}
