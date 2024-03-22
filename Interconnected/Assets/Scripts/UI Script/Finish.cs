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

    [SerializeField] TextMeshProUGUI textFinishStatus;

    [SerializeField] int curValueButton;
    [SerializeField] int[] curValueButtonIndex; // buat mouse cursor
    [SerializeField] int maxListButton;
    [SerializeField] int buttonHighlightedValue; // yang di highlight sama cursor mouse

    [SerializeField] bool isButtonHighlighted;

    [SerializeField] Button[] listFinishButton;

    bool isDpadPressed = false;
    bool isSaving = false;

    private void Start()
    {
        finishUI.SetActive(false);
        mouseListener();
        curValueButton = 0;
        buttonHighlightedValue = 0;
    }

    private void Update()
    {
        if (GameFinish.gameFinish.isGameFinish) 
        {
            if (SceneSystem.sceneSystem.isChangeScene) 
            {
                for (int k = 0; k < listFinishButton.Length; k++)
                {
                    listFinishButton[k].interactable = false;
                }
            }
            if (LevelStatus.levelStatus.levelID == 2) 
            {
                //textFinishStatusEnemyDestroy.text = EnemyTargetDestroy.enemyTargetDestroy.curValueEnemyDestroy + " Enemy Destroyed";
                
            }
            compareButtonValue();
            StartCoroutine(waitToActive());
           
        }
        
        

        for (int i = 0; i < listFinishButton.Length; i++)
        {
            if (curValueButton == i + 1)
            {
                listFinishButton[i].transform.localScale = Vector2.Lerp(listFinishButton[i].transform.localScale, new Vector2(1.1f, 1.1f), 10 * Time.unscaledDeltaTime);
            }
            else
            {
                listFinishButton[i].transform.localScale = Vector2.Lerp(listFinishButton[i].transform.localScale, new Vector2(1, 1), 16 * Time.unscaledDeltaTime);
            }
        }
    }

    private void mouseListener()
    {

        for (int j = 0; j < listFinishButton.Length; j++)
        {
            if (!SceneSystem.sceneSystem.isChangeScene)
            {
                int buttonValue = curValueButtonIndex[j];

                EventTrigger eventTrigger = listFinishButton[j].gameObject.GetComponent<EventTrigger>();

                if (eventTrigger == null)
                {
                    eventTrigger = listFinishButton[j].gameObject.AddComponent<EventTrigger>();
                }

                // cursor masuk ke dalam tombol
                EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
                pointerEnter.eventID = EventTriggerType.PointerEnter;
                pointerEnter.callback.AddListener((data) => { OnButtonPointerEnter(); });
                pointerEnter.callback.AddListener((data) => { buttonFinishHighlighted(buttonValue); });
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
    }

    public void inputNavigationConfirm(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (LevelStatus.levelStatus.levelID != 4)
            {
                if (GameFinish.gameFinish.isGameFinish && !SceneSystem.sceneSystem.isChangeScene)
                {
                    if (curValueButton == 1)
                    {
                        sceneSystem.isNextScene = true;
                        if (!isSaving)
                        {
                            LevelManager.levelManager.saveDataCurrentLevel();
                            isSaving = true;
                        }
                        //kalau continue save data totalLevelUnlocked dari script level manager
                    }

                    if (curValueButton == 2)
                    {
                        sceneSystem.isExitScene = true;
                    }
                    
                }
            }
            if(LevelStatus.levelStatus.levelID == 4)
            {
                if (GameFinish.gameFinish.isGameFinish && !SceneSystem.sceneSystem.isChangeScene) 
                {
                    sceneSystem.isNextScene = true;
                    if (!isSaving)
                    {
                        LevelManager.levelManager.saveDataCurrentLevel();
                        isSaving = true;
                    }
                }
               
            }
        }
    }

    public void inputNavigationUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (GameFinish.gameFinish.isGameFinish && !SceneSystem.sceneSystem.isChangeScene) 
            {
                if (LevelStatus.levelStatus.levelID != 4)
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

    }

    public void inputNavigationDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (GameFinish.gameFinish.isGameFinish && !SceneSystem.sceneSystem.isChangeScene)
            {
                if (LevelStatus.levelStatus.levelID != 4)
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
                        if (!isSaving) 
                        {
                            LevelManager.levelManager.saveDataCurrentLevel();
                            isSaving = true;
                        }
                        //kalau continue save data totalLevelUnlocked dari script level manager
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

   
        
    IEnumerator waitToActive() 
    {
        yield return new WaitForSeconds(.1f);
        finishUI.SetActive(true);
        inGameUI.SetActive(false);
    }

    public void onClickContinue()
    {
        sceneSystem.isNextScene = true;
        LevelManager.levelManager.saveDataCurrentLevel();
        //kalau continue save data totalLevelUnlocked dari script level manager
    }
    public void onClickExit()
    {
        sceneSystem.isExitScene = true;
    }

   
}
