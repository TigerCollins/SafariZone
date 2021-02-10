using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PopMinigameController : MonoBehaviour
{
    public GameController scriptController;

    public EventSystem eventSystem;
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
    [SerializeField]
    private int totalSpawned = 0;
    public GameObject popIconHolder;
    public PopMinigameButton pmb;
    public int currentButtonPress;
    public int neededButtonInt;
    



    // Start is called before the first frame update
    public void Start()
    {
        SelectRandomBubble();
        eventSystem = FindObjectOfType<EventSystem>();
        totalSpawned = 0;
        StartTutorial();
    }

    public void OnEnable()
    {
        
    }

    //public void Ac

    // Update is called once per frame
    void Update()
    {

            captureTimer -= Time.deltaTime;
            timerBar.fillAmount = captureTimer / awakeCaptureTimer;
            TimerEvents();
       // animator.SetBool("IsOpen", false);
        SelectRandomBubble();


    }

    public void StartTutorial()
    {
        //if(FindObjectOfType<FirstPlaythrough>().playerProfile.firstTime == false)
     //   {
            animator.SetBool("IsOpen", true);
            StartCoroutine(TutorialCountdown());
      //  }

    }

    IEnumerator TutorialCountdown()
    {
        yield return new WaitForSeconds(2f);
       // CloseTutorial();

    }

    public void CloseTutorial()
    {

            animator.SetBool("IsOpen", false);
        
    }

    //TRANSFORM.PARENT.PARENT

    void SpawnPopIcons()
    {
        spawnpoints = spawnpointContainer.GetComponentsInChildren<Transform>();
        totalSpawned = 0;
        List<Transform> freeSpawnPoints = new List<Transform>(spawnpoints);
        int i = 0;
        for (i = 0; i <= baseSpawned; i++)
        {
            if (freeSpawnPoints.Count <= 0)
            {
                return; // Not enough spawn points
            }  
            int index = Random.Range(0, freeSpawnPoints.Count);
            Transform pos = freeSpawnPoints[index];
            freeSpawnPoints.RemoveAt(index); // remove the spawnpoint from our temporary list
            GameObject Button = Instantiate(balloonButtonPrefab, pos.position, pos.rotation);
            Button.transform.SetParent(gameObject.transform.GetChild(2).transform);
            totalSpawned += 1;
            if (totalSpawned >= baseSpawned -1)
            {
                SelectRandomBubble();
                return;
            }


        }
        
    }

    public void UpdateMiniGame()
    {
        totalPopped = 0;
        //UpdateTimer(0);
        awakeCaptureTimer = captureTimer;
        spawnedCreature = scriptController.selectedCreature;
        SpawnPopIcons();
       // SelectRandomBubble();
        // spawnedCreature.creatureRarity;
    }

    void TimerEvents()
    {
        //minigame failed
        if(captureTimer < 0f)
        {
            ResetMinigame();
            scriptController.playerScript.canMove = true;
           
        }
    }

    public void PopIcon()
    {
       

        if(scriptController.platformDetection.controllerInput == true && pmb.canBeDestroyedController == true)
        {
            //  if(currentButtonPress == neededButtonInt)
            ////   {
            ///
            scriptController.gameoverController.CaughtCreature(spawnedCreature);
            totalPopped += 1;
                Destroy(pmb.gameObject);

                if (totalPopped >= 5)
                {
                    spawnedCreature.previouslyCaptured = true;
                    spawnedCreature.totalCaught += 1;
                    if (spawnedCreature.dateFirstCaught == null)
                    {
                        spawnedCreature.dateFirstCaught = System.DateTime.Now.ToShortDateString();
                    }
                    scriptController.currencyScript.AddToLocalWallet(spawnedCreature.price);
                    if (spawnedCreature.currentWeight >= spawnedCreature.heaviestCaught)
                    {
                        spawnedCreature.heaviestCaught = spawnedCreature.currentWeight;
                    }

                    if (spawnedCreature.currentWeight <= spawnedCreature.lightestCaught)
                    {
                        spawnedCreature.lightestCaught = spawnedCreature.currentWeight;
                    }
                    scriptController.CapturePopup(spawnedCreature);
                    spawnedCreature = null;
                    scriptController.statTracker.creaturesCaught++;

                    scriptController.playerScript.canMove = true;
                    ResetMinigame();
                    gameObject.SetActive(false);
           
            }
                pmb = null;
                SelectRandomBubble();

         //   }

          
        }

        else
        {
            totalPopped += 1;
            Destroy(pmb.gameObject);

            if (totalPopped >= 5)
            {
                scriptController.gameoverController.CaughtCreature(spawnedCreature);
                spawnedCreature.previouslyCaptured = true;
                spawnedCreature.totalCaught += 1;
                if (spawnedCreature.dateFirstCaught == null)
                {
                    spawnedCreature.dateFirstCaught = System.DateTime.Now.ToShortDateString();
                }
                scriptController.currencyScript.AddToLocalWallet(spawnedCreature.price);
                if (spawnedCreature.currentWeight >= spawnedCreature.heaviestCaught)
                {
                    spawnedCreature.heaviestCaught = spawnedCreature.currentWeight;
                }

                if (spawnedCreature.currentWeight <= spawnedCreature.lightestCaught)
                {
                    spawnedCreature.lightestCaught = spawnedCreature.currentWeight;
                }
                scriptController.CapturePopup(spawnedCreature);
                spawnedCreature = null;
                scriptController.statTracker.creaturesCaught++;
                


                scriptController.playerScript.canMove = true;
                ResetMinigame();
                gameObject.SetActive(false);

            }
            pmb = null;
            SelectRandomBubble();
        }



    }

    public void ClosePopIcon()
    {
       // Destroy(gameObject);
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
       
        PopMinigameButton pmb;
        GameObject theSelectedButton = GameObject.Find("Bubble QTE(Clone)");

        totalSpawned = 0;
        totalPopped = 0;
        gameObject.SetActive(false);
        for (int i = 0; i < baseSpawned + 1; i++)
        {
            if(theSelectedButton != null && GameObject.Find("Bubble QTE(Clone)")!=null)
            {
                pmb = GameObject.Find("Bubble QTE(Clone)").GetComponent<PopMinigameButton>();
            }
            else
            {
              
                return;
            }
           
            if (pmb !=null)
            {
                print("YAY IT DESTROYED SHIZ");
                Destroy(pmb.gameObject);
            }


          
        }
   

    }

    public void SelectRandomBubble()
    {
        if(pmb == null)
        {
            if(GameObject.Find("Bubble QTE(Clone)"))
            {
                pmb = GameObject.Find("Bubble QTE(Clone)").GetComponent<PopMinigameButton>();
            }
         
        } 
      
        if(pmb != null)
        {
            //if(totalSpawned <= baseSpawned)
           // {
           if(pmb.platformDetection.controllerInput == true && pmb.gameObject!=null)
            {
                eventSystem.SetSelectedGameObject(pmb.gameObject);
            }
                
            ///}

        }

        else
        {
            Debug.LogWarning("COULD NOT FIND A QTE BUBBLE");
        }
       
    }
}
