using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGround : MonoBehaviour
{
    public GameObject player;
    private GameObject ground;
    public float yOffest;
    public float xOffset;
    public bool updateOn;
    // Start is called before the first frame update
    void Start()
    {
        if(player!=null && !updateOn)
        {
            Bruh();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (updateOn)
        {
            Bruh();
        }

    }

    void Bruh()
    {
        if (player != null)
        {
            Vector3 playerPos = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffest,  player.transform.position.z);
            // playerPosition = new Vector3(ground.transform.position.x, +ground.transform.position.y, +ground.transform.position.z);
            transform.position = playerPos;

        }
    }
}
