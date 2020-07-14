using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private PlayerController playerController;
    public GameController gameController;
    private TMP_Animated animatedText;
    public GameObject nextButton;
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
    public bool finished = true;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameController = GameObject.FindObjectOfType<GameController>().GetComponent<GameController>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        if(!gameOverBool)
        {
            gameController.dialogueActive = true;
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
            playerController.canMove = false;
            DisplayNextSentence();
        }
       
    }


    public void DisplayNextSentence()
    {
       // gameController.SetSelectedButton(nextButton.gameObject);
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();

        if(!animator.IsInTransition(0) && finished == true)
        {
            nextButton.gameObject.GetComponent<Button>().interactable= false;
            gameController.SetSelectedButton(nextButton);
            NPCSentence.ReadText(sentence);

        }

       // Debug.Log(sentence);
    }

    public void DialogueFinished()
    {
        finished = true;
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

        gameController.DeselectButton();
        gameController.dialogueActive = false;
        playerController.canMove = true;
        Debug.Log("End of conversation");
        animator.SetBool("IsOpen", false);
        dialogueActive = false;
        if(gameOverBool)
        {
            GetComponent<GameController>().GameOver();
        }
    }
}
