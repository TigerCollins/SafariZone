using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopUIManager : MonoBehaviour
{
    [Header("Toggle Changer / Pages")]
    public Toggle lureToggle;
    public Toggle incenseToggle;
    public Toggle whistlesToggle;

    public GameObject luresContainer;
    public GameObject incenseContainer;
    public GameObject whistlesContainer;
    // Start is called before the first frame update
    void Start()
    {
        CategoryChanger();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CategoryChanger()
    {
        if(lureToggle.isOn)
        {
            luresContainer.SetActive(true);
            incenseContainer.SetActive(false);
            whistlesContainer.SetActive(false);
        }

        if (incenseToggle.isOn)
        {
            incenseContainer.SetActive(true);
            luresContainer.SetActive(false);
            whistlesContainer.SetActive(false);
        }

        if (whistlesToggle.isOn)
        {
            whistlesContainer.SetActive(true);
            luresContainer.SetActive(false);
            incenseContainer.SetActive(false);
        }
    }
}
