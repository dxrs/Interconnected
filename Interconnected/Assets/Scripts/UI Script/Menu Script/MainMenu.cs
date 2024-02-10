using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public static MainMenu mainMenu;

    public bool isMainMenuActive;

    [SerializeField] int curValueButton;
    [SerializeField] int[] curValueButtonIndex;
    [SerializeField] int maxListButton;
    [SerializeField] int buttonHighlightedValue;

    [SerializeField] Button[] listMainMenuButton;

    [SerializeField] GameObject mainMenuSelector;

    [SerializeField] Vector2[] mainMenuSelectorPosY;


    private void Awake()
    {
        mainMenu = this;
    }
    private void Start()
    {
        mouseListener();
        curValueButton = 1;
        buttonHighlightedValue = 1;
    }

    private void Update()
    {
        mainMenuValue();
    }

    private void mainMenuValue() 
    {
        if (isMainMenuActive)
        {
            for (int k = 0; k < listMainMenuButton.Length; k++)
            {
                listMainMenuButton[k].interactable = true;
            }
            if (MouseCursorActivated.mouseCursorActivated.isMouseActive)
            {
                curValueButton = buttonHighlightedValue;
            }
            else
            {
                buttonHighlightedValue = curValueButton;
            }

            if (!MouseCursorActivated.mouseCursorActivated.isMouseActive)
            {
                for (int j = 0; j < mainMenuSelectorPosY.Length; j++)
                {
                    if (curValueButton == j + 1)
                    {
                        mainMenuSelector.transform.localPosition = mainMenuSelectorPosY[j];
                    }
                }
            }

            for (int i = 0; i < mainMenuSelectorPosY.Length; i++)
            {
                if (buttonHighlightedValue == i + 1)
                {
                    mainMenuSelector.transform.localPosition = mainMenuSelectorPosY[i];
                }
            }
        }
        else 
        {
            for(int k = 0; k < listMainMenuButton.Length; k++) 
            {
                listMainMenuButton[k].interactable = false;
            }
        }
       
    }

    private void mouseListener()
    {
        for (int j = 0; j < listMainMenuButton.Length; j++)
        {
            if (isMainMenuActive) 
            {
                int buttonValue = curValueButtonIndex[j];

                EventTrigger eventTrigger = listMainMenuButton[j].gameObject.AddComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerEnter;
                entry.callback.AddListener((data) => { buttonMainMenuHighlighted(buttonValue); });
                eventTrigger.triggers.Add(entry);
            }
           
        }
       
    }

    #region input system navigation
    private void buttonMainMenuHighlighted(int value) 
    {
        buttonHighlightedValue = value;
    }

    public void inputNavigationUp(InputAction.CallbackContext context) 
    {
        if (context.performed) 
        {
            MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
            curValueButton--;
            if (curValueButton < 1)
            {
                curValueButton = maxListButton;
            }

        }
    }

    public void inputNavigationDown(InputAction.CallbackContext context) 
    {
        if (context.performed)
        {
            MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
            curValueButton++;
            if (curValueButton > maxListButton)
            {
                curValueButton = 1;
            }
            
        }
    }

    public void inputNavigationConfirm(InputAction.CallbackContext context) 
    {
        if (isMainMenuActive) 
        {
            if (context.performed)
            {
                if (curValueButton == 1)
                {
                    Debug.Log("1");

                    //jika tidak ada data
                    SceneSystem.sceneSystem.goingToTutorialScene();
                    isMainMenuActive = false;
                }
                if (curValueButton == 2)
                {
                    Debug.Log("2");
                }
                if (curValueButton == 3)
                {
                    Debug.Log("3");
                }
                if (curValueButton == 4)
                {
                    Debug.Log("4");
                }
            }
        }
        
    }
    #endregion
}
