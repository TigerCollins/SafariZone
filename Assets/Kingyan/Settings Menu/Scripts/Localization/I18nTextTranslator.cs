using UnityEngine;
using UnityEngine.UI;

//author:Daniel Erdmann
/// <summary>
/// translates normal unity text
/// </summary>
public class I18nTextTranslator : MonoBehaviour
{
    public string TextId;

    // Use this for initialization
    void Start()
    {
        var text = GetComponent<Text>();
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
