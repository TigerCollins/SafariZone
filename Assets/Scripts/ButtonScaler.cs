using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScaler : MonoBehaviour
{
    public float changeTime;
    public float smallSizePercentage;
    public float largeSizePercentage;
    private Vector3 originalSize;
    private bool canChangeSize = true;
    public GameObject buttonObject;
    private Toggle toggle;

    public void Awake()
    {
        buttonObject = gameObject;
        originalSize = buttonObject.transform.localScale;
        
        if(buttonObject.GetComponent<Toggle>() != null)
        {
            toggle = buttonObject.GetComponent<Toggle>();
        }
    }

    public void MakeButtonSmall()
    {
        if (canChangeSize)
        {
            canChangeSize = false;
            buttonObject.transform.localScale -= buttonObject.transform.localScale * (smallSizePercentage / 100);
            if (buttonObject.activeInHierarchy)
            {
                StartCoroutine("ReturnButtonToOriginalSize");
            }
        }

    }

    public void MakeToggleChange()
    {
        if(toggle != null)
        {
            if (canChangeSize && toggle.isOn == true)
            {
                canChangeSize = false;
                buttonObject.transform.localScale -= buttonObject.transform.localScale * (smallSizePercentage / 100);
                StartCoroutine("ReturnButtonToOriginalSize");
            }
        }

    }


    IEnumerator ReturnButtonToOriginalSize()
    {
        yield return new WaitForSecondsRealtime(changeTime);
        if(buttonObject.activeInHierarchy)
        {
            buttonObject.transform.localScale = originalSize;
            canChangeSize = true;
        }

    }
}
