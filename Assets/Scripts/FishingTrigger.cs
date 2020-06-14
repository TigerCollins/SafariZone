using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingTrigger : MonoBehaviour
{
    public bool isFishing;
    public GameObject thisObject;
    public FirstPlaythrough firstPlaythrough;


    [Header("Script References")]
    public GameController scriptController;
    public PlayerController playerController;
    public TrapManager trapManager;

    [Header("Creature Variables")]
    public float smallWeightFactor;
    public float largeWeightFactor;
    public List<Creature> possibleCreatures = new List<Creature>();
    public List<Creature> spawnList;
    private Creature spawnedCreature;

    [Header("Fishing Variables")]
    public int timeReduction;
    public float minFishingTime;
    public float maxFishingTime;
    private bool stillFishing;

    [Header("Trap Variables")]
    public string[] encounterMessage;
    public string[] failedMessage;

    [Header("Bubble Variables")]
    public GameObject player;
    public float distance;
    public bool visible;
    public Animator animatorBubble;
    public Renderer renderer;
    public Material exclamationMark;
    public Material lureSymbol;


    public void BubbleClosed()
    {
        animatorBubble.SetBool("Closed", true);
        animatorBubble.SetBool("Triggered", false);
    }

    public void BubbleOpened()
    {
        if(visible == true)
        {
            animatorBubble.SetBool("Closed", false);
            animatorBubble.SetBool("Triggered", true);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
        trapManager = FindObjectOfType<TrapManager>();
        playerController = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        firstPlaythrough = FindObjectOfType<FirstPlaythrough>();
        scriptController = FindObjectOfType<GameController>().GetComponent<GameController>();
        player = playerController.gameObject;
        thisObject = gameObject;

        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < distance)
        {
            visible = true;
        }
        if (visible == true)
        {
            BubbleOpened();
        }
    }


    public void AttemptFish()
    {
        visible = false;
        BubbleClosed();
        if (!isFishing)
        {
            firstPlaythrough.fishingTriggered = false;
            stillFishing = true;
            playerController.animator.SetBool("isFishing",true);
            isFishing = true;
            StartCoroutine("FishingTime");
            spawnedCreature = spawnList[Random.Range(0, spawnList.Count)];
           
            
        }

    }

    IEnumerator FishingTime()
    {
        Debug.Log("IEnumerator Started");
        yield return new WaitForSeconds(Random.Range(minFishingTime, maxFishingTime));
        if(stillFishing == true)
        {
            if (spawnedCreature != null)
            {
                trapManager.UpdateFishingEncounter(spawnedCreature, encounterMessage[0], encounterMessage[1]);
            }

            else
            {
                trapManager.UpdateFishingFailed(failedMessage[0]);
            }
            scriptController.distanceTravelled += timeReduction;
            isFishing = false;
            playerController.animator.SetBool("isFishing", false);
            playerController.fishingRod.SetBool("isFishing", false);
            stillFishing = false;
            if (visible == true)
            {
                BubbleOpened();
            }
            
            
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        if(stillFishing == true && Input.GetAxisRaw("Horizontal") != 0 || stillFishing == true && Input.GetAxisRaw("Vertical") != 0)
        {
            stillFishing = false;
            isFishing = false;
            if(visible == true)
            {
                BubbleOpened();
            }

            playerController.animator.SetBool("isFishing", false);
            playerController.fishingRod.SetBool("isFishing", false);
            Debug.Log("Fishing Cancelled");
        }
        BubbleOpened();
        //Check distance
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < distance && isFishing == false)
        {
            visible = true;
            
        }

        else
        {
            visible = false;
            BubbleClosed();
        }
    }
}
