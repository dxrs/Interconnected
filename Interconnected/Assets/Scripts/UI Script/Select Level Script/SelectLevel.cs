using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public static SelectLevel selectLevel;

    public int curSelectLevelValue;

    public int curLevelSectionValue;

    public bool isCameraNotMoving;
    public bool isLevelButtonHighlighted;
    public bool isInputKeyboardChoose;

    [SerializeField] int curValueButton;
    [SerializeField] int[] curValueButtonIndex;
    [SerializeField] int buttonHighlightedValue;

    [SerializeField] GameObject[] typeLevelObject;

    [SerializeField] Button[] listLevelButton;
    private void Awake()
    {
        selectLevel = this;
    }
    private void Start()
    {
        mouseListener();
        Time.timeScale = 1;
    }
    private void Update()
    {
        curSelectLevelValue = buttonHighlightedValue;
        compareLevelTypeValue();
        compareSectionValue();
        if (MouseCursorActivated.mouseCursorActivated.isMouseActive) 
        {
            isInputKeyboardChoose = false;
        }
        else 
        {
            isInputKeyboardChoose = true;
        }
    }
    private void mouseListener()
    {
        for (int j = 0; j < listLevelButton.Length; j++)
        {
            if (!isCameraNotMoving)
            {
                int buttonValue = curValueButtonIndex[j];

                EventTrigger eventTrigger = listLevelButton[j].gameObject.GetComponent<EventTrigger>();

                if (eventTrigger == null)
                {
                    eventTrigger = listLevelButton[j].gameObject.AddComponent<EventTrigger>();
                }

                // cursor masuk ke dalam tombol
                EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
                pointerEnter.eventID = EventTriggerType.PointerEnter;
                pointerEnter.callback.AddListener((data) => { OnButtonPointerEnter(); });
                pointerEnter.callback.AddListener((data) => { buttonMainMenuHighlighted(buttonValue); });
                eventTrigger.triggers.Add(pointerEnter);

                // cursor keluar dari tombol
                EventTrigger.Entry pointerExit = new EventTrigger.Entry();
                pointerExit.eventID = EventTriggerType.PointerExit;
                pointerExit.callback.AddListener((data) => { OnButtonPointerExit(); });
                eventTrigger.triggers.Add(pointerExit);
            }
        }
    }

    private void compareSectionValue() 
    {
        if (isInputKeyboardChoose) 
        {
            curLevelSectionValue = Mathf.FloorToInt((curSelectLevelValue) / 3.0f);

            /*
            if (curSelectLevelValue == Mathf.Clamp(curSelectLevelValue, 0, 2))
            {
                curLevelSectionValue = 0;
            }
            if (curSelectLevelValue == Mathf.Clamp(curSelectLevelValue, 3, 5))
            {
                curLevelSectionValue = 1;
            }
            if (curSelectLevelValue == Mathf.Clamp(curSelectLevelValue, 6, 8)) 
            {
                curLevelSectionValue = 2;
            }
            */
        }
       
    }

    private void OnButtonPointerEnter()
    {
        Debug.Log("true - Highlighted Button Value: ");
        isLevelButtonHighlighted = true;
    }

    private void OnButtonPointerExit()
    {
        Debug.Log("false - Unhighlighted Button Value: ");
        isLevelButtonHighlighted = false;
    }

    private void compareLevelTypeValue() 
    {
        for (int i = 0; i < typeLevelObject.Length; i++)
        {
            if (!SceneSystem.sceneSystem.isChangeScene) 
            {
                typeLevelObject[i].SetActive(buttonHighlightedValue == i);
            }
            
            
        }
    }

    private void buttonMainMenuHighlighted(int value)
    {
        buttonHighlightedValue = value;
    }


    public void inputNavigationRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
            if (!SceneSystem.sceneSystem.isChangeScene)
            {
                if (curSelectLevelValue <= LevelManager.levelManager.totalLevel)
                {
                    buttonHighlightedValue++;
                }
            }
           
        }
    }

    public void inputNavigationLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
            if (!SceneSystem.sceneSystem.isChangeScene) 
            {
                if (curSelectLevelValue > 0)
                {
                    buttonHighlightedValue--;
                }
            }
            
            
        }
    }

    public void inputNavigationConfirm(InputAction.CallbackContext context) 
    {
        if (context.performed) 
        {
            if(isInputKeyboardChoose && isCameraNotMoving) 
            {
                SceneSystem.sceneSystem.goingToLevelSelected();
            }
        }
    }

    public void onClickChooseLevelLeft() 
    {
        if (!SceneSystem.sceneSystem.isChangeScene) 
        {
            if (curSelectLevelValue > 0) 
            {
                curLevelSectionValue--;
            }
            
            buttonHighlightedValue = curLevelSectionValue * 3;
        }
        
    }

    public void onClickChooseLevelRight() 
    {
        if (!SceneSystem.sceneSystem.isChangeScene)
        {
            if (curSelectLevelValue <= LevelManager.levelManager.totalLevel) 
            {
                curLevelSectionValue++;
            }

            buttonHighlightedValue = curLevelSectionValue * 3;
        }
        
    }

    public void onClickLevelButton() 
    {
        SceneSystem.sceneSystem.goingToLevelSelected();
    }
}
