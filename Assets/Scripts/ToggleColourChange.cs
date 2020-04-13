using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleColourChange : MonoBehaviour
{
    public Color unselectedColour;
    public Color selectedColour;
    public Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        if(toggle == null)
        {
            toggle = gameObject.GetComponent<Toggle>();
        }

        StateCheck();
    }

    public void StateCheck()
    {
        if(toggle.isOn == true)
        {
            toggle.targetGraphic.color = selectedColour;
        }

        else
        {
            toggle.targetGraphic.color = unselectedColour;
        }
    }


}
