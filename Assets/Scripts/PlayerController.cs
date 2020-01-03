using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Backpack playerInventory;
    public GameController scriptControllerObject;
    public Rigidbody rigidbody;

    [Header("Movement Variables")]
    public float baseMovementMultiplier;
    public bool isSneaking = false;
    public float sneakMultiplier;

    [Header("Camera Options")]
    public Camera gameCamera;
    public float cameraDelay;
    public float xOffset;
    public float zOffset;
    private Vector3 velocity = Vector3.zero;

    [Header("Raycast Options")]
    public float raycastDistance;
    public Color raycastColour;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
        Movement();
        CameraMove();
        Interactions();
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();
    }

    void Interactions()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            if(scriptControllerObject.creatureSpawn != null)
            {
                if(scriptControllerObject.creatureSpawn.spawnType == CreatureSpawn.SpawnType.TrapSpot)
                {
                    scriptControllerObject.creatureSpawn.CreatureSpawnTrap();
                }
                
            }
            
        }
    }

    void Movement()
    {
        //MOVEMENT MULTIPLIER VALUES
        float horizontal = Input.GetAxisRaw("Horizontal") * baseMovementMultiplier;
        float vertical = Input.GetAxisRaw("Vertical") * baseMovementMultiplier;

        float sneakHorizontal = horizontal / sneakMultiplier;
        float sneakVertical = vertical / sneakMultiplier;

        //Sneak Bool Changing
        if(Input.GetKey(KeyCode.LeftControl))
        {
            isSneaking = true;
        }

        else
        {
            isSneaking = false;
        }

        //Normal Movement
        if (isSneaking == false && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            rigidbody.MovePosition(transform.position + new Vector3(horizontal, 0, vertical) * Time.fixedDeltaTime);

        }

        //Sneak Movement
        if (isSneaking == true && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            rigidbody.MovePosition(transform.position + new Vector3(sneakHorizontal, 0, sneakVertical) * Time.fixedDeltaTime);

        }

    }

    void CameraMove()
    {
        gameCamera.transform.position = Vector3.SmoothDamp(gameCamera.transform.position, new Vector3(transform.position.x + xOffset, gameCamera.transform.position.y, transform.position.z + zOffset),ref velocity, cameraDelay);
    }

    void Raycast()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * raycastDistance, raycastColour, Time.deltaTime);
        if (Physics.Raycast(transform.position,  transform.forward, out hit, raycastDistance))
        {
            //Creature spawn script initialise variable
            if(hit.collider.GetComponent<CreatureSpawn>())
            {
                scriptControllerObject.creatureSpawn = hit.collider.GetComponent<CreatureSpawn>();
            }

            //Deselect a creature spawn when anopther raycast has been hit
            if (hit.collider.GetComponent<CreatureSpawn>() == false)
            {
                scriptControllerObject.creatureSpawn = null;
            }
            
            Debug.Log("Raycast Hit");
        }

        else
        {
            scriptControllerObject.creatureSpawn = null;
        }
    }
}
