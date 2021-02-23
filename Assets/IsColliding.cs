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
    public bool isAnomaly;
    public float minPitch;
    public float maxPitch;
    public float currentPitch;

    private void OnCollisionExit(Collision collision)
    {
        if (isAnomaly == false)
        {
            if (collision.gameObject.layer == 11 && !isTriggered && playerController.canMove && playerController.rigidbody.velocity != new Vector3(0, 0, 0))
            {
                isTriggered = true;
                localAudioManager.OneShotShrub(playerController.bushClip);

            }
        }

        else
        {
            if (collision.gameObject.layer == 11 && !isTriggered)
            {
                isTriggered = true;
                GetComponent<HuntingCreature>().AnomalyOneShot(currentPitch);

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        localAudioManager = GameObject.FindObjectOfType<LocalAudioManager>().GetComponent<LocalAudioManager>();
        minPitch = localAudioManager.audioManager.footstepsMinPitch;
        maxPitch = localAudioManager.audioManager.footstepsMaxPitch;
    }

    // Update is called once per frame
    void Update()
    {
     

       
            if (currentFrequency > 0 && isTriggered)
            {
                currentFrequency -= Time.deltaTime;
            }

            else
            {
                isTriggered = false;
                currentFrequency = frequency;
            }
            currentPitch = Random.Range(minPitch, maxPitch);

    }
}
