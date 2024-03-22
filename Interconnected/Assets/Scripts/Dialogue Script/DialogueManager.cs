using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager dialogueManager;

    public bool isDialogueActive;

    public int curTextValue;

    [SerializeField] float typingSpeedValue;
    [SerializeField] float targetDialoguePos;

    [SerializeField] bool isReadyToInteractWithDialogue;

    [TextArea(3,12)]
    [SerializeField] string[] listDialogueString;

    [SerializeField] TextMeshProUGUI textDialogue;

    [SerializeField] Button buttonDialogue;

    [SerializeField] GameObject dialogueObject;

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
        
        if (isDialogueActive) 
        {
            if (ReadyToStart.readyToStart.isGameStart)
            {
                
                if (!isReadyToInteractWithDialogue)
                {
                    dialogueObject.transform.localPosition = Vector2.MoveTowards(dialogueObject.transform.localPosition, new Vector2(dialogueObject.transform.localPosition.x, targetDialoguePos), 2000 * Time.deltaTime);
                }

               

            }
            if (dialogueObject.transform.localPosition.y == targetDialoguePos)
            {
                isReadyToInteractWithDialogue = true;

            }
            if (isReadyToInteractWithDialogue)
            {
                buttonDialogue.enabled = true;
            }
            else { buttonDialogue.enabled = false; }
        }
        else 
        {
            if (GlobalVariable.globalVariable.isLevelHasDialogue) 
            {
                dialogueObject.transform.position = Vector2.MoveTowards(dialogueObject.transform.position, new Vector2(dialogueObject.transform.position.x, 0), 2800 * Time.deltaTime);
                isReadyToInteractWithDialogue = false;

                buttonDialogue.enabled = false;
            }
            
        }
       
       
    

       
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Gamepad Enter")) 
        {
            
            
        }

        if (isDialogueActive) 
        {
            textDialogue.enabled = true;

            dialogueObject.SetActive(true);
           
        }
        else 
        {
            if (textDialogue!=null) 
            {
                textDialogue.enabled = false;
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

    public void inputNavigationDialogue(InputAction.CallbackContext context) 
    {
        if (context.performed) 
        {
            if (isDialogueActive && isReadyToInteractWithDialogue)
            {
                MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
                if (LevelStatus.levelStatus.levelID != 4)
                {
                    if (ReadyToStart.readyToStart.isGameStart)
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
                else
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
