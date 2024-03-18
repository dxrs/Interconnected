using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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

    [SerializeField] bool isTransitionSceneActive;
    [SerializeField] bool isLevelButtonHighlighted;

    [SerializeField] Button[] listMainMenuButton;

    [SerializeField] Image imageTransition;
    [SerializeField] GameObject[] animatedButtonSelect;

    Color alphaColor;

    private void Awake()
    {
        mainMenu = this;
    }
    private void Start()
    {
        mouseListener();
        alphaColor.a = 0;

    }

    private void Update()
    {
        mainMenuValue();
        compareValueButton();

        if (isTransitionSceneActive) 
        {
            alphaColor.a = Mathf.MoveTowards(alphaColor.a, 1, 3 * Time.deltaTime);
            imageTransition.color = alphaColor;
            if (alphaColor.a >= 1) 
            {
                SceneSystem.sceneSystem.goingToChapterSelect();
            }
        }
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
                if (!isLevelButtonHighlighted) 
                {
                    curValueButton = 0;
                    
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

                EventTrigger eventTrigger = listMainMenuButton[j].gameObject.GetComponent<EventTrigger>();

                if (eventTrigger == null)
                {
                    eventTrigger = listMainMenuButton[j].gameObject.AddComponent<EventTrigger>();
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

    private void compareValueButton() 
    {
        for (int i = 0; i < animatedButtonSelect.Length - 1; i++)
        {
            if (curValueButton == i + 1)
            {
                animatedButtonSelect[i].transform.localScale = Vector2.Lerp(animatedButtonSelect[i].transform.localScale, new Vector2(1.1f, 1.1f), 10 * Time.deltaTime);
            }
            else
            {
                animatedButtonSelect[i].transform.localScale = Vector2.Lerp(animatedButtonSelect[i].transform.localScale, new Vector2(1, 1), 16 * Time.deltaTime);
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
                    //SceneSystem.sceneSystem.goingToTutorialScene();
                    //jika ada 
                    // ke scene chapter
                    isTransitionSceneActive = true;
                    isMainMenuActive = false;
                }
                if (curValueButton == 2)
                {
                    Debug.Log("2");
                }
                if (curValueButton == 3)
                {
                    Debug.Log("3");
                    Application.Quit();
                }
            }
        }
        
    }
    #endregion

    public void onClickToPrologue() 
    {
        isTransitionSceneActive = true;
        isMainMenuActive = false;
    }
    public void onClickQuitGame() 
    {
        Application.Quit();
    }
}
