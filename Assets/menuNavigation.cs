using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuNavigation : MonoBehaviour
{
    public SceneManager sceneManager;
    public Input escapeKey;

    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnMenu();
        }

    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(sceneName);
    }
}
