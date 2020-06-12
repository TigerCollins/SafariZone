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

     [Header("Hunting Tut")]
    public bool huntingTriggered;
    public CanvasGroup huntingCanvasGroup;
    public bool huntingCompleted;
    public bool triggeredHuntingDialogue;

    [Header("Trap Tut")]
    public bool trapTutTriggered;
    public CanvasGroup trapTutCanvasGroup;
    public bool trapTutCompleted;
    public bool triggeredTrapDialogue;

    [Header("TitleCard")]
    public bool titlecardTriggered;
    public CanvasGroup titlecardCanvasGroup;
    public float titlecardTimer;
    public bool titlecardCompleted;
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
            StartHuntingTutorial();
            StartTrapTutorial();
            StartTitleCard();
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

    public void StartHuntingTutorial()
    {
        if (huntingTriggered)
        {
            if (triggeredHuntingDialogue != true)
            {
                huntingCanvasGroup.alpha += Time.deltaTime;
                if (huntingCanvasGroup.alpha >= 1)
                {
                    triggeredHuntingDialogue = true;
                }
            }

        }

        else
        {
            if (huntingCanvasGroup.alpha <= 0)
            {
                huntingCompleted = true;
            }

            else
            {
                huntingCanvasGroup.alpha -= Time.deltaTime;
            }
        }
    }

    public void StartTrapTutorial()
    {
        if (trapTutTriggered)
        {
            if (triggeredTrapDialogue != true)
            {
                trapTutCanvasGroup.alpha += Time.deltaTime;
                if (trapTutCanvasGroup.alpha >= 1)
                {
                    triggeredTrapDialogue = true;
                }
            }

        }

        else
        {
            if (trapTutCanvasGroup.alpha <= 0)
            {
                trapTutCompleted = true;
            }

            else
            {
                trapTutCanvasGroup.alpha -= Time.deltaTime;
            }
        }
    }

    public void StartTitleCard()
    {
        if (titlecardTriggered)
        {
            if (titlecardCompleted != true)
            {
                titlecardCanvasGroup.alpha += Time.deltaTime;
                if (titlecardCanvasGroup.alpha >= 1)
                {
                    titlecardCompleted = true;
                }
            }

        }


            if (titlecardCanvasGroup.alpha > 0 && titlecardTimer <= 0)
            {
                titlecardCanvasGroup.alpha -= Time.deltaTime;
            }



        if(titlecardCompleted)
        {
            titlecardTimer -= Time.deltaTime;
            if(titlecardTimer <= 0)
            {
                titlecardTriggered = false;
            }
        }
    }
}
