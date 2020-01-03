using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawn : MonoBehaviour
{
    public enum SpawnType {HuntingSpot,TrapSpot,FishingSpot }
    
    public SpawnType spawnType;
    public List<Creature> creatureSpawns;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreatureSpawnTrap()
    {
        Debug.Log("You encountered a " + creatureSpawns[Random.Range(0, creatureSpawns.Count)]);
    }
}
