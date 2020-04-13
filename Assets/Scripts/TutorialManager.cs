using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Text tutorialTitle;
    public Text tutorialText;
    public Queue<string> sentences;
    public Animator animator;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartTutorial(Tutorial tutorial)
    {
            Debug.Log("Starting the tutorial for " + tutorial.tutorialName + " Tutorial");
            animator.SetBool("isOpen", true);

            tutorialTitle.text = tutorial.tutorialName + " Tutorial";

            sentences.Clear();

            foreach (string sentence in tutorial.sentences)
            {
            print("bruh");
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();


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

         if (!animator.IsInTransition(0))
          {
          StartCoroutine(TypeSentence(sentence));
          }

        // Debug.Log(sentence);
    }

    IEnumerator TypeSentence(string sentence)
    {
        tutorialText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            tutorialText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation");
        animator.SetBool("isOpen", false);

    }
}
