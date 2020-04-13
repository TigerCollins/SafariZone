using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionToggle : MonoBehaviour
{
    public GameObject toggleContainer;
    public ResolutionDropdown resolutionDropdown;
    public int selectedResNum;
    public int resX;
    public int resY;
    public int hertz;
    public Text textObject;
    // Start is called before the first frame update
    void Start()
    {
        resolutionDropdown = gameObject.transform.parent.GetComponent<ResolutionDropdown>();
        toggleContainer = gameObject.transform.parent.gameObject;
        GetComponent<Toggle>().group = toggleContainer.GetComponent<ToggleGroup>();

        SetResolutionOptions();
    }


    public void SetResolutionOptions()
    {
        textObject.text = resX.ToString() + " x " + resY.ToString() + " @ " + hertz.ToString() + "Hz";
    }

    public void TriggerResolutionChange()
    {
        if(resolutionDropdown!=null)
        {
            resolutionDropdown.SetResolution(resX, resY);
        }
          

    }

    public void IsOn()
    {
        if (resolutionDropdown != null)
        {
            if (GetComponent<Toggle>().isOn && resolutionDropdown.timer < .3f)
            {
                resolutionDropdown.selectedToggle = gameObject.GetComponent<Toggle>();
               //resolutionDropdown.SnapTo(GetComponent<RectTransform>());
            }
        }
       
    }
}
