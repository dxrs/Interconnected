using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager dialogueManager;

    public bool isDialogueActive;

    public int curTextValue;

    [SerializeField] float typingSpeedValue;

    [TextArea(3,12)]
    [SerializeField] string[] listDialogueString;

    [SerializeField] TextMeshProUGUI textDialogue;

    [SerializeField] Button buttonDialogue;

    [SerializeField] GameObject dialogueObject;
    [SerializeField] GameObject dialoguePanel;

    private void Awake()
    {
        dialogueManager = this;
    }

    private void Start()
    {
        if (isDialogueActive) 
        {
            textDialogue.text = listDialogueString[0];
        }

        StartCoroutine(typingText());
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Gamepad Enter")) 
        {
            if (isDialogueActive) 
            {
                if (textDialogue.text == listDialogueString[curTextValue])
                {
                    curTextValue++;
                    nextSentence();
                }
                else
                {
                    // kalau lagi typing ketika ingin tulisan langsung ada semua
                    StopAllCoroutines();
                    textDialogue.text = listDialogueString[curTextValue];
                }

            }
            
        }

        if (isDialogueActive) 
        {
            textDialogue.enabled = true;
            buttonDialogue.interactable = true;
            dialoguePanel.SetActive(true);
            dialogueObject.SetActive(true);
           
        }
        else 
        {
            if(textDialogue!=null
                && buttonDialogue!=null
                && dialoguePanel!=null
                && dialogueObject != null) 
            {
                textDialogue.enabled = false;
                buttonDialogue.interactable = false;
                dialoguePanel.SetActive(false);
                dialogueObject.SetActive(false);
            }
           
        }
       
        if (LevelStatus.levelStatus.levelID == 4)
        {
            int tutorialProgress = Tutorial.tutorial.tutorialProgress;

            if (tutorialProgress == 1 && curTextValue == 6)
            {
                isDialogueActive = false;
            }
            else if (tutorialProgress == 2 && Tutorial.tutorial.isReadyToShareLives)
            {
               isDialogueActive = (curTextValue <= 14);
            }
            else if (tutorialProgress == 3)
            {
                bool playersEnterGarbageArea = Tutorial.tutorial.isPlayersEnterGarbageArea[0] && Tutorial.tutorial.isPlayersEnterGarbageArea[1];

                if (playersEnterGarbageArea && curTextValue <= listDialogueString.Length - 1)
                {
                    isDialogueActive = true;
                    GlobalVariable.globalVariable.colliderInactive();
                }
                else if (!GameFinish.gameFinish.isGameFinish)
                {
                    GlobalVariable.globalVariable.colliderActive();
                }
            }
        }

    }

    void nextSentence() 
    {
        if (isDialogueActive) 
        {
            if (curTextValue <= listDialogueString.Length - 1)
            {
                textDialogue.text = string.Empty;
                StartCoroutine(typingText());
            }
            else
            {
                isDialogueActive = false;
            }
        }
       
    }

    IEnumerator typingText()
    {
        if (curTextValue > 0) 
        {
            foreach (char c in listDialogueString[curTextValue].ToCharArray())
            {

                textDialogue.text += c;
                yield return new WaitForSeconds(typingSpeedValue);

            }
        }
       
    }

    public void onClickDialogueButton() 
    {
        
        if (textDialogue.text == listDialogueString[curTextValue])
        {
            curTextValue++;
            nextSentence();
        }
        else
        {
            // kalau lagi typing ketika ingin tulisan langsung ada semua
            StopAllCoroutines();
            textDialogue.text = listDialogueString[curTextValue];
        }

    }
}
