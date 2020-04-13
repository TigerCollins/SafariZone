using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool NPC;
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        if(NPC)
        {
            Vector3 playerPos = new Vector3(GameObject.Find("Player").transform.position.x, gameObject.transform.position.y, GameObject.Find("Player").transform.position.z);
            gameObject.transform.LookAt(playerPos);
        }
       
    }
}
