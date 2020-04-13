using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public bool NPC;
    public Tutorial tutorial;

    public void TriggerDialogue()
    {

        FindObjectOfType<TutorialManager>().StartTutorial(tutorial);
        if (NPC)
        {
            Vector3 playerPos = new Vector3(GameObject.Find("Player").transform.position.x, gameObject.transform.position.y, GameObject.Find("Player").transform.position.z);
            gameObject.transform.LookAt(playerPos);
        }

    }
}
