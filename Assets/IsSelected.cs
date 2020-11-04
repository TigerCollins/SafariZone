using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsSelected : MonoBehaviour
{
    public PlatformDetection platformDetection;
    public GameObject selectedIcon;
    public UIManager interfaceManager;
    public bool isSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        platformDetection = FindObjectOfType<PlatformDetection>();
        interfaceManager = FindObjectOfType<UIManager>();
       // selectedIcon = transform.parent.GetChild(0).GetChild(0).GetComponent<GameObject>(); 
        if (isSlider)
        {
            selectedIcon = interfaceManager.resolutionSelectedIcon;
           
        }
      //  selectedIcon.SetActive(false);
    }

    // Update is called once per frame
    public void Selected(GameObject selectedIconPublic)
    {
        if(interfaceManager !=null)
        {
            if(isSlider == false)
            {
                interfaceManager.SnapTo(gameObject.transform.parent.GetComponent<RectTransform>());
            }

            else
            {
                interfaceManager.SnapTo(transform.parent.parent.parent.parent.GetComponent<RectTransform>());
             //   print(transform.parent.parent.parent.parent.transform.name);
            }
           
        }
        if(selectedIconPublic != null)
        {
            selectedIcon = selectedIconPublic;
            
        }

        else
        {
            selectedIcon = interfaceManager.resolutionSelectedIcon;
        }
        selectedIcon.SetActive(true);
    }

    public void Deselected()
    {
        selectedIcon.SetActive(false);
    }


}
