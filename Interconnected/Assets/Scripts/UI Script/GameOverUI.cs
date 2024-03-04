using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] SceneSystem sceneSystem;

    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject buttonSelector;

    [SerializeField] int curValueButton;
    [SerializeField] int[] curValueButtonIndex; // buat mouse cursor
    [SerializeField] int maxListButton;
    [SerializeField] int buttonHighlightedValue; // yang di highlight sama cursor mouse

    [SerializeField] Button[] listGameOverButton;
    [SerializeField] Button[] allButtonDisable;

    [SerializeField] TextMeshProUGUI textGameOverStatus;

    [SerializeField] float[] buttonSelectorPosY;

    bool isDpadPressed = false;

    private void Start()
    {
        gameOverUI.SetActive(false);
        mouseListener();
        curValueButton = 1;
        buttonHighlightedValue = 1;
    }

    private void Update()
    {
        if (GameOver.gameOver.isGameOver) 
        {
            if (SceneSystem.sceneSystem.isChangeScene)
            {
                for (int k = 0; k < allButtonDisable.Length; k++)
                {
                    allButtonDisable[k].interactable = false;
                }
            }

            compareButtonValue();
            StartCoroutine(waiToActive());
        }
        inputConfirmButton();
        inputListSelection();
        selectorPos();
    }

    void compareButtonValue()
    {
        if (LevelStatus.levelStatus.levelID != 4)
        {
            if (MouseCursorActivated.mouseCursorActivated.isMouseActive)
            {
                curValueButton = buttonHighlightedValue;
            }
            else
            {
                buttonHighlightedValue = curValueButton;
            }
        }
    }

    void selectorPos()
    {
        if (LevelStatus.levelStatus.levelID != 4)
        {
            if (!MouseCursorActivated.mouseCursorActivated.isMouseActive && GameOver.gameOver.isGameOver)
            {
                for (int j = 0; j < buttonSelectorPosY.Length; j++)
                {
                    if (curValueButton == j + 1)
                    {
                        buttonSelector.transform.localPosition = new Vector2(buttonSelector.transform.localPosition.x, buttonSelectorPosY[j]);
                    }

                }

            }
            for (int i = 0; i < buttonSelectorPosY.Length; i++)
            {
                if (MouseCursorActivated.mouseCursorActivated.isMouseActive)
                {
                    if (buttonHighlightedValue == i + 1)
                    {
                        buttonSelector.transform.localPosition = new Vector2(buttonSelector.transform.localPosition.x, buttonSelectorPosY[i]);
                    }

                }
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
        do
        {
            for (int j = 0; j < listGameOverButton.Length; j++)
            {
                int buttonValue = curValueButtonIndex[j];

                EventTrigger eventTrigger = listGameOverButton[j].gameObject.AddComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerEnter;
                entry.callback.AddListener((data) => { buttonGameOverHighlighted(buttonValue); });
                eventTrigger.triggers.Add(entry);
            }
        } while (GameFinish.gameFinish.isGameFinish && LevelStatus.levelStatus.levelID != 4 && !SceneSystem.sceneSystem.isChangeScene);
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
