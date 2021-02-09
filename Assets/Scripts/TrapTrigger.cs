using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public enum SpawnType { HuntingSpot, TrapSpot, FishingSpot }
    private GameObject thisObject;



    [Header("Script References")]
    public GameController scriptController;
    public TrapManager trapManager;
    public FirstPlaythrough firstPlaythrough;

    [Header("Creature Variables")]
    public float smallWeightFactor;
    public float largeWeightFactor;
    public List<Creature> possibleCreatures = new List<Creature>();
    public List<Creature> spawnList;
    private Creature spawnedCreature;


    [Header("Trap Variables")]
    public TrapObject trapObject;
    public bool creatureSet;
    public bool trapPlaced;
    public float trapTimer;
    public float baseTrapTimer;
    private float baseDistanceTravelled;
    public SpawnType spawnType;
    public Lure usedTrap;
    public string[] trapMessage;
    public string[] failedTrapMessage;
    public string[] encounterMessage;

    [Header("Bubble Variables")]
    public GameObject player;
    public bool canSee;
    public float distance;
    public Animator animatorBubble;
    public Renderer renderer;
    public Material exclamationMark;
    public Material lureSymbol;
    public Material runningMark;

    private Vector3 lastPosition;

    public void Start()
    {
        trapManager = FindObjectOfType<TrapManager>();
        thisObject = gameObject;
        BubbleOpened();
        renderer.material = lureSymbol;
        player = FindObjectOfType<PlayerController>().gameObject;
        firstPlaythrough = FindObjectOfType<FirstPlaythrough>().GetComponent<FirstPlaythrough>();
        scriptController = FindObjectOfType<GameController>().GetComponent<GameController>();

        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < distance)
        {
            canSee = true;
        }
        if (canSee == true)
        {
            BubbleOpened();
        }
    }

    public void BubbleClosed()
    {

        animatorBubble.SetBool("Closed", true);
        animatorBubble.SetBool("Triggered", false);
    }

    public void BubbleOpened()
    {
        if (canSee == true)
        {
            animatorBubble.SetBool("Closed", false);
            animatorBubble.SetBool("Triggered", true);
        }
    }

    public void CheckIfTrapPlaced()
    {

        if (trapPlaced)
        {
            TriggerTrap();
            if (firstPlaythrough.localFirstTime == true)
            {
                firstPlaythrough.trapTutTriggered = false;
                firstPlaythrough.trapTutCompleted = true;
            }
        }
        else
        {
            if (scriptController.equippedLure)
            {
                BubbleClosed();
                trapManager.placedTrap = scriptController.equippedLure;
                usedTrap = trapManager.placedTrap;
                trapManager.UpdateText(usedTrap.itemName, trapMessage[0], trapMessage[1]);
                trapPlaced = true;
                PlaceTrap();
                trapObject.TrapPlaced();
                BubbleClosed();
            }
            else
            {
                trapManager.UpdateFailedText(failedTrapMessage[0]);
                trapManager.OpenTrapMenu();
                trapObject.TrapReset();
                BubbleOpened();
                renderer.material = lureSymbol;
            }
        }
    }

    public void PlaceTrap()
    {
        trapManager.OpenTrapMenu();
        
        if (trapPlaced)
        {
            BubbleClosed();
            if (trapManager.placedTrap.canHaveMultiple)
            {
                trapManager.placedTrap.quantity -= 1;
                if(trapManager.placedTrap.quantity < 1 && trapManager.placedTrap.itemName != "Catching Net")
                {
                    scriptController.equippedLure = null;

                }
            }
            trapTimer = baseTrapTimer + (usedTrap.trapEffectiveness * 10);

            for (int i = 0; i < possibleCreatures.Count; i++)
            {
                for (int ii = 0; ii < possibleCreatures[i].spawnRate; ii++)
                {
                    spawnList.Add(possibleCreatures[i]);
                }
            }
            SetCreature();
        }

        else
        {

        }
       
    }

    public void LaunchMinigame(Creature spawnedCreature)
    {
        if(trapPlaced)
        {
            if(firstPlaythrough.localFirstTime)
            {
                firstPlaythrough.trapTutTriggered = false;
                firstPlaythrough.trapTutCompleted = true;
            }
            trapManager.SpawnCreature(spawnedCreature);
            trapPlaced = false;
        }

    }

    public void SetCreature()
    {
        if (!creatureSet && spawnList.Count != 0)
        {
            creatureSet = true;
            spawnedCreature = spawnList[Random.Range(0, spawnList.Count)];
            spawnedCreature.currentWeight = Random.Range((spawnedCreature.minWeight - (spawnedCreature.minWeight * smallWeightFactor)), (spawnedCreature.maxWeight + (spawnedCreature.maxWeight * largeWeightFactor)));
        }
    }

    public void TriggerTrap()
    {
        if (trapTimer <= 0 && creatureSet && trapPlaced) 
        {
            trapManager.UpdateTrapEncounter(spawnedCreature, thisObject,encounterMessage[0], encounterMessage[1]);
            usedTrap = null;
            trapPlaced = false;
            creatureSet = false;
            trapObject.TrapReset();
            renderer.material = lureSymbol;
            FindObjectOfType<PlayerController>().trapTrigger = null;
        }
   }

    public void Update()
    {
        TrapTimer();
        SetWeightFactor();
        if (trapTimer <= 0 && !trapObject.isAnimated && creatureSet)
        {
            trapObject.TrapCaught();
        }

        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < distance)
        {
            canSee = true;
        }

        else
        {
            canSee = false;
            BubbleClosed();
        }
    }

    void TrapTimer()


   
    {
        if(trapTimer > 0)
        {
            trapTimer -= Vector3.Distance(scriptController.playerObject.transform.position, lastPosition);
            lastPosition = scriptController.playerObject.transform.position;
            if (trapPlaced)
            {
                renderer.material = runningMark;
            }
        }

        else
        {
            lastPosition = scriptController.playerObject.transform.position;
            BubbleOpened();
            if(trapPlaced)
            {
                renderer.material = exclamationMark;
            }
        }
    }


    //BAD OPTIMISATION
    void SetWeightFactor()
    {
        float incenseSmallFactor = 0;
        float incenseLargeFactor = 0;
        float whistleSmallFactor = 0;
        float whistleLargeFactor = 0;
        float lureSmallFactor = 0;
        float lureLargeFactor = 0;

        if(scriptController.activeIncense)
        {
            incenseSmallFactor = scriptController.activeIncense.smallWeightFactor;
            incenseLargeFactor = scriptController.activeIncense.largeWeightFactor;
        }

        if (scriptController.activeWhistle)
        {
            whistleSmallFactor = scriptController.activeWhistle.smallWeightFactor;
            whistleLargeFactor = scriptController.activeWhistle.largeWeightFactor;
        }

        if(scriptController.equippedLure)
        {
            lureSmallFactor = scriptController.equippedLure.smallWeightFactor;
            lureLargeFactor = scriptController.equippedLure.largeWeightFactor;
        }

            smallWeightFactor = incenseSmallFactor + whistleSmallFactor + lureSmallFactor;
        largeWeightFactor = incenseLargeFactor + whistleLargeFactor  + lureLargeFactor;

    }
}
