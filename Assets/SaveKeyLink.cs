using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveKeyLink : MonoBehaviour
{
    private SaveKeyBind saveKey;
    // Start is called before the first frame update
    void Start()
    {
        saveKey = FindObjectOfType<SaveKeyBind>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisablePlayerInput()
    {
        saveKey.DisablePlayerInput();

    }

    public void SaveValues()
    {
        saveKey.StoreControlOverrides();

    }
}
