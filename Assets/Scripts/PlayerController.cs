using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset input;
    public Backpack playerInventory;
    public GameController scriptControllerObject;
    public TrapTrigger trapTrigger;
    public CharacterController rigidbody;
    public DialogueTrigger dialogueTrigger;
    public DialogueManager dialogueManager;
    public TrapManager trapManager;
    public Animator animator;
    public Animation newAnimation;
    public Animator fishingRod;
    public AreaIdentifier areaIdentifier;
    public int areaIdentifierID;
    public FishingTrigger fishingTrigger;
    public ItemDrop itemDrop;
    public InteractTutorial interactTutorial;
    public Currency currency;
    public LocalAudioManager localAudioManager;

    [Header("Movement Variables")]
    public Vector2 moveAxis;
    public bool crouch;
    public Vector3 desiredMoveDirection;
    public Vector3 sneakDesiredMoveDirection;
    public float desiredRotationSpeed;
    private float originalMovementMultiplier;
    private bool valueChanged;
    public float maxVelocity = 3;
    public bool canMove = true;
    public float baseMovementMultiplier;
    public bool isSneaking = false;
    public float sneakMultiplier;
    public float verticalVel;
    private Vector3 moveVector;
    public bool isGrounded;

    [Header("Camera Options")]
    public Camera gameCamera;
    public float cameraDelay;
    public float xOffset;
    public float yOffset;
    public float zOffset;
    private Vector3 velocity = Vector3.zero;

    [Header("Raycast Options")]
    public Transform rayCastingPoint;
    public float raycastDistance;
    public Color raycastColour;

    [Header("Hunting Options")]
    public HuntingZone huntingZone;
    public float baseHuntingTimer;
    public float distanceTravelled = 0;
    private Vector3 lastPosition;

    [Header("Audio Options")]
    public float baseFootstepTimer;
    public float baseCrouchFootstepTimer;
    public float footstepTimer;
    public AudioClip bushClip;

    [Header("Animation Options")]
    public GameObject playerModel;
    private float verticalMovement;
    private float horizontalMovement;
    public float walkingSpeed;
    private float idleTimer;
    private float move;
    [SerializeField]
    private float baseIdleTimer = 3;
    public float animationTransitionSpeed;

    [Header("Player Input")]
    public Input interact;
    public Input pause;
    public Input zoomIn;
    public Input zoomOut;
    public CanvasGroup controlPopup;
    public bool canSeeControl;
    public float timeMultiplierUI = .5f;

    [Header("Tutorial")]
    public FirstPlaythrough firstPlaythrough;
    public CloseMovementTutorial closeMovementTutorialScript;
    public StartFishingTutorial startFishingTutorial;
    public StartHuntingTutorial startHuntingTutorial;
    public CloseHuntingTutorial closeHuntingTutorial;
    public StartTrapTutorial startTrapTutorial;
    public StartTitleCard startTitleCard;
    



    public void Awake()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.gameObject.GetComponent<HuntingCreature>())
        {
            bool tempRunningAway = collision.collider.GetComponent<HuntingCreature>().runningAway;
            bool tempNoticed = collision.collider.GetComponent<HuntingCreature>().noticed;
            Creature spawnedCreature = collision.collider.GetComponent<HuntingCreature>().selectedCreature;
            print(collision.collider.GetComponent<HuntingCreature>().selectedCreature);
            if (!tempRunningAway && tempNoticed)
            {
                firstPlaythrough.huntingTriggered = false;
                scriptControllerObject.CapturePopup(collision.collider.GetComponent<HuntingCreature>().selectedCreature);
                currency.AddToLocalWallet(spawnedCreature.price);
                spawnedCreature.previouslyCaptured = true;
                spawnedCreature.totalCaught += 1;
                if (spawnedCreature.dateFirstCaught == null)
                {
                    spawnedCreature.dateFirstCaught = System.DateTime.Now.ToShortDateString();
                }
                if (spawnedCreature.currentWeight > spawnedCreature.heaviestCaught)
                {
                    spawnedCreature.heaviestCaught = spawnedCreature.currentWeight;
                }

                if (spawnedCreature.currentWeight < spawnedCreature.lightestCaught)
                {
                    spawnedCreature.lightestCaught = spawnedCreature.currentWeight;
                }
                collision.gameObject.GetComponent<Collider>().enabled = false;
            }
        }


       
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ZoneTrigger")
        {
            huntingZone = other.transform.parent.GetComponent<HuntingZone>();
        }

        if (other.GetComponent<TeleportCave>())
        {
            if (other.GetComponent<TeleportCave>().toEgress == true)
            {
                scriptControllerObject.TeleportCaveToEgress();
                //   moveVector = new Vector3(scriptControllerObject.spawnPoints[0].transform.position.x, scriptControllerObject.spawnPoints[0].transform.position.y, scriptControllerObject.spawnPoints[0].transform.position.z);
            }

            else if (other.GetComponent<TeleportCave>().toGuidanceIsle == true)
            {
                scriptControllerObject.TeleportCaveToGuidance();
                //  moveVector = new Vector3(scriptControllerObject.spawnPoints[3].transform.position.x, scriptControllerObject.spawnPoints[3].transform.position.y, scriptControllerObject.spawnPoints[3].transform.position.z);
            }

        }
    

        if (other.GetComponent<AreaIdentifier>())
        {
            areaIdentifier = other.GetComponent<AreaIdentifier>();
            //Debug.Log(areaIdentifier.areaID);
            if (areaIdentifier.areaID != areaIdentifierID)
            {
                scriptControllerObject.areaIdentifier = areaIdentifier;
                scriptControllerObject.areaID = areaIdentifier.areaID;
                scriptControllerObject.areaName = areaIdentifier.areaName.ToUpper();
                scriptControllerObject.areaIcon = areaIdentifier.areaIcon;
                scriptControllerObject.areaText.text = areaIdentifier.areaName.ToUpper();
                scriptControllerObject.OpenAreaTitle();
                scriptControllerObject.NewArea();
                if(areaIdentifier.areaID < 90)
                {
                    scriptControllerObject.audioManager.ChangeRouteSoundtrackClip(areaIdentifier.areaID, areaIdentifier.changeMovementTracker);
                }

                else
                {
                    scriptControllerObject.audioManager.ChangeRouteSoundtrackClip(0, areaIdentifier.changeMovementTracker);
                }
                areaIdentifierID = areaIdentifier.areaID;
                scriptControllerObject.pauseMenuText.text = areaIdentifier.areaName.ToUpper();
            }
        }

        if (other.GetComponent<CloseMovementTutorial>())
        {
            closeMovementTutorialScript = other.GetComponent<CloseMovementTutorial>();
            closeMovementTutorialScript.ChangeBool();
        }

        if (other.tag == "IntroDialogue" && scriptControllerObject.playerData.firstTime && !firstPlaythrough.chatStarted)
        {
            canMove = false;
            firstPlaythrough.professorDialogueTrigger = other.transform.parent.GetComponent<DialogueTrigger>();
            firstPlaythrough.chatStarted = true;
            scriptControllerObject.playerData.firstTime = true;
            
        }

        if (other.GetComponent<StartFishingTutorial>())
        {
            startFishingTutorial = other.GetComponent<StartFishingTutorial>();
            startFishingTutorial.ChangeBool();
            firstPlaythrough.CheckForSkipping();
        }

        if (other.GetComponent<InteractTutorial>())
        {
            interactTutorial = other.GetComponent<InteractTutorial>();
            interactTutorial.ChangeBool();
            firstPlaythrough.CheckForSkipping();
        }

        if (other.GetComponent<StartHuntingTutorial>())
        {
            startHuntingTutorial = other.GetComponent<StartHuntingTutorial>();
            startHuntingTutorial.ChangeBool();
            firstPlaythrough.CheckForSkipping();
        }

        if (other.GetComponent<CloseHuntingTutorial>())
        {
            closeHuntingTutorial = other.GetComponent<CloseHuntingTutorial>();
            closeHuntingTutorial.ChangeBool();
            firstPlaythrough.CheckForSkipping();
        }

        if (other.GetComponent<StartTrapTutorial>())
        {
            startTrapTutorial = other.GetComponent<StartTrapTutorial>();
            startTrapTutorial.ChangeBool();
            firstPlaythrough.CheckForSkipping();
        }

        if (other.GetComponent<StartTitleCard>())
        {
            startTitleCard = other.GetComponent<StartTitleCard>();
            startTitleCard.ChangeBool();
            firstPlaythrough.CheckForSkipping();
        }

        if(other.tag == "StartFall")
        {
            isGrounded = false;
        }

        if(other.tag == "FallEnd")
        {
            isGrounded = true;
        }

 
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<SpeedModifier>() && valueChanged == false && areaIdentifierID == 7)
        {
            scriptControllerObject.distanceMultiplier = 1.25f;


            valueChanged = true;
        }
    }
      
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ZoneTrigger")
        {
            huntingZone = null;
        }

        if (other.GetComponent<SpeedModifier>() && valueChanged == true && areaIdentifierID == 7)
        {
            scriptControllerObject.distanceMultiplier = 1f;
            valueChanged = false;
        }

        if (other.GetComponent<TeleportCave>())
        {
           
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<Vector2>();
        //Debug.Log("OnMove: " + moveAxis.ToString("#.#####"));
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
       // if(c)
       if(context.ReadValue<float>() !=0)
        {
            crouch = true;
        }
         else
        {
            crouch = false;
        }
      
    }

    // Start is called before the first frame update
    void Start()
    {
        localAudioManager = GameObject.FindObjectOfType<LocalAudioManager>().GetComponent<LocalAudioManager>();
        originalMovementMultiplier = maxVelocity;
        lastPosition = transform.position;
        rigidbody = GetComponent<CharacterController>();
        distanceTravelled = baseHuntingTimer;
        canMove = true;

    }

    void FixedUpdate()
    {
            HuntingZoneCountdown();
            CameraMove();
            if (canMove)
            {
                Movement();
        }
        
      

    }



    // Update is called once per frame
    void Update()
    { 

        if (isGrounded)
        {
            animator.SetBool("Falling", false);
        }
        else
        {
            animator.SetBool("Falling", true);
        }

        if(canSeeControl == true)
        {
            if(controlPopup.alpha != 1)
            {
                controlPopup.alpha += Time.deltaTime * timeMultiplierUI;
            }
        }
      
        else
        {
            if (controlPopup.alpha != 0)
            {
                controlPopup.alpha -= Time.deltaTime * timeMultiplierUI;
            }
        }
            


        Raycast();
        AnimationMovement();
        AudioMovement();
    }


    void AnimationMovement()
    {
        if (canMove && rigidbody.velocity != new Vector3(0, rigidbody.velocity.y, 0))
        {
            verticalMovement = Mathf.Abs(moveAxis.y);
            horizontalMovement = Mathf.Abs(moveAxis.x);

           
                move = Mathf.Clamp(verticalMovement + horizontalMovement, 0, 1);
            
           

            /*
            if (!valueChanged && areaIdentifierID == 7)
            {
                move = Mathf.MoveTowards(move, Mathf.Clamp(verticalMovement + horizontalMovement, 0, .5f), animationTransitionSpeed * Time.deltaTime);
            }

            else
            {
               
            }
            */
        }

        else
        {
            move = Mathf.MoveTowards(move, 0, Time.deltaTime);
        }


        animator.SetFloat("Blend", move);



        if(canMove)
        {
            //Cancle Idle Timer
            if (moveAxis != Vector2.zero && idleTimer != baseIdleTimer)
            {
                idleTimer = baseIdleTimer;
                animator.SetBool("Idle", false);
            }
        }


        //Idle Timer
        if (moveAxis == Vector2.zero)
        {
            if (idleTimer <= 0)
            {
                animator.SetBool("Idle", true);
            }
            else
            {
                idleTimer -= Time.deltaTime;
            }
        }

        //Sneak
        if (isSneaking)
        {
            animator.SetBool("isSneaking", true);
        }

        else
        {
            animator.SetBool("isSneaking", false);
        }

    }

    void AudioMovement()
    {
        //Footsteps
        if(moveAxis == new Vector2(0,0) && rigidbody.velocity != new Vector3(0, 0, 0))
        {
            if(!isSneaking)
            {
                footstepTimer = baseFootstepTimer;
            }

            else
            {
                footstepTimer = baseCrouchFootstepTimer;
            }

        }

        else if (moveAxis != Vector2.zero && rigidbody.velocity != new Vector3(0, 0, 0))
        {
            footstepTimer -= Time.deltaTime;
        }


        if(footstepTimer < 0 )
        {
            scriptControllerObject.audioManager.OneShotFootsteps();
            if (!isSneaking)
            {
                footstepTimer = baseFootstepTimer;
            }

            else
            {
                footstepTimer = baseCrouchFootstepTimer;
            }
        }
    }

    void HuntingZoneCountdown()
    {
        if(huntingZone != null)
        {
            distanceTravelled -= Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;
            if(distanceTravelled <= 0 && huntingZone.amountSpawned < huntingZone.maxCreatures)
            {
                huntingZone.SpawnCreature();
                distanceTravelled = baseHuntingTimer;
                huntingZone.amountSpawned += 1;
            }

        }
    }

    public void Interactions()
    {
            if (trapTrigger != null)
            {
                if(firstPlaythrough.interactionTriggered)
                {
                    firstPlaythrough.interactionTriggered = false;
                }
                trapTrigger.CheckIfTrapPlaced();
                trapTrigger = null;
            }

            if (dialogueTrigger != null)
            {
                if (firstPlaythrough.interactionTriggered)
                {
                    firstPlaythrough.interactionTriggered = false;
                }

                dialogueTrigger.TriggerDialogue();
                dialogueTrigger = null;
            }

            if (fishingTrigger != null)
            {
                if (firstPlaythrough.interactionTriggered)
                {
                    firstPlaythrough.interactionTriggered = false;
                }
                fishingRod.SetBool("isFishing", true);
                fishingTrigger.AttemptFish();
                fishingTrigger = null;
            }

            if (itemDrop != null)
            {
                if (firstPlaythrough.interactionTriggered)
                {
                    firstPlaythrough.interactionTriggered = false;
                }
                itemDrop.OnChestTrigger();
                itemDrop = null;
            }
        
    }

    void Movement()
    {
        //MOVEMENT MULTIPLIER VALUES
        float horizontal = moveAxis.x * baseMovementMultiplier;
        float vertical = moveAxis.y * baseMovementMultiplier;

        float sneakHorizontal = horizontal / sneakMultiplier;
        float sneakVertical = vertical / sneakMultiplier;
        //  if (!inCave)
        // {
        moveVector = new Vector3(0, verticalVel * .2f * Time.deltaTime, 0);

        rigidbody.Move(moveVector);
        //Sneak Bool Changing
        if (crouch)
            {
                if (firstPlaythrough.playerProfile.firstTime)
                {
                    firstPlaythrough.huntingTriggered = false;
                }
                isSneaking = true;
            }

            else
            {
                isSneaking = false;
            }

            if (isSneaking == false && moveAxis == Vector2.zero)
            {
                walkingSpeed = 0;
            }
            //Clamps max speed
            desiredMoveDirection = Vector3.ClampMagnitude(new Vector3(horizontal, 0, vertical), maxVelocity);
            //Normal Movement
            if (isSneaking == false && moveAxis != Vector2.zero)
            {
                if (desiredMoveDirection != new Vector3(0, 0, 0))
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);

                    rigidbody.Move(desiredMoveDirection * Time.deltaTime * baseMovementMultiplier);
                }

                walkingSpeed = 1;
            }

            //Sneak Movement
            sneakDesiredMoveDirection = new Vector3((sneakHorizontal / baseMovementMultiplier), 0, (sneakVertical / baseMovementMultiplier));
            if (isSneaking == true && moveAxis != Vector2.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(sneakDesiredMoveDirection), desiredRotationSpeed);
                rigidbody.Move(sneakDesiredMoveDirection * Time.deltaTime * (maxVelocity / sneakMultiplier));

            }
      //  }
    }

    void CameraMove()
    {
        if (scriptControllerObject.sceneTickDelay >= 0)
        {
            gameCamera.transform.position = transform.transform.position;
        }
        else
        {
            gameCamera.transform.position = Vector3.SmoothDamp(gameCamera.transform.position, new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z + zOffset), ref velocity, cameraDelay);

        }
        
    }

    void Raycast()
    {
        RaycastHit hit;
        Debug.DrawRay(rayCastingPoint.position, transform.forward * raycastDistance, raycastColour, Time.deltaTime);
        if (Physics.Raycast(rayCastingPoint.position, transform.forward, out hit, raycastDistance))
        {
            if(hit.collider.GetComponent<TrapTrigger>() || hit.collider.GetComponent<DialogueTrigger>() || hit.collider.GetComponent<FishingTrigger>() || hit.collider.GetComponent<ItemDrop>())
            {
                canSeeControl = true;
            }

            else
            {
                canSeeControl = false;
            }
            //Creature spawn script initialise variable
            if (hit.collider.GetComponent<TrapTrigger>())
            {
                trapTrigger = hit.collider.GetComponent<TrapTrigger>();

            }

            //Deselect a creature spawn when anopther raycast has been hit
            else
            {
                trapTrigger = null;
 
            }

            //Dialogue Trigger script initialise variable
            if (hit.collider.GetComponent<DialogueTrigger>())
            {
                dialogueTrigger = hit.collider.GetComponent<DialogueTrigger>();
               
            }

            else 
            {
                dialogueTrigger = null;
             
            }

            if (hit.collider.GetComponent<FishingTrigger>())
            {
                fishingTrigger = hit.collider.GetComponent<FishingTrigger>();
               
            }

            else
            {
                fishingTrigger = null;
              
            }

            if(hit.collider.GetComponent<ItemDrop>())
            {
                itemDrop = hit.collider.GetComponent<ItemDrop>();
              
            }

            else
            {
                itemDrop = null;
        
            }
        }


        else
        {
            fishingTrigger = null;
            itemDrop = null;
            dialogueTrigger = null;
            trapTrigger = null;
            canSeeControl = false;
        }
    }
}
