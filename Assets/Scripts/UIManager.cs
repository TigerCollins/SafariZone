using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public EventSystem eventSystem;

    [Header("Main Menu")]
    public GameObject quitMenu;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            Time.timeScale = 1;
        }
    }

   public void OpenQuitMenu(GameObject selectedButton)
    {
        quitMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(selectedButton);
    }
    public  void CloseQuitMenu(GameObject selectedButton)
    {
        quitMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(selectedButton);
    }

    public void LoadNewScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
