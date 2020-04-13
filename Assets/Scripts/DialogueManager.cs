using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [HideInInspector]
    public bool gameOverBool;
    private Queue<string> sentences;

    public Text NPCName;
    public Text NPCSentence;

    public Animator animator;

    public bool dialogueActive;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        if(!gameOverBool)
        {
            gameOverBool = dialogue.gameOverMessage;
            dialogueActive = true;
            Debug.Log("Starting convo with " + dialogue.NPCName);
            animator.SetBool("IsOpen", true);

            NPCName.text = dialogue.NPCName;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }
       
    }


    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        if(!animator.IsInTransition(0))
        {
            StartCoroutine(TypeSentence(sentence));
        }
        
       // Debug.Log(sentence);
    }

    IEnumerator TypeSentence(string sentence)
    {
        NPCSentence.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            NPCSentence.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        animator.SetBool("IsOpen", false);
        dialogueActive = false;
        if(gameOverBool)
        {
            GetComponent<GameController>().GameOver();
        }
    }
}
