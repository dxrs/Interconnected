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
    [SerializeField] GameObject pauseSelector;

    [SerializeField] int curValueButton;
    [SerializeField] int[] curValueButtonIndex; // buat mouse cursor
    [SerializeField] int maxListButton;
    [SerializeField] int buttonHighlightedValue; // yang di highlight sama cursor mouse

    [SerializeField] Button[] listPauseButton;

    [SerializeField] Vector2[] pauseSelectorPos;

    bool isDpadPressed = false;


    private void Awake()
    {
        pause = this;
    }
    private void Start()
    {
        mouseListener();
        curValueButton = 1;
        buttonHighlightedValue = 1;
    }
    private void Update()
    {
       if(!GameOver.gameOver.isGameOver && 
            !GameFinish.gameFinish.isGameFinish && 
            ReadyToStart.readyToStart.isGameStart) 
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
            {
               //MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
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
        gamePaused();
        pauseInputConfirmButton();
        pauseInputListSelection();
        selectorPos();
    }
    void gamePaused()
    {
        if (isGamePaused)
        {
        
            if (MouseCursorActivated.mouseCursorActivated.isMouseActive)
            {
                curValueButton = buttonHighlightedValue;
            }
            else
            {
                buttonHighlightedValue = curValueButton;
            }

            Time.timeScale = 0;
        }
        else
        {
            pauseUI.SetActive(false);
            curValueButton = 1;
            if(!Player1Health.player1Health.isSharingLivesToP2 && !Player2Health.player2Health.isSharingLivesToP1) 
            {
                Time.timeScale = 1;
            }

        }
        
        if(isGamePaused || sceneSystem.isRestartScene || sceneSystem.isExitScene) 
        {
            pauseUI.SetActive(true);
        }
    }
    void selectorPos() 
    {
        if(!MouseCursorActivated.mouseCursorActivated.isMouseActive && isGamePaused) 
        {
            for(int j = 0; j < pauseSelectorPos.Length; j++) 
            {
                if (curValueButton == j + 1) 
                {
                    pauseSelector.transform.localPosition = pauseSelectorPos[j];
                }
               
            }
            
        }
        for (int i = 0; i < pauseSelectorPos.Length; i++)
        {
            if (MouseCursorActivated.mouseCursorActivated.isMouseActive) 
            {
                if (buttonHighlightedValue == i + 1)
                {
                    pauseSelector.transform.localPosition = pauseSelectorPos[i];
                }
                
            }            
        }
    }

    private void mouseListener() 
    {
        do
        {
            for(int j = 0; j < listPauseButton.Length; j++) 
            {
                int buttonValue = curValueButtonIndex[j];

                EventTrigger eventTrigger = listPauseButton[j].gameObject.AddComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerEnter;
                entry.callback.AddListener((data) => { buttonPauseHighlighted(buttonValue); });
                eventTrigger.triggers.Add(entry);
            }
        } while (isGamePaused && !SceneSystem.sceneSystem.isChangeScene);
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
                isGamePaused = false;
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

        isGamePaused = false;

    }
    public void onClickExit() 
    {
        sceneSystem.isExitScene = true;

        isGamePaused = false;
    }

    public void ok(InputAction.CallbackContext context) 
    {
        if (context.performed) 
        {
            Debug.Log("pause");
        }
    }
}
