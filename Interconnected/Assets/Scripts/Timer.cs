using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public static Timer timerInstance;

    public bool isTimerLevel;

    public float curTimerValue;

    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] SceneSystem sceneSystem;

    [SerializeField] TextMeshProUGUI textCurrentTimer;

    bool isTimerStop = false;

    TimeSpan timerCount;

    private void Awake()
    {
        timerInstance = this;
    }

    private void Start()
    {
        
        StartCoroutine(timerCountDown());
    }

    private void Update()
    {
        if (LevelStatus.levelStatus.levelID != 4) 
        {
            if (curTimerValue <= 0)
            {
                curTimerValue = 0;
            }
        }

        if (isTimerLevel) 
        {
            textCurrentTimer.enabled = true;
        }
        else 
        {
            textCurrentTimer.enabled = false;
        }
        
    }
    
    IEnumerator timerCountDown()
    {
        while (isTimerLevel)
        {
            if (LevelStatus.levelStatus.levelID == 1) 
            {
                if (ReadyToStart.readyToStart.isGameStart
                    && !GameFinish.gameFinish.isGameFinish
                    && !GameOver.gameOver.isGameOver)
                {
                    if (!isTimerStop)
                    {
                        if (curTimerValue > 0)
                        {
                            curTimerValue -= Time.deltaTime;
                        }

                        timerCount = TimeSpan.FromSeconds(curTimerValue);
                        string timer = timerCount.ToString("mm':'ss':'ff");
                        textCurrentTimer.text = timer;

                    }
                }
            }
            if (LevelStatus.levelStatus.levelID == 2) 
            {
                if (ReadyToStart.readyToStart.isGameStart
                   && !GameFinish.gameFinish.isGameFinish
                   && !GameOver.gameOver.isGameOver
                   && globalVariable.isTimerStart)
                {
                    if (!isTimerStop)
                    {
                        if (curTimerValue > 0)
                        {
                            curTimerValue -= Time.deltaTime;
                        }

                        timerCount = TimeSpan.FromSeconds(curTimerValue);
                        string timer = timerCount.ToString("mm':'ss':'ff");
                        textCurrentTimer.text = timer;

                    }
                }
            }
            
            
            yield return null;
        }
    }
}
