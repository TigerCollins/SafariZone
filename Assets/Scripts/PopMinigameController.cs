using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopMinigameController : MonoBehaviour
{
    public GameController scriptController;

    public Creature spawnedCreature;
    private float awakeCaptureTimer;
    public float baseCaptureTimer = 1f;
    public float captureTimerMultiplier;
    public float captureTimer;

    public GameObject spawnpointContainer;
    public Transform[] spawnpoints;

    [Header("UI Variables")]
    public Image timerBar;
    public Animator animator;

    [Header("Pop Icon Variables")]
    public GameObject balloonButtonPrefab;
    public int baseSpawned;
    public int totalPopped;
    public GameObject popIconHolder;


    // Start is called before the first frame update
    void Start()
    {
        
        //SpawnPopIcons();
    }

    // Update is called once per frame
    void Update()
    {

            captureTimer -= Time.deltaTime;
            timerBar.fillAmount = captureTimer / awakeCaptureTimer;
            TimerEvents();
            //animator.SetBool("IsOpen", false);

        
    }

    public void StartTutorial()
    {
        animator.SetBool("IsOpen", true);
        //StartCoroutine(TutorialCountdown());
    }

    public void CloseTutorial()
    {

            animator.SetBool("IsOpen", false);
        
    }

    void SpawnPopIcons()
    {
        spawnpoints = spawnpointContainer.GetComponentsInChildren<Transform>();
        List<Transform> freeSpawnPoints = new List<Transform>(spawnpoints);
        int i = 0;
        for (i = 0; i < baseSpawned; i++)
        {
            if (freeSpawnPoints.Count <= 0)
            {
                return; // Not enough spawn points
            }  
            int index = Random.Range(0, freeSpawnPoints.Count);
            Transform pos = freeSpawnPoints[index];
            freeSpawnPoints.RemoveAt(index); // remove the spawnpoint from our temporary list
            GameObject Button = Instantiate(balloonButtonPrefab, pos.position, pos.rotation);
            Button.transform.SetParent(gameObject.transform);
        }
    }

    public void UpdateMiniGame()
    {
        totalPopped = 0;
        //UpdateTimer(0);
        awakeCaptureTimer = captureTimer;
        spawnedCreature = scriptController.selectedCreature;
        SpawnPopIcons();

        // spawnedCreature.creatureRarity;
    }

    void TimerEvents()
    {
        //minigame failed
        if(captureTimer < 0)
        {
            scriptController.playerScript.canMove = true;
            this.gameObject.SetActive(false);
        }
    }

    public void PopIcon()
    {
        totalPopped += 1;
        if(totalPopped >= 5)
        {
            spawnedCreature.previouslyCaptured = true;
            spawnedCreature.totalCaught += 1;
            scriptController.currencyScript.wallet += spawnedCreature.price;
            if(spawnedCreature.dateFirstCaught == null)
            {
                spawnedCreature.dateFirstCaught = System.DateTime.Now.ToShortDateString();
            }
            scriptController.currencyScript.AddToLocalWallet(spawnedCreature.price);
            if(spawnedCreature.currentWeight >= spawnedCreature.heaviestCaught)
            {
                spawnedCreature.heaviestCaught = spawnedCreature.currentWeight;
            }

            if (spawnedCreature.currentWeight <= spawnedCreature.lightestCaught)
            {
                spawnedCreature.lightestCaught = spawnedCreature.currentWeight;
            }
            scriptController.CapturePopup(spawnedCreature);
            spawnedCreature = null;

                scriptController.playerScript.canMove = true;
            this.gameObject.SetActive(false);
        }
    }

    public void ClosePopIcon(GameObject thisObject)
    {
        thisObject.SetActive(false);
    }

    public void InitialisePopIcon()
    {
        foreach (Transform child in popIconHolder.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void UpdateTimer(float timer, float itemMultiplier)
    {
        captureTimer = baseCaptureTimer - timer; ;
    }

    public void ResetMinigame()
    {
        
    }
}
