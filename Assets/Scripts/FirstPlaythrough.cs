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

    [Header("Interact Intro")]
    public bool interactionTriggered;
    public CanvasGroup interactionCanvasGroup;
    public bool interactionCompleted;
    public bool triggeredInteractionDialogue;


    [Header("Fishing Tut")]
    public bool fishingTriggered;
    public CanvasGroup fishingCanvasGroup;
    public bool fishingCompleted;
    public bool triggeredFishingDialogue;
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
            StartFishingTutorial();
            StartInteractionTutorial();
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
        
        yield return new WaitForSeconds(.1f);
            professorDialogueTrigger.TriggerDialogue();
        
    }

    public void StartFishingTutorial()
    {
        if (fishingTriggered)
        {
            if (triggeredFishingDialogue != true)
            {
                fishingCanvasGroup.alpha += Time.deltaTime;
                if (fishingCanvasGroup.alpha >= 1)
                {
                    triggeredFishingDialogue = true;
                }
            }

        }

        else
        {
            if (fishingCanvasGroup.alpha <= 0)
            {
                fishingCompleted = true;
            }

            else
            {
                fishingCanvasGroup.alpha -= Time.deltaTime;
            }
        }
    }

    public void StartInteractionTutorial()
    {
        if (interactionTriggered)
        {
            if (triggeredInteractionDialogue != true)
            {
                interactionCanvasGroup.alpha += Time.deltaTime;
                if (interactionCanvasGroup.alpha >= 1)
                {
                    triggeredInteractionDialogue = true;
                }
            }

        }

        else
        {
            if (interactionCanvasGroup.alpha <= 0)
            {
                interactionCompleted = true;
            }

            else
            {
                interactionCanvasGroup.alpha -= Time.deltaTime;
            }
        }
    }
}
