using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerLink : MonoBehaviour
{
    public UIManager uiManager;
    public RectTransform rectTransform;
    // Start is called before the first frame update
    void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
       // rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ManagerLink()
    {
        if(rectTransform==null)
        {
            print("uh oh");
        }

        //uiManager.SnapToHorizontal(rectTransform);
    }
}
