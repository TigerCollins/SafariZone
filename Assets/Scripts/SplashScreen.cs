using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{

    public PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            playerData.controllerInput = false;
            ChangeScene();
        }

       // if()
    }

    void ChangeScene()
    {
        if (playerData.firstTime == true)
        {
            SceneManager.LoadScene(2);
        }

        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
