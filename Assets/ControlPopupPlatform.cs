using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPopupPlatform : MonoBehaviour
{
    public PlatformDetection platform;
    public GameObject[] controllerObjects;
    public GameObject[] keyboardObjects;

    // Start is called before the first frame update
    void Awake()
    {
        platform = FindObjectOfType<PlatformDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if(platform.controllerInput)
        {
            for (int i = 0; i < controllerObjects.Length; i++)
            {
                if(controllerObjects[i].activeInHierarchy == false)
                {
                    for (int ii = 0; ii < keyboardObjects.Length; ii++)
                    {
                        keyboardObjects[ii].SetActive(false);
                    }
                  
                    controllerObjects[i].SetActive(true);
                   
                }
            }
        }

        if (platform.controllerInput == false)
        {
            for (int i = 0; i < keyboardObjects.Length; i++)
            {
                if (keyboardObjects[i].activeInHierarchy == false)
                {
                    for (int ii = 0; ii < controllerObjects.Length; ii++)
                    {
                        controllerObjects[ii].SetActive(false);
                    }
                    
                    keyboardObjects[i].SetActive(true);
                   
                   
                }
            }
        }    
    }
}
