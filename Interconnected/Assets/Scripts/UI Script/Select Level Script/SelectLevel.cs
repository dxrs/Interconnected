using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class SelectLevel : MonoBehaviour
{
    public static SelectLevel selectLevel;

    public int curSelectLevelValue;

    public int curLevelSectionValue; // variable buat pengolompokan level, di sini saya buat 3 level per section

    public bool isCameraNotMoving;
    public bool isInputKeyboardChoose;

    [SerializeField] bool isLevelButtonHighlighted;
    

    [SerializeField] int curValueButton;
    [SerializeField] int[] curValueButtonIndex;
    [SerializeField] int buttonHighlightedValue;

    [SerializeField] GameObject[] typeLevelObject;

    [SerializeField] Button[] listLevelButton;
    [SerializeField] Button[] listContentButton;
    private void Awake()
    {
        selectLevel = this;
    }
    private void Start()
    {
        mouseListener();

        buttonHighlightedValue = LevelManager.levelManager.totalLevelUnlocked;

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
    private void OnButtonPointerEnter()
    {
        isLevelButtonHighlighted = true;
    }

    private void OnButtonPointerExit()
    {
        isLevelButtonHighlighted = false;
    }
    private void compareSectionValue() 
    {
        if (isInputKeyboardChoose) 
        {
            //curLevelSectionValue = Mathf.FloorToInt((curSelectLevelValue) / 3);

           
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
            
        }
       
    }

    private void compareLevelTypeValue() 
    {

        for (int i = 0; i < typeLevelObject.Length; i++)
        {
            typeLevelObject[i].SetActive(buttonHighlightedValue == i && listLevelButton[i].enabled);
        }

        /*
        if(buttonHighlightedValue==0 && listLevelButton[0].enabled == true) 
        {
            typeLevelObject[0].SetActive(true);
        }
        else 
        {
            typeLevelObject[0].SetActive(false);
        }

        if (buttonHighlightedValue == 1 && listLevelButton[1].enabled == true)
        {
            typeLevelObject[1].SetActive(true);
        }
        else
        {
            typeLevelObject[1].SetActive(false);
        }

        if (buttonHighlightedValue == 2 && listLevelButton[2].enabled == true)
        {
            typeLevelObject[2].SetActive(true);
        }
        else
        {
            typeLevelObject[2].SetActive(false);
        }

        
        for (int i = 0; i < typeLevelObject.Length; i++)
        {
            if (!SceneSystem.sceneSystem.isChangeScene) 
            {
                typeLevelObject[i].SetActive(buttonHighlightedValue == i);
            }
            
            
        }
        */
    }

    private void buttonMainMenuHighlighted(int value)
    {
        
        if (value <= LevelManager.levelManager.totalLevelUnlocked && !SceneSystem.sceneSystem.isChangeScene) 
        {
            buttonHighlightedValue = value;
        }
    }


    public void inputNavigationRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
            if (!SceneSystem.sceneSystem.isChangeScene)
            {
                if (curSelectLevelValue < LevelManager.levelManager.totalLevelUnlocked)
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

   

    public void onClickChooseLevelLeft() 
    {
        if (!SceneSystem.sceneSystem.isChangeScene) 
        {
            if (curLevelSectionValue > 0) 
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

            if (curLevelSectionValue < LevelManager.levelManager.currentTotalLevelSection)
            {
                curLevelSectionValue++;
                buttonHighlightedValue = curLevelSectionValue * 3;
            }

        }
        
    }

  

    public void onClickBackToMenu() 
    {
        SceneSystem.sceneSystem.goingToMainMenu();
        for(int n = 0; n < listContentButton.Length; n++) 
        {
            listContentButton[n].enabled = false;
        }
    }
}
