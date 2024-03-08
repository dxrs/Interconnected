using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.InputSystem;

public class Finish : MonoBehaviour
{

    [SerializeField] SceneSystem sceneSystem;

    [SerializeField] GameObject finishUI;
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject buttonSelector;

    [SerializeField] TextMeshProUGUI textFinishStatusEnemyDestroy;

    [SerializeField] int curValueButton;
    [SerializeField] int[] curValueButtonIndex; // buat mouse cursor
    [SerializeField] int maxListButton;
    [SerializeField] int buttonHighlightedValue; // yang di highlight sama cursor mouse

    [SerializeField] Button[] listFinishButton;
    [SerializeField] Button[] allButtonDisable;

    [SerializeField] float[] buttonSelectorPosY;

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
        if (GameFinish.gameFinish.isGameFinish) 
        {
            if (SceneSystem.sceneSystem.isChangeScene) 
            {
                for (int k = 0; k < allButtonDisable.Length; k++)
                {
                    allButtonDisable[k].interactable = false;
                }
            }
            if (LevelStatus.levelStatus.levelID == 2) 
            {
                //textFinishStatusEnemyDestroy.text = EnemyTargetDestroy.enemyTargetDestroy.curValueEnemyDestroy + " Enemy Destroyed";
                
            }
            compareButtonValue();
            StartCoroutine(waitToActive());
           
        }
        
        inputConfirmButton();
        inputListSelection();
        selectorPos();
    }

    private void mouseListener()
    {
        do
        {
            for (int j = 0; j < listFinishButton.Length; j++)
            {
                int buttonValue = curValueButtonIndex[j];

                EventTrigger eventTrigger = listFinishButton[j].gameObject.AddComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerEnter;
                entry.callback.AddListener((data) => { buttonFinishHighlighted(buttonValue); });
                eventTrigger.triggers.Add(entry);
            }
        } while (GameFinish.gameFinish.isGameFinish && LevelStatus.levelStatus.levelID != 4 && !SceneSystem.sceneSystem.isChangeScene);
    }

    void buttonFinishHighlighted(int value)
    {
        buttonHighlightedValue = value;
    }

    void compareButtonValue() 
    {
        if(LevelStatus.levelStatus.levelID != 4) 
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

    void inputConfirmButton()
    {
        if (LevelStatus.levelStatus.levelID != 4) 
        {
            if (GameFinish.gameFinish.isGameFinish && !SceneSystem.sceneSystem.isChangeScene)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Gamepad Enter"))
                {
                    if (curValueButton == 1)
                    {
                        sceneSystem.isNextScene = true;
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
            if (GameFinish.gameFinish.isGameFinish && !SceneSystem.sceneSystem.isChangeScene)
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

    void selectorPos()
    {
        if (LevelStatus.levelStatus.levelID != 4)
        {
            if (!MouseCursorActivated.mouseCursorActivated.isMouseActive && GameFinish.gameFinish.isGameFinish)
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

    IEnumerator waitToActive() 
    {
        yield return new WaitForSeconds(.1f);
        finishUI.SetActive(true);
        inGameUI.SetActive(false);
    }

    public void onClickContinue()
    {
        sceneSystem.isNextScene = true;

        // nanti ke next scene

    }
    public void onClickExit()
    {
        sceneSystem.isExitScene = true;


    }

   
}
