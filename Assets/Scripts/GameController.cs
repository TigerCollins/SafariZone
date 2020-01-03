using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerObject;
    public PlayerController playerScript;

    [Header("Player Variables")]
    [SerializeField]
    private float distanceTravelled;
    private Vector3 lastPosition;

    [Header("Player Raycast Variables")]
    public CreatureSpawn creatureSpawn;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        playerScript = playerObject.GetComponent<PlayerController>();
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Statistics();
        SpawnCreature();
    }

    void Statistics()
    {
        distanceTravelled += Vector3.Distance(playerObject.transform.position, lastPosition);
        lastPosition = playerObject.transform.position;
    }

    void SpawnCreature()
    {

    }
}
