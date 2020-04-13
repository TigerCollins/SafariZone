using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopMinigameButton : MonoBehaviour
{
    public PopMinigameController popMinigameController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PopIcon()
    {
        popMinigameController = GameObject.Find("Pop Minigame").GetComponent<PopMinigameController>();
        popMinigameController.PopIcon();
    }

    public void ClosePopIcon()
    {
        popMinigameController.ClosePopIcon(gameObject);
    }

    public void CloseTutorial()
    {
        popMinigameController.CloseTutorial();
    }
}

