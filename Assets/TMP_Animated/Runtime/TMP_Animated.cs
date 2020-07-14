using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace TMPro
{
    public enum Emotion { happy, sad, suprised, angry };
    [System.Serializable] public class EmotionEvent : UnityEvent<Emotion> { }

    [System.Serializable] public class ActionEvent : UnityEvent<string> { }

    [System.Serializable] public class TextRevealEvent : UnityEvent<char> { }

    [System.Serializable] public class DialogueEvent : UnityEvent { }

    public class TMP_Animated : TextMeshProUGUI
    {

        [SerializeField] private float speed = 10;
        public EmotionEvent onEmotionChange;
        public ActionEvent onAction;
        public TextRevealEvent onTextReveal;
        public DialogueEvent onDialogueFinish;
        private Text tempText;
        private PlayerData playerData;
        private LocalAudioManager localAudioManager;
        private GameController gameController;
        private DialogueManager dialogueManager;

        
        public void ReadText(string newText)
        {
            dialogueManager = GameObject.FindObjectOfType<DialogueManager>().GetComponent<DialogueManager>();
            playerData = GameObject.FindObjectOfType<GameController>().GetComponent<GameController>().playerData;
            localAudioManager = GameObject.FindObjectOfType<LocalAudioManager>().GetComponent<LocalAudioManager>();
            gameController = GameObject.FindObjectOfType<GameController>().GetComponent<GameController>();
            text = string.Empty;
            dialogueManager.finished = false;
            newText.Replace("PlayerName", "boi");
            // split the whole text into parts based off the <> tags 
            // even numbers in the array are text, odd numbers are tags

            string[] subTexts = newText.Split('<', '>');

            // textmeshpro still needs to parse its built-in tags, so we only include noncustom tags
            string displayText = "";
            for (int i = 0; i < subTexts.Length; i++)
            {
            
                if (i % 2 == 0)
                {
                    displayText += subTexts[i];
                    
                }

                else if (subTexts[i].Contains("PlayerName"))
                {
                    displayText += $"{playerData.playerName}";
                }

                else if (!isCustomTag(subTexts[i].Replace(" ", "")))
                {
                    displayText += $"<{subTexts[i]}>";
                }

      


            }
            // check to see if a tag is our own
            bool isCustomTag(string tag)
            {
                return tag.StartsWith("speed=") || tag.StartsWith("pause=") || tag.StartsWith("emotion=") || tag.StartsWith("action");
            }


            // send that string to textmeshpro and hide all of it, then start reading

            text = displayText;
            maxVisibleCharacters = 0;
            StartCoroutine(Read());

            IEnumerator Read()
            {
                int subCounter = 0;
                int visibleCounter = 0;
                while (subCounter < subTexts.Length)
                {
                    // if 
                    if (subCounter % 2 == 1)
                    {
                        yield return EvaluateTag(subTexts[subCounter].Replace(" ", ""));
                     
                    }
                    else
                    {
                        while (visibleCounter < subTexts[subCounter].Length)
                        {
                            onTextReveal.Invoke(subTexts[subCounter][visibleCounter]);
                            if(gameController.dialogueActive)
                            {
                                localAudioManager.OneShotDialogue();
                            }

                            visibleCounter++;
                            maxVisibleCharacters++;
                            yield return new WaitForSeconds(1f / speed);
                        }
                        visibleCounter = 0;
                    }
                    subCounter++;
                }
                yield return null;

                    WaitForSeconds EvaluateTag(string tag)
                {
                    if (tag.Length > 0)
                    {
                        if (tag.StartsWith("speed="))
                        {
                            speed = float.Parse(tag.Split('=')[1]);
                        }
                        else if (tag.StartsWith("pause="))
                        {
                            return new WaitForSeconds(float.Parse(tag.Split('=')[1]));
                        }
                        else if (tag.StartsWith("emotion="))
                        {
                            onEmotionChange.Invoke((Emotion)System.Enum.Parse(typeof(Emotion), tag.Split('=')[1]));
                        }
                        else if (tag.StartsWith("action="))
                        {
                            onAction.Invoke(tag.Split('=')[1]);
                        }
                    }
                    return null;
                }

                
                onDialogueFinish.Invoke();
                dialogueManager.finished = true;
                dialogueManager.nextButton.GetComponent<Button>().interactable = true;

                gameController.SetSelectedButton(dialogueManager.nextButton);
                //gameController.eventSystem.SetSelectedGameObject(dialogueManager.nextButton);
            }
        }
    }
}