using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapManager : MonoBehaviour
{
    public GameController gameController;
    [Header("Trap Details")]
    public Creature spawnedCreature;
    public Lure placedTrap;
    public Button closeButton;
    private GameObject selectedTrapTrigger;
    private Creature selectedCreature;


    [Header("Visual")]
    public Animator animator;
    public Text infoText;
    public bool trapMessageActive;


[Header("Minigame Details")]
    public GameObject PopMinigame;
    public PopMinigameController popMinigameController;

    public void OpenTrapMenu()
    {
        animator.SetBool("IsOpen", true);
        trapMessageActive = true;
        gameController.SetSelectedButton(closeButton.gameObject);
    }


    public void CloseTrapMenu()
    {
        animator.SetBool("IsOpen", false);
        trapMessageActive = false;
        gameController.DeselectButton();
    }




    public void SpawnCreature(Creature creature)
    {
        closeButton.onClick.RemoveAllListeners();
        gameController.selectedCreature = creature;
        gameController.SpawnCreature();
        PopMinigame.SetActive(true);

    }

    public void UpdateText(string trapName, string trapMessagePart1, string trapMessagePart2)
    {
        string sentence = trapMessagePart1 + trapName + trapMessagePart2;
        if (!animator.IsInTransition(0))
        {
            StartCoroutine(TypeSentence(sentence));
        }
    }

    public void UpdateFailedText(string trapMessagePart1)
    {
        string sentence = trapMessagePart1;
        if (!animator.IsInTransition(0))
        {
            StartCoroutine(TypeSentence(sentence));
        }
    }

    public void UpdateTrapEncounter(Creature creature, GameObject trapTrigger, string encounterMessage1, string encounterMessage2)
    {
        string sentence = encounterMessage1 + creature.name + encounterMessage2 + ".";
        selectedTrapTrigger = trapTrigger;
        trapTrigger.GetComponent<TrapTrigger>().creatureSet = false;
        trapTrigger.GetComponent<TrapTrigger>().trapPlaced = false;
        selectedCreature = creature;
        OpenTrapMenu();
            StartCoroutine(TypeSentence(sentence));
        closeButton.onClick.AddListener(delegate { SpawnCreature(selectedCreature); });
    }

    public void UpdateFishingEncounter(Creature creature, string encounterMessage1, string encounterMessage2)
    {
        string sentence = encounterMessage1 + creature.name + encounterMessage2 + ".";
        selectedCreature = creature;
        OpenTrapMenu();
        StartCoroutine(TypeSentence(sentence));
        closeButton.onClick.AddListener(delegate { SpawnCreature(selectedCreature); });
    }

    public void UpdateFishingFailed(string encounterMessage1)
    {
        string sentence = encounterMessage1;
        OpenTrapMenu();
        StartCoroutine(TypeSentence(sentence));
       // closeButton.onClick.AddListener(delegate { SpawnCreature(selectedCreature); });
    }

    IEnumerator TypeSentence(string sentence)
    {
        infoText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            infoText.text += letter;
            yield return null;
        }
    }

}
