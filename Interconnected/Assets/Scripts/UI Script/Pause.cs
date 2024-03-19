using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public static Pause pause;

    public bool isGamePaused;

    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] SceneSystem sceneSystem;

    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject inGameUI;

    [SerializeField] int curValueButton;
    [SerializeField] int[] curValueButtonIndex; // buat mouse cursor
    [SerializeField] int maxListButton;
    [SerializeField] int buttonHighlightedValue; // yang di highlight sama cursor mouse

    [SerializeField] bool isButtonHighlighted;

    [SerializeField] Button[] listPauseButton;

    bool isDpadPressed = false;


    private void Awake()
    {
        pause = this;
    }
    private void Start()
    {
        mouseListener();
        
    }
    private void Update()
    {
       if(!GameOver.gameOver.isGameOver && 
            !GameFinish.gameFinish.isGameFinish && 
            ReadyToStart.readyToStart.isGameStart &&
            !DialogueManager.dialogueManager.isDialogueActive) 
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
            {

                if (!isGamePaused)
                {
                    isGamePaused = true;
                }
                else
                {
                    isGamePaused = false;
                }
            }
        }
        for(int i = 0; i < listPauseButton.Length; i++) 
        {
            if (curValueButton == i + 1)
            {
                listPauseButton[i].transform.localScale = Vector2.Lerp(listPauseButton[i].transform.localScale, new Vector2(1.1f, 1.1f), 10 * Time.unscaledDeltaTime);
            }
            else
            {
                listPauseButton[i].transform.localScale = Vector2.Lerp(listPauseButton[i].transform.localScale, new Vector2(1, 1), 16 * Time.unscaledDeltaTime);
            }
        }
        gamePaused();
        pauseInputConfirmButton();
        pauseInputListSelection();

    }
    void gamePaused()
    {
        
        if (isGamePaused)
        {
            pauseUI.SetActive(true);
            inGameUI.SetActive(false);
            if (MouseCursorActivated.mouseCursorActivated.isMouseActive)
            {
                if (!isButtonHighlighted)
                {
                    curValueButton = 0;
                    buttonHighlightedValue = 0;
                }
                else
                {
                    curValueButton = buttonHighlightedValue;
                }
            }
            else
            {
                buttonHighlightedValue = curValueButton;
            }
            if(!GameOver.gameOver.isGameOver)
            {
                Time.timeScale = 0;
            }

        }
        else
        {
            pauseUI.SetActive(false);
            if(!GameFinish.gameFinish.isGameFinish && !GameOver.gameOver.isGameOver) 
            {
                inGameUI.SetActive(true);
            }

            if(!Player1Health.player1Health.isSharingLivesToP2 && !Player2Health.player2Health.isSharingLivesToP1) 
            {
                if(!GameOver.gameOver.isGameOver)
                {
                    Time.timeScale = 1;
                }
            }

        }
        
       
    }


    private void mouseListener()
    {
       
        for (int j = 0; j < listPauseButton.Length; j++)
        {
            if (!SceneSystem.sceneSystem.isChangeScene)
            {
                int buttonValue = curValueButtonIndex[j];

                EventTrigger eventTrigger = listPauseButton[j].gameObject.GetComponent<EventTrigger>();

                if (eventTrigger == null)
                {
                    eventTrigger = listPauseButton[j].gameObject.AddComponent<EventTrigger>();
                }

                // cursor masuk ke dalam tombol
                EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
                pointerEnter.eventID = EventTriggerType.PointerEnter;
                pointerEnter.callback.AddListener((data) => { OnButtonPointerEnter(); });
                pointerEnter.callback.AddListener((data) => { buttonPauseHighlighted(buttonValue); });
                eventTrigger.triggers.Add(pointerEnter);

                // cursor keluar dari tombol
                EventTrigger.Entry pointerExit = new EventTrigger.Entry();
                pointerExit.eventID = EventTriggerType.PointerExit;
                pointerExit.callback.AddListener((data) => { OnButtonPointerExit(); });
                eventTrigger.triggers.Add(pointerExit);
            }
          
           
        }

    }

    private void OnButtonPointerEnter()
    {
        isButtonHighlighted = true;
    }

    private void OnButtonPointerExit()
    {
        isButtonHighlighted = false;
       
    }
    void buttonPauseHighlighted(int value)
    {
        buttonHighlightedValue = value;
    }

    #region pause input

    void pauseInputConfirmButton() 
    {
        if (isGamePaused && !SceneSystem.sceneSystem.isChangeScene) 
        {
            
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Gamepad Enter")) 
            {
                MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
                if (curValueButton == 1) 
                {
                    sceneSystem.isRestartScene = true;
                    
                    // restart scene
                }

                if (curValueButton == 2) 
                {
                    sceneSystem.isExitScene = true;
                    //exit scene ke menu
                }
            }
        }
    }

    void pauseInputListSelection() 
    {
        if (isGamePaused && !SceneSystem.sceneSystem.isChangeScene)
        {
            
            float inputDpadVertical = Input.GetAxis("Dpad Vertical");

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                curValueButton++;
                if (curValueButton > maxListButton) 
                {
                    curValueButton = 1;
                }
                MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                curValueButton--;
                if (curValueButton < 1) 
                {
                    curValueButton = maxListButton;
                }
                MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
            }

            if (inputDpadVertical == 1 && !isDpadPressed)
            {
                MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
                curValueButton++;
                if (curValueButton > maxListButton)
                {
                    curValueButton = 1;
                }
                isDpadPressed = true;
            }
            if (inputDpadVertical == -1 && !isDpadPressed)
            {
                MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
                curValueButton--;
                if (curValueButton < 1)
                {
                    curValueButton = maxListButton;
                }
                isDpadPressed = true;
            }

            if (inputDpadVertical == 0)
            {
                isDpadPressed = false;
            }
        }
    }
    #endregion
    public void onClickRestart() 
    {
        sceneSystem.isRestartScene = true;
    }
    public void onClickExit() 
    {
        sceneSystem.isExitScene = true;
    }
}
