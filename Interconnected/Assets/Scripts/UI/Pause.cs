using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static Pause pause;

    public bool isGamePaused;

    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] SceneSystem sceneSystem;

    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject pauseSelector;

    [SerializeField] bool isMouseMoving;

    [SerializeField] int curValueButton;
    [SerializeField] int[] curValueButtonIndex; // buat mouse cursor
    [SerializeField] int maxListButton;
    [SerializeField] int buttonPauseHighlightedValue; // yang di highlight sama cursor mouse

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
        buttonPauseHighlightedValue = 1;
    }
    private void Update()
    {
       if(!globalVariable.isGameOver || !globalVariable.isGameFinish) 
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
        gamePaused();
        pauseInputConfirmButton();
        pauseInputListSelection();
        selectorPos();
    }
    void gamePaused()
    {
        if (isGamePaused)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            if (Mathf.Abs(mouseX) > 0.1f || Mathf.Abs(mouseY) > 0.1f)
            {
                isMouseMoving = true;
            }

            if (isMouseMoving)
            {
                curValueButton = buttonPauseHighlightedValue;
            }
            else
            {
                buttonPauseHighlightedValue = curValueButton;
            }

            Time.timeScale = 0;
        }
        else
        {
            pauseUI.SetActive(false);
            curValueButton = 1;
            Time.timeScale = 1;
        }
        
        if(isGamePaused || sceneSystem.isRestartScene || sceneSystem.isExitScene) 
        {
            pauseUI.SetActive(true);
        }
    }
    void selectorPos() 
    {
        if(!isMouseMoving && isGamePaused) 
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
            if (isMouseMoving) 
            {
                if (buttonPauseHighlightedValue == i + 1)
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
        } while (isGamePaused);
    }
    void buttonPauseHighlighted(int value)
    {
        buttonPauseHighlightedValue = value;
    }

    #region pause input

    void pauseInputConfirmButton() 
    {
        if (isGamePaused) 
        {
            
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Gamepad Enter")) 
            {
                isGamePaused = false;
                if (curValueButton == 1) 
                {
                    sceneSystem.isRestartScene = true;
                    SceneManager.LoadScene("Level 1");
                    // restart scene
                }

                if (curValueButton == 2) 
                {
                    sceneSystem.isExitScene = true;
                    SceneManager.LoadScene("Menu");
                    //exit scene ke menu
                }
            }
        }
    }

    void pauseInputListSelection() 
    {
        if (isGamePaused)
        {
            
            float inputDpadVertical = Input.GetAxis("Dpad Vertical");

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                curValueButton++;
                if (curValueButton > maxListButton) 
                {
                    curValueButton = 1;
                }
                isMouseMoving = false;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                curValueButton--;
                if (curValueButton < 1) 
                {
                    curValueButton = maxListButton;
                }
                isMouseMoving = false;
            }

            if (inputDpadVertical == 1 && !isDpadPressed)
            {
                isMouseMoving = false;
                curValueButton++;
                if (curValueButton > maxListButton)
                {
                    curValueButton = 1;
                }
                isDpadPressed = true;
            }
            if (inputDpadVertical == -1 && !isDpadPressed)
            {
                isMouseMoving = false;
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
        Debug.Log("restart");
        isGamePaused = false;

        SceneManager.LoadScene("Level 1");// restart scene

    }
    public void onClickExit() 
    {
        sceneSystem.isExitScene = true;
        Debug.Log("exit");
        isGamePaused = false;
        SceneManager.LoadScene("Menu");
        // exit scene to menu
    }
}
