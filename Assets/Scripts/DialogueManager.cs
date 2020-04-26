using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private TMP_Animated animatedText;
    [HideInInspector]
    public bool gameOverBool;
    private Queue<string> sentences;

    public TextMeshProUGUI NPCName;
    public TMP_Animated NPCSentence;

    public Animator animator;

    public Color personColour;
    public Color itemColour;
    public Color defaultColour;
    //public Color personColour;


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
            NPCSentence.ReadText(sentence);
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
