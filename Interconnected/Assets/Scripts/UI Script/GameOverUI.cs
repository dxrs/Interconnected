using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] SceneSystem sceneSystem;

    [SerializeField] GameObject gameOverUI;

    [SerializeField] int curValueButton;
    [SerializeField] int[] curValueButtonIndex; // buat mouse cursor
    [SerializeField] int maxListButton;
    [SerializeField] int buttonHighlightedValue; // yang di highlight sama cursor mouse

    [SerializeField] bool isButtonHighlighted;

    [SerializeField] Button[] listGameOverButton;

    [SerializeField] TextMeshProUGUI textGameOverStatus;


    bool isDpadPressed = false;

    private void Start()
    {
        gameOverUI.SetActive(false);
        mouseListener();
        curValueButton = 0;
        buttonHighlightedValue = 0;
    }

    private void Update()
    {
        if (GameOver.gameOver.isGameOver) 
        {
            if (SceneSystem.sceneSystem.isChangeScene)
            {
                for (int k = 0; k < listGameOverButton.Length; k++)
                {
                    listGameOverButton[k].enabled = false;
                }
            }
            gameOverStatus();
            compareButtonValue();
            StartCoroutine(waiToActive());
        }
        
    }

    void compareButtonValue()
    {
        if (LevelStatus.levelStatus.levelID != 4)
        {
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
        }

        for (int i = 0; i < listGameOverButton.Length; i++)
        {
            if (curValueButton == i + 1)
            {
                listGameOverButton[i].transform.localScale = Vector2.Lerp(listGameOverButton[i].transform.localScale, new Vector2(1.1f, 1.1f), 10 * Time.unscaledDeltaTime);
            }
            else
            {
                listGameOverButton[i].transform.localScale = Vector2.Lerp(listGameOverButton[i].transform.localScale, new Vector2(1, 1), 16 * Time.unscaledDeltaTime);
            }
        }
    }

   

    void gameOverStatus() 
    {
        if (Timer.timerInstance.isTimerLevel) 
        {
            if (Timer.timerInstance.curTimerValue <= 0)
            {
                textGameOverStatus.text = "Time is Over";
            }
        }
        if (Player1Health.player1Health.curPlayer1Health <= 0) 
        {
            textGameOverStatus.text = "Player 1 Destroyed";
        }
        if (Player2Health.player2Health.curPlayer2Health <= 0) 
        {
            textGameOverStatus.text = "Player 2 Destroyed";
        }

    }

    public void inputNavigationConfirm(InputAction.CallbackContext context) 
    {
        if (context.performed) 
        {
            if (LevelStatus.levelStatus.levelID != 4)
            {
                if (GameOver.gameOver.isGameOver && !SceneSystem.sceneSystem.isChangeScene)
                {
                    if (curValueButton == 1)
                    {
                        sceneSystem.isRestartScene = true;
                    }

                    if (curValueButton == 2)
                    {
                        sceneSystem.isExitScene = true;
                    }
                    
                }
            }

        }
    }

    public void inputNavigationUp(InputAction.CallbackContext context) 
    {
        if (context.performed)
        {
            if (GameOver.gameOver.isGameOver && !SceneSystem.sceneSystem.isChangeScene)
            {
                curValueButton--;
                if (curValueButton < 1)
                {
                    curValueButton = maxListButton;
                }
                MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
            }
          

        }

    }

    public void inputNavigationDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (GameOver.gameOver.isGameOver && !SceneSystem.sceneSystem.isChangeScene) 
            {
                curValueButton++;
                if (curValueButton > maxListButton)
                {
                    curValueButton = 1;
                }
                MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
            }
         

        }

    }

    void inputConfirmButton()
    {
        if (LevelStatus.levelStatus.levelID != 4)
        {
            if (GameOver.gameOver.isGameOver && !SceneSystem.sceneSystem.isChangeScene)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Gamepad Enter"))
                {
                    if (curValueButton == 1)
                    {
                        sceneSystem.isRestartScene = true;
                        //nanti di ubah ke next scene
                    }

                    if (curValueButton == 2)
                    {
                        sceneSystem.isExitScene = true;
                    }
                }
            }
        }

    }

    void inputListSelection()
    {
        if (LevelStatus.levelStatus.levelID != 4)
        {
            if (GameOver.gameOver.isGameOver && !SceneSystem.sceneSystem.isChangeScene)
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

    }

    IEnumerator waiToActive() 
    {
        yield return new WaitForSeconds(2.5f);
        gameOverUI.SetActive(true);
    }

    private void mouseListener()
    {

        for (int j = 0; j < listGameOverButton.Length; j++)
        {
            if (!SceneSystem.sceneSystem.isChangeScene)
            {
                int buttonValue = curValueButtonIndex[j];

                EventTrigger eventTrigger = listGameOverButton[j].gameObject.GetComponent<EventTrigger>();

                if (eventTrigger == null)
                {
                    eventTrigger = listGameOverButton[j].gameObject.AddComponent<EventTrigger>();
                }

                // cursor masuk ke dalam tombol
                EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
                pointerEnter.eventID = EventTriggerType.PointerEnter;
                pointerEnter.callback.AddListener((data) => { OnButtonPointerEnter(); });
                pointerEnter.callback.AddListener((data) => { buttonGameOverHighlighted(buttonValue); });
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
    void buttonGameOverHighlighted(int value)
    {
        buttonHighlightedValue = value;
    }

    public void onClickRestart()
    {
        sceneSystem.isRestartScene = true;

    }
    public void onClickExit()
    {
        sceneSystem.isExitScene = true;


    }
}
