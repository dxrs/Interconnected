using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Finish : MonoBehaviour
{

    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] SceneSystem sceneSystem;

    [SerializeField] GameObject finishUI;
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject finishSelector;

    [SerializeField] int curValueButton;
    [SerializeField] int[] curValueButtonIndex; // buat mouse cursor
    [SerializeField] int maxListButton;
    [SerializeField] int buttonHighlightedValue; // yang di highlight sama cursor mouse

    [SerializeField] Button[] listPauseButton;

    [SerializeField] Vector2[] finishSelectorPos;

    bool isDpadPressed = false;

    private void Start()
    {
        finishUI.SetActive(false);
        mouseListener();
        curValueButton = 1;
        buttonHighlightedValue = 1;
    }

    private void Update()
    {
        if (globalVariable.isGameFinish) 
        {
            StartCoroutine(waitToActive());
            if (MouseCursorActivated.mouseCursorActivated.isMouseActive)
            {
                curValueButton = buttonHighlightedValue;
            }
            else
            {
                buttonHighlightedValue = curValueButton;
            }
        }

        finishInputConfirmButton();
        finishInputListSelection();
        selectorPos();
    }

    private void mouseListener()
    {
        do
        {
            for (int j = 0; j < listPauseButton.Length; j++)
            {
                int buttonValue = curValueButtonIndex[j];

                EventTrigger eventTrigger = listPauseButton[j].gameObject.AddComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerEnter;
                entry.callback.AddListener((data) => { buttonPauseHighlighted(buttonValue); });
                eventTrigger.triggers.Add(entry);
            }
        } while (globalVariable.isGameFinish);
    }

    void buttonPauseHighlighted(int value)
    {
        buttonHighlightedValue = value;
    }

    void finishInputConfirmButton()
    {
        if (globalVariable.isGameFinish)
        {

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Gamepad Enter"))
            {

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

    void finishInputListSelection()
    {
        if (globalVariable.isGameFinish)
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

    void selectorPos()
    {
        if (!MouseCursorActivated.mouseCursorActivated.isMouseActive && globalVariable.isGameFinish)
        {
            for (int j = 0; j < finishSelectorPos.Length; j++)
            {
                if (curValueButton == j + 1)
                {
                    finishSelector.transform.localPosition = finishSelectorPos[j];
                }

            }

        }
        for (int i = 0; i < finishSelectorPos.Length; i++)
        {
            if (MouseCursorActivated.mouseCursorActivated.isMouseActive)
            {
                if (buttonHighlightedValue == i + 1)
                {
                    finishSelector.transform.localPosition = finishSelectorPos[i];
                }

            }
        }
    }

    IEnumerator waitToActive() 
    {
        yield return new WaitForSeconds(1.5f);
        finishUI.SetActive(true);
        inGameUI.SetActive(false);
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
