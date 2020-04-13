using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraFollow : MonoBehaviour {
    [SerializeField]
    GameObject player;

	void Update ()
    {
        transform.position = new Vector3(player.transform.position.x,  8, player.transform.position.z - 10);
	}
}
