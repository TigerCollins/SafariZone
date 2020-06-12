using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingZone : MonoBehaviour
{
    public int maxCreatures;
    public int amountSpawned;
    [Header("AI")]
    public List<Transform> spawnPoints;
    public GameObject creaturePrefab;
    [SerializeField]
    private GameObject spawnPointHolder;

    [Header("Creature")]
    public List<Creature> possibleCreatures;
    // Start is called before the first frame update
    void Awake()
    {
        int index = 0;
        foreach (Transform spawnPoint in spawnPointHolder.transform)
        {
            spawnPoints.Add(spawnPointHolder.transform.GetChild(index));
            index++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCreature()
    {
        /*
        int creatureCount = 0;
        int index = 0;
        foreach (Transform spawnPoint in spawnPointHolder.transform)
        {
            if (spawnPointHolder.transform.GetChild(index).childCount != 0)
            {
                creatureCount++;
                print("Added to creature count");
            }
            index++;
        }


        if(creatureCount == 0)
        {    */
        int spawnNum = Random.Range(0, spawnPoints.Count);
            Instantiate(creaturePrefab, spawnPoints[spawnNum].transform.position, spawnPoints[spawnNum].transform.rotation, spawnPoints[spawnNum]);
            int creatureSpawnNum = Random.Range(0, possibleCreatures.Count);
            //GetComponent<>
            spawnPoints[spawnNum].GetChild(0).GetComponent<HuntingCreature>().selectedCreature = possibleCreatures[creatureSpawnNum];
        //}

                


    }
}
