using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class HuntingCreature : MonoBehaviour
{
    public GameController gameController;
    private bool beenDestroyed = false;

    [Header("AI")]
    public GameObject player;
    [SerializeField]
    public bool runningAway;
    public float runAwayTimer;
    [SerializeField]
    public bool noticed;
    public NavMeshAgent navMeshAgent;
    public HuntingZone huntingZone;
    [SerializeField]
    private Vector3 destination;
    public int targetInt;
    public float objectSpeed;
    public float objectSensitivity;
    private float objectSensitivityDivider;
    private int destroyTimer = 5;
    [Header("Creature")]
    public Creature selectedCreature;

    [Header("Audio")]
    public AudioSource bushMovementSource1;
    public AudioSource bushMovementSource2;
    public AudioSource caughtSource;
    public AudioSource lostSource;
    private int timesPlayed;

    // Start is called before the first frame update
    void Awake()
    {
        gameController = GameObject.FindObjectOfType<GameController>().GetComponent<GameController>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        huntingZone = transform.parent.parent.parent.GetComponent<HuntingZone>();
        
        player = GameObject.Find("Player");
        if (selectedCreature != null)
        {
            objectSensitivity = selectedCreature.minigameMultiplier;
            objectSpeed = (selectedCreature.minigameMultiplier / 5f) + 0;
            navMeshAgent.speed = objectSpeed;
            
        }

        
        ChangeTargetPoint();
        GetComponent<HuntingCreature>().enabled = true;

       
    }

    // Update is called once per frame
    void Update()
    {
        //SNEAK UPDATE
        if (player.GetComponent<PlayerController>().isSneaking == true)
        {
            objectSensitivityDivider = 1700;
        }

        else
        {

            objectSensitivityDivider = 1700;
        }

        //START
        if (selectedCreature)
        {
            //HORRIBLE CODE
            objectSensitivity = (((selectedCreature.minigameMultiplier / objectSensitivityDivider)+1)*3);
        }
        MoveObject();
        if (runningAway)
        {
            if(gameController.audioManager.sfx.isPlaying == false && timesPlayed==0)

            {
                timesPlayed += 1;
                gameController.audioManager.OneShotSFX(gameController.creatureSpooked);

            }
            GetComponent<SphereCollider>().enabled = false;
            DropObject();
        }

            if (Vector3.Distance(transform.position, player.transform.position) < objectSensitivity)
        {
            noticed = true;
        }

            if(noticed  == true)
        {
            navMeshAgent.enabled = false;
            
            StartCoroutine("AnimateExclamationMark");
            runAwayTimer -= Time.deltaTime;
            if (runAwayTimer <= 0)
            {
                
                runningAway = true;

            }
        }
    }

    IEnumerator AnimateExclamationMark()
    {
        transform.GetChild(3).GetComponent<Animator>().SetBool("Visible", true);
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(runAwayTimer -(runAwayTimer/3));
        transform.GetChild(3).GetComponent<Animator>().SetBool("Visible", false);
    }

    void DropObject()
    {

        float timeMultiplier = Time.deltaTime;
        gameObject.transform.position -= new Vector3(gameObject.transform.position.x, 1 * timeMultiplier,gameObject.transform.position.z);
        if(!beenDestroyed)
        {
            beenDestroyed = true;
            huntingZone.amountSpawned -= 1;

        }

        Destroy(gameObject, destroyTimer);
    }

    void MoveObject()
    {

        if (!noticed)
        {
            navMeshAgent.destination = destination;

            if (Vector3.Distance(transform.position, huntingZone.spawnPoints[targetInt].position) < 2.0f)
            {

                ChangeTargetPoint();
            }

          
        }

        else
        {
            ChangeTargetPoint();
           // navMeshAgent.destination = destination;
        }

    }

    public void ChangeTargetPoint()
    {
        targetInt = Random.Range(0, huntingZone.spawnPoints.Count);
        if(!runningAway)
        {
            destination = huntingZone.spawnPoints[targetInt].position;
        }

    } 

    public void AnomalyOneShot(float pitch)
    {
        if (bushMovementSource1.isPlaying)
        {
            bushMovementSource2.pitch = pitch;
            bushMovementSource2.Play();
        }

        else
        {
            bushMovementSource1.pitch = pitch;
            bushMovementSource1.Play();
        }
    }

}
