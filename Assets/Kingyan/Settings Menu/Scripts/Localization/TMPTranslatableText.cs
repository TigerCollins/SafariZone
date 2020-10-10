using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//author: Kingyan
/// <summary>
/// translates TextMeshPro text
/// </summary>
public class TMPTranslatableText : MonoBehaviour
{
    //variable from txt file
    public string TextId;

    // Use this for initialization
    void Start()
    {
        var text = GetComponent<TextMeshProUGUI>();
        if (text != null)
            if (TextId == "ISOCode")
                text.text = I18n.GetLanguage();
            else
                text.text = I18n.Fields[TextId];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
