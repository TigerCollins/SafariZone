using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsColliding : MonoBehaviour
{
    public PlayerController playerController;
    public LocalAudioManager localAudioManager;
    private float currentFrequency;
    public float frequency;
    private bool isTriggered;

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 11 && !isTriggered && playerController.canMove && playerController.rigidbody.velocity != new Vector3(0, 0, 0))
        {
            isTriggered = true;
            localAudioManager.OneShotShrub(playerController.bushClip);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        localAudioManager = GameObject.FindObjectOfType<LocalAudioManager>().GetComponent<LocalAudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFrequency > 0 && isTriggered)
        {
            currentFrequency -= Time.deltaTime;
        }

        else
        {
            isTriggered = false;
            currentFrequency = frequency;
        }
    }
}
