using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPlaythrough : MonoBehaviour
{
    public PlayerData playerProfile;
    

    [Header("Movement")]
    public bool triggeredMovementDialogue;
    public bool closeMovementDialogue;
    public CanvasGroup movementCanvasGroup;

    [Header("Intro Chat")]
    private bool chatBeenTriggered;
    public GameObject ProfessorNPC;
    public bool chatStarted;
    public DialogueTrigger professorDialogueTrigger;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerProfile.firstTime)
        {
            OpenMovementTutorial();
            CloseMovementTutorial();
            FirstChatCoroutine();
        }
    }

    public void OpenMovementTutorial()
    {
        if(triggeredMovementDialogue != true)
            {
            movementCanvasGroup.alpha += Time.deltaTime;
            if (movementCanvasGroup.alpha >= 1)
            {
                triggeredMovementDialogue = true;
            }
        }
       
    }

    public void CloseMovementTutorial()
    {
        if(closeMovementDialogue == true)
        {
            movementCanvasGroup.alpha -= Time.deltaTime;
        }
      
    }

    public void FirstChatCoroutine()
    {
        if (!chatBeenTriggered && chatStarted)
        {
            StartCoroutine("FirstChat");
            chatBeenTriggered = true;
        }
    }

    IEnumerator FirstChat()
    {
        
        yield return new WaitForSeconds(.5f);
            professorDialogueTrigger.TriggerDialogue();
    }
}
