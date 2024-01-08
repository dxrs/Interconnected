using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public static Pause pause;

    public bool isGamePaused;

    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject pauseSelector;

    [SerializeField] bool isMouseMoving;

    [SerializeField] int curValueButton;
    [SerializeField] int[] curValueButtonIndex; // buat mouse cursor
    [SerializeField] int maxListButton;
    [SerializeField] int buttonPauseHighlightedValue; // yang di highlight sama cursor mouse
    [SerializeField] int buttonPauseSelectedValue;

    [SerializeField] Button[] listPauseButton;

    [SerializeField] Vector2[] pauseSelectorPos;

    bool dpadPressed = false;


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

        if (isGamePaused)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            if (Mathf.Abs(mouseX) > 0.1f || Mathf.Abs(mouseY) > 0.1f)
            {
                isMouseMoving = true;
            }
            else 
            {
                //isMouseMoving = false;
            }

            
            if (isMouseMoving)
            {
                curValueButton = buttonPauseHighlightedValue;
                Debug.Log("Mouse bergerak");
            }
            else
            {
                buttonPauseHighlightedValue = curValueButton;
                Debug.Log("Mouse tidak bergerak");
            }
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseUI.SetActive(false);
            curValueButton = 1;
            Time.timeScale = 1;
        }
        pauseInput();
        selectorPos();
    }

    void selectorPos() 
    {
        if(!isMouseMoving && isGamePaused) 
        {
            if (curValueButton == 1) 
            {
                pauseSelector.transform.localPosition = pauseSelectorPos[0];
            }
            if (curValueButton == 2) 
            {
                pauseSelector.transform.localPosition = pauseSelectorPos[1];
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

                listPauseButton[j].onClick.AddListener(() => buttonPauseClick(buttonValue));

                EventTrigger eventTrigger = listPauseButton[j].gameObject.AddComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerEnter;
                entry.callback.AddListener((data) => { buttonPauseHighlighted(buttonValue); });
                eventTrigger.triggers.Add(entry);
            }
        } while (isGamePaused);
    }

    void buttonPauseClick(int value) 
    {
        buttonPauseSelectedValue = value;
    }
    void buttonPauseHighlighted(int value)
    {
        buttonPauseHighlightedValue = value;
       

    }
    void pauseInput() 
    {
        if (isGamePaused)
        {
            
            float inputDpadVertical = Input.GetAxis("Dpad");

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

            if (inputDpadVertical == 1 && !dpadPressed)
            {
                isMouseMoving = false;
                curValueButton++;
                if (curValueButton > maxListButton)
                {
                    curValueButton = 1;
                }
                dpadPressed = true;
            }
            if (inputDpadVertical == -1 && !dpadPressed)
            {
                isMouseMoving = false;
                curValueButton--;
                if (curValueButton < 1)
                {
                    curValueButton = maxListButton;
                }
                dpadPressed = true;
            }

            if (inputDpadVertical == 0)
            {
                dpadPressed = false;
            }
        }
    }
}
