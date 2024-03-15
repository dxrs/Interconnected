using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyToStart : MonoBehaviour
{
    public static ReadyToStart readyToStart;

    public bool isGameStart;

    [SerializeField] float timerCountToStart;

    [SerializeField] TextMeshProUGUI textTimerCountToStart;

    [SerializeField] GameObject startUI;
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject dialogue;

 
    private void Awake()
    {
        readyToStart = this;
    }

    private void Start()
    {
        inGameUI.SetActive(false);
        startUI.SetActive(true);
        timerCountToStart = 3;
        dialogue.SetActive(false);
    }

    private void Update()
    {
        
        if (LevelStatus.levelStatus.levelID != 4) 
        {
            StartCoroutine(waitToCount());

            if (timerCountToStart > 1)
            {
                textTimerCountToStart.text = Mathf.RoundToInt(timerCountToStart).ToString();
            }
            if (isGameStart && !GameFinish.gameFinish.isGameFinish)
            {
                inGameUI.SetActive(true);
                startUI.SetActive(false);
                
                if (DialogueManager.dialogueManager.isDialogueActive) 
                {
                    dialogue.SetActive(true);

                }
                else 
                {
                    dialogue.SetActive(false);
                }
            }
        }
        else 
        {
            isGameStart = true;
            if (isGameStart && !GameFinish.gameFinish.isGameFinish)
            {
                inGameUI.SetActive(true);
                startUI.SetActive(false);
            }
        }
      
    }

    IEnumerator waitToCount() 
    {
        yield return new WaitForSeconds(1);
        
        if (timerCountToStart > 0)
        {
            timerCountToStart -= 1.2f * Time.deltaTime;
        }
        if (timerCountToStart <= 0) 
        {
            textTimerCountToStart.text = "Let's go !";
            yield return new WaitForSeconds(.7f);
            isGameStart = true;
        }
    }
}
