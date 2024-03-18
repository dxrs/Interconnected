using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectChapter : MonoBehaviour
{
    public static SelectChapter selectChapter;

    public bool isSelectChapterActive;


    public int curValueButton;

    [SerializeField] bool isLevelButtonHighlighted;

    [SerializeField] int[] curValueButtonIndex;
    [SerializeField] int buttonHighlightedValue;

    [SerializeField] Button[] listChapterButton;

    [SerializeField] Image imageTransition;

    [SerializeField] GameObject[] animatedChapterButton;

    Color alpahColor;
 
    private void Awake()
    {
        selectChapter = this;
        isSelectChapterActive = true;
    }

    private void Start()
    {
        mouseListener();

       
        alpahColor.a = 1;
        imageTransition.color = alpahColor;
        
    }
    private void Update()
    {
        curValueButton = buttonHighlightedValue;
        alpahColor.a = Mathf.MoveTowards(alpahColor.a, 0, 2 * Time.deltaTime);
        imageTransition.color = alpahColor;
    }
    private void mouseListener()
    {
        for (int j = 0; j < listChapterButton.Length; j++)
        {
            if (isSelectChapterActive)
            {
                int buttonValue = curValueButtonIndex[j];

                EventTrigger eventTrigger = listChapterButton[j].gameObject.GetComponent<EventTrigger>();

                if (eventTrigger == null)
                {
                    eventTrigger = listChapterButton[j].gameObject.AddComponent<EventTrigger>();
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
        if (isSelectChapterActive) 
        {
            buttonHighlightedValue = 0;
        }
        
    }

    private void buttonMainMenuHighlighted(int value)
    {
        if (isSelectChapterActive) 
        {
            
            if (value <= ChapterManager.chapterManager.currentChapter && !SceneSystem.sceneSystem.isChangeScene)
            {
                buttonHighlightedValue = value;
            }
        }
       
    }

    public void onClickButtonChapter() 
    {
        isSelectChapterActive = false;
        SceneSystem.sceneSystem.goingToSelectLevelPerChapter();
    }
}
