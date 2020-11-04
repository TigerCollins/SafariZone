using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public Text toggleLabel;
    public Toggle toggle;
    public Color selectedColour;
    public Color deselectedColour;

    // Start is called before the first frame update
    void Start()
    {
        toggleLabel = gameObject.GetComponentInChildren<Text>();
        toggle = GetComponent<Toggle>();
        ChangeButtonColour();
        toggle.group = transform.parent.GetComponent<ToggleGroup>();
    }

    // Update is called once per frame
    public void ChangeButtonColour()
    {
        if(toggle)
        {
            if (toggle.isOn)
            {
                toggleLabel.color = selectedColour;
            }

            else
            {
                toggleLabel.color = deselectedColour;
            }
        }
        
    }
}
