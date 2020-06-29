using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject playerObject;
    public PlayerController playerScript;
    public UseItem useItem;
    public Button buyButton;
    public List<Creature> allCreatures;
    public List<Item> allItems;
    public Currency currencyScript;
    public PlayerData playerData;
    public StatTracker statTracker;
    public List<Lure> completeLureList;
    public AreaIdentifier areaIdentifier;
    public AudioManager audioManager;
    public MenuPrefabController menuPrefabController;
    public float timePlayed;
    public Animator menuAnimation;
    public Animator metaAnimator;
    public Text metaText;
    public string metaString; //FEED INTO HERE
    public Inventory inventoryScript;
    public CharacterController characterController;

    [Header("Game Variables")]
    [SerializeField]
    private float maxDistance;
    public bool gamePaused = false;
    public GameObject screenTransition;
    public Vector3 newScreenTransitionSize;
    private bool sceneTransitionBool;
    public float gameOverTransitionTime;

    [Header("Player Variables")]
    public bool distanceCounted;
    [SerializeField]
    public float distanceTravelled;
    private Vector3 lastPosition;
    public string currentArea;
    public float distanceMultiplier;



    [Header("Minigame Variable")]
    public Creature selectedCreature;
    public GameObject popMinigame;
    public Text popMinigameCreatureText;
    public Image popMinigameCreatureSprite;
    private Animator initialWalkAnimator;

    [Header("Item Variable")]
    public Color invisibleInk;
    public Color visibleInk;
    public Lure equippedLure;
    public Image lureIconHolder;
    public Image lureIconBG;
    public Item activeIncense;
    public float incenseTimeRemaining;
    public Image incenseIconHolder;
    public Image incenseIconBG;
    public Item activeWhistle;
    public float whistleTimeRemaining;
    public Image whistleIconHolder;
    public Image whistleIconBG;

    [Header("Captured Popup")]
    public Text creatureText;
    public Text creatureValueText;
    public Image creatureSprite;
    public GameObject creatureTextObject;

    [Header("Area Popup")]
    public Animator animator;
    public Text areaText;
    public int areaID;
    public Sprite areaIcon;
    public string areaName;
    public Text pauseMenuText;
    public Image areaPopupIcon;
    public AreaIdentifier[] areaIdentifierForSpawn;

    [Header("Timer Popup")]
    public GameObject pauseIcon;
    public float midCapacityThreshold;
    public float lowCapacityThreshold;
    public Image timerImage;
    public Image timerImageBG;
    public Color fullCapacity;
    public Color midCapacity;
    public Color lowCapacity;
    public Color pausedColour;
    public Color timerColour;

    [Header("Audio Clips")]
    public AudioClip menuOpenSC;

    [Header("Spawn Zones")]
    public GameObject[] spawnPoints;


    [HideInInspector]
    public int caveChange;

    // Start is called before the first frame update
    void Awake()
    {
        
        menuPrefabController = GetComponent<MenuPrefabController>();
        playerObject = GameObject.Find("Player");
        playerScript = playerObject.GetComponent<PlayerController>();
        characterController = playerObject.GetComponent<CharacterController>();
        characterController.enabled = false;


        //Needs to be the end of Awake
        // lastPosition = transform.position;




        //Close minigames on startup
        popMinigame.SetActive(false);

        //Reset Sprites
        incenseIconHolder.color = invisibleInk;
        incenseIconBG.fillAmount = 0;
        whistleIconHolder.color = invisibleInk;
        whistleIconBG.fillAmount = 0;

        initialWalkAnimator = playerObject.GetComponent<Animator>();
        if (!equippedLure)
        {
            equippedLure = completeLureList[0];
            lureIconHolder.color = visibleInk;
            lureIconHolder.sprite = equippedLure.icon;
            lureIconHolder.preserveAspect = true;
        }
        if (GameObject.Find("AudioManager").GetComponent<AudioManager>() != null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            audioManager.ChangeRouteSoundtrackClip(PlayerPrefs.GetInt("AreaID"), false);
        }
        SpawnPoints();




    }

    private void Start()
    {
        playerObject = GameObject.Find("Player");
        playerScript = playerObject.GetComponent<PlayerController>();
        audioManager.ChangeRouteSoundtrackClip(PlayerPrefs.GetInt("AreaID"),false);

        SpawnPoints();
    }

    public void TeleportCaveToEgress()
    {
        characterController.enabled = false;
        playerObject.GetComponent<CharacterController>().transform.position = spawnPoints[0].transform.position;
            playerObject.transform.localRotation = spawnPoints[0].transform.rotation;
            areaID = 1;
            playerScript.areaIdentifierID = 1;
            areaIdentifier = areaIdentifierForSpawn[0];
        characterController.enabled = true;

    }

    public void TeleportCaveToGuidance()
    {
        characterController.enabled = false;
        playerObject.GetComponent<CharacterController>().transform.position = spawnPoints[4].transform.position;
        playerObject.transform.localRotation = spawnPoints[4].transform.rotation;
        areaID = 0;
        playerScript.areaIdentifierID = 0;
        areaIdentifier = areaIdentifierForSpawn[3];
        characterController.enabled = true;
     
    }

    public void SpawnPoints()
    {
        if (PlayerPrefs.GetInt("AreaID") == 1)
        {
            playerObject.GetComponent<CharacterController>().transform.position = spawnPoints[0].transform.position;
            playerObject.transform.localRotation = spawnPoints[0].transform.rotation;
            areaID = 1;
            playerScript.areaIdentifierID = 1;
            areaIdentifier = areaIdentifierForSpawn[0];
            characterController.enabled = true;
            Debug.Log("Spawned at first spawn");
        }

        else if (PlayerPrefs.GetInt("AreaID") == 6)
        {
            playerObject.GetComponent<CharacterController>().transform.position = spawnPoints[1].transform.position;
            playerObject.transform.localRotation = spawnPoints[1].transform.rotation;
            areaID = 6;
            playerScript.areaIdentifierID = 6;
            areaIdentifier = areaIdentifierForSpawn[1];
            characterController.enabled = true;
            Debug.Log("Spawned at second spawn");
        }

        else if (PlayerPrefs.GetInt("AreaID") == 9)
        {
            playerObject.GetComponent<CharacterController>().transform.position = spawnPoints[2].transform.position;
            playerObject.transform.localRotation = spawnPoints[2].transform.rotation;
            areaID = 9;
            playerScript.areaIdentifierID = 9;
            areaIdentifier = areaIdentifierForSpawn[2];
            characterController.enabled = true;
            Debug.Log("Spawned at third spawn");
        }

        else if (PlayerPrefs.GetInt("AreaID") == 0)
        {
            playerObject.GetComponent<CharacterController>().transform.position = spawnPoints[3].transform.position;
            playerObject.transform.localRotation = spawnPoints[3].transform.rotation;
            areaID = 0;
            playerScript.areaIdentifierID = 0;
            areaIdentifier = areaIdentifierForSpawn[3];
            characterController.enabled = true;
            Debug.Log("Spawned at forth spawn");
        }

        else
        {
            Debug.LogError("No spawn point could be chosen.");
        }
    }

    public void OpenMetaMenu()
    {
        metaText.text = metaString;
        playerScript.canMove = false;
        metaAnimator.SetBool("IsOpen", true);
    }


    public void CloseMetaMenu()
    {
        playerScript.canMove = true;
        metaAnimator.SetBool("IsOpen", false);
    }

    public void NewArea()
    {
        /*
        if(areaID == 1)
        {
            playerData.egressCave = true;
        }

        if (areaID == 6)
        {
            playerData.kebarVillage = true;
        }

        if (areaID == 9)
        {
            playerData.parharVillage = true;
        }

        if (areaID == 9)
        {
            playerData.parharVillage = true;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (useItem.selectedItem != null)
        {
            if (useItem.selectedItem.itemTypes == Item.ItemTypes.Incense && activeIncense || useItem.selectedItem.itemTypes == Item.ItemTypes.Whistle && activeIncense)
            {
                buyButton.interactable = false;
            }
            else
            {
                buyButton.interactable = true;
            }
        }

        if (distanceTravelled >= maxDistance)
        {
            if (!GetComponent<DialogueManager>().dialogueActive)
            {
                playerScript.canMove = false;
                GetComponent<DialogueTrigger>().TriggerDialogue();
            }

        }

        if(sceneTransitionBool == true)
        {
            //screenTransition.transform.localScale =
            screenTransition.transform.localScale = Vector3.MoveTowards(screenTransition.transform.localScale, newScreenTransitionSize, Time.deltaTime * gameOverTransitionTime);
            if(screenTransition.transform.localScale == newScreenTransitionSize)
            {
                Application.LoadLevel(1);
            }
        }

        Statistics();
        CheckForPause();
        ItemUpdateTime();
        timePlayed += Time.deltaTime;
    }

    public void CapturePopup(Creature creature)
    {
        creatureText.text = creature.name;
        creatureValueText.text = "+" + creature.price.ToString();
        creatureSprite.sprite = creature.creatureIcon;

        StartCoroutine("CloseCaptureText");
    }

    IEnumerator CloseCaptureText()
    {
        yield return new WaitForSeconds(.5f);
        creatureTextObject.GetComponent<Animator>().SetBool("isOn", true);
       // yield return new WaitForSeconds(2);
        //creatureText.GetComponent<Animator>().SetBool("isOn", false);

    }

    public void ItemUpdateTime()
    {
        if (activeIncense)
        {

            incenseIconBG.fillAmount = incenseTimeRemaining / activeIncense.lingerTime;
            incenseTimeRemaining -= Time.deltaTime;
            if (incenseTimeRemaining <= 0)
            {

                activeIncense = null;
                incenseIconHolder.sprite = null;
                incenseIconHolder.color = invisibleInk;
            }
            else
            {
                incenseIconHolder.color = visibleInk;

            }
        }

        if (activeWhistle)
        {
            whistleIconBG.fillAmount = whistleTimeRemaining / activeWhistle.lingerTime;
            whistleTimeRemaining -= Time.deltaTime;
            if (whistleTimeRemaining <= 0)
            {

                activeWhistle = null;
                whistleIconHolder.sprite = null;
                whistleIconHolder.color = invisibleInk;
            }
            else
            {
                whistleIconHolder.color = visibleInk;

            }


        }
        if (equippedLure)
        {
            lureIconHolder.color = visibleInk;
            lureIconHolder.sprite = equippedLure.icon;
            lureIconHolder.preserveAspect = true;
        }
        else
        {
            lureIconHolder.color = invisibleInk;
            lureIconHolder.sprite = null;
            lureIconHolder.preserveAspect = true;
        }


    }
    public void ItemUpdate()
    {
        if (activeIncense)
        {
            incenseIconBG.fillAmount = 1;
            incenseIconHolder.sprite = activeIncense.icon;
            incenseTimeRemaining = activeIncense.lingerTime;

        }

        if (activeWhistle)
        {
            whistleIconBG.fillAmount = 1;
            whistleIconHolder.sprite = activeWhistle.icon;
            whistleTimeRemaining = activeWhistle.lingerTime;

        }


    }

    void CheckForPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == false)
        {
            gameObject.GetComponent<MenuPrefabController>().GoToMainMenuFromGame();
            menuAnimation.SetBool("IsOpen", true);
            //Time.timeScale = 0;
            gamePaused = true;
            audioManager.inMenu = true;
            audioManager.ChangeLowPass();
            audioManager.OneShotMenuClick(menuOpenSC);
        }
    }

    void UpdateCreatureSpawn()
    {
        for (int i = 0; i < allCreatures.Count; i++)
        {
            if (activeIncense)
            {
                //  allCreatures[i].spawnRate + 
            }

            if (activeWhistle)
            {

            }
        }
    }

    void Statistics()
    {
        if (areaID != 1 && areaID != 6 && areaID != 9 && areaID != 0 )
        {
            distanceCounted = true;
            distanceTravelled += (Vector3.Distance(playerObject.transform.position, lastPosition)*distanceMultiplier);

            if (pauseIcon.activeInHierarchy != false)
            {
                pauseIcon.SetActive(false);
            }

            timerImage.fillAmount = (maxDistance - distanceTravelled) / maxDistance;


            //(maxDistance -(maxDistance/10))
            if (distanceTravelled < (maxDistance - (maxDistance / midCapacityThreshold)))
            {
                timerImage.color = fullCapacity;
                timerImageBG.color = timerColour;
            }


            if (distanceTravelled < (maxDistance - (maxDistance / midCapacityThreshold)) && distanceTravelled > (maxDistance - (maxDistance / lowCapacityThreshold)))
            {
                timerImage.color = midCapacity;
                timerImageBG.color = timerColour;
            }
            if (distanceTravelled > (maxDistance - (maxDistance / lowCapacityThreshold)))
            {
                timerImage.color = lowCapacity;
                timerImageBG.color = lowCapacity;
            }

        }
        //Paused timer
        else
        {
            distanceCounted = false;
            timerImage.color = pausedColour;
            if(pauseIcon.activeInHierarchy != true)
            {
                pauseIcon.SetActive(true);
            }
        }

        lastPosition = playerObject.transform.position;

        statTracker.totalDistanceTravelled = distanceTravelled;

       

    }

    public void SpawnCreature()
    {
        popMinigame.GetComponent<PopMinigameController>().UpdateTimer(selectedCreature.minigameMultiplier, 0);
        playerScript.canMove = false;
        popMinigame.SetActive(true);
        popMinigame.GetComponent<PopMinigameController>().StartTutorial();
        popMinigameCreatureText.text = selectedCreature.creatureName;
        popMinigameCreatureSprite.sprite = selectedCreature.creatureIcon;
        popMinigame.GetComponent<PopMinigameController>().UpdateMiniGame();
        popMinigame.GetComponent<PopMinigameController>().InitialisePopIcon();
    }

    public void GameOver()
    {
        screenTransition.SetActive(true);
        sceneTransitionBool = true;
        currencyScript.UpdatePlayerData();
        playerData.runsCompleted += 1;
        playerData.playTime = timePlayed;
        playerObject.GetComponent<PlayerController>().canMove = false;
       
    }

    public void OpenAreaTitle()
    { 
        animator.SetBool("Is On", true);
        areaPopupIcon.sprite = areaIcon;
        StartCoroutine(CloseAreaHeading());
    }

    public IEnumerator CloseAreaHeading()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("Is On", false);
    }
}
