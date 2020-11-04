using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneClickToggle : MonoBehaviour
{
    public PlatformDetection platformDetection;
    public Toggle currentToggle;
    public Toggle nextToggle;
    public int groupCount;
    // Start is called before the first frame update
    void Awake()
    {
        platformDetection = FindObjectOfType<PlatformDetection>();
        currentToggle = GetComponent<Toggle>();
        groupCount = gameObject.transform.parent.childCount;
        if (gameObject.transform.GetSiblingIndex() < (groupCount - 1))
        {
            nextToggle = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() + 1).GetComponent<Toggle>();
        }

        else
        {
            nextToggle = gameObject.transform.parent.GetChild(0).GetComponent<Toggle>();
        }
    }

    // Update is called once per frame
    public void SelectOtherOption()
    {
        if(platformDetection != null)
        {
            if (platformDetection.controllerInput)
            {
                nextToggle.Select();
            }

        }

    }
}
