using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//author: Kingyan
/// <summary>
/// text displaying the value of a slider
/// </summary>
public class SliderText : MonoBehaviour
{
    public Slider slider;
    //text that needs to display the value of the slider
    public TextMeshProUGUI coolText;

    int textValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textValue = (int)slider.value;

        coolText.text = textValue.ToString();
    }
}
